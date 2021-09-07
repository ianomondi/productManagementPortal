using supermarketFrontEnd.Communication.Variant;
using supermarketFrontEnd.Communication.VariantOption;
using supermarketFrontEnd.Helpers;
using supermarketFrontEnd.Models;
using supermarketFrontEnd.Models.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace supermarketFrontEnd.Controllers
{
    public class VariantsController : Controller
    {

        readonly IVariantService _variantService;
        readonly IVariantOptionService _variantOptionService;

        public VariantsController(IVariantService variantService, IVariantOptionService variantOptionService)
        {
            _variantService = variantService;
            _variantOptionService = variantOptionService;
        }

        // GET: Variants
        public async Task<ActionResult> Index()
        {
            IEnumerable<Variant> variants = await _variantService.ListAsync();

            return View(variants);
        }

        public async Task<ActionResult> Create()
        {
            Variant variant = new Variant
            {
                name = "Mens' Fashion"
            };

            try
            {
                VariantResponse cat = await _variantService.SaveAsync(variant);

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Variant variant)
        {
            TempData["Message"] = Utils.GenerateToastSuccess("Error adding variant");
            try
            {
                VariantResponse cat = await _variantService.SaveAsync(variant);

                if (cat.success)
                {
                    TempData["Message"] = Utils.GenerateToastSuccess("Variant added successfully");
                    return RedirectToAction("Index");

                }

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }

            return View(variant);
        }
        public async Task<ActionResult> Edit(int id)
        {
            try
            {

                IEnumerable<Variant> variants = await _variantService.ListAsync();

                Variant variant = variants.FirstOrDefault(y => y.id == id);

                return View(variant);

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }

            TempData["Message"] = Utils.GenerateToastError("Bad Request");
            return RedirectToAction("Index");
        }

        public ActionResult ListVariantOptionsPartial(int id)
        {
            try
            {

                var variantOptionstask = _variantOptionService.ListAsync();
                variantOptionstask.Wait();

                IEnumerable<VariantOption> variantOptions = variantOptionstask.Result.Where(vo => vo.variantId == id);

                return PartialView("_ListVariantOptionsPartial", variantOptions);


            }
            catch(Exception e)
            {
                Utils.HandleException(e);
            }

            ViewBag.Message = "No options for this variant.";

            return PartialView("_ListVariantOptionsPartial");
        }


        public async Task<ActionResult> Details(int id)
        {
            try
            {

                IEnumerable<Variant> variants = await _variantService.ListAsync();

                Variant variant = variants.FirstOrDefault(y => y.id == id);

                return View(variant);

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }

            TempData["Message"] = Utils.GenerateToastError("Bad Request");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, Variant variant)
        {
            TempData["Message"] = Utils.GenerateToastError("Error updating variant");
            try
            {

                VariantResponse variantResponse = await _variantService.UpdateAsync(id, variant);

                if (variantResponse.success)
                {
                    TempData["Message"] = Utils.GenerateToastSuccess("Variant updated successfully");
                    return RedirectToAction("Index");
                }

                return View(variant);

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }

            TempData["Message"] = Utils.GenerateToastError("Bad Request");
            return View(variant);
        }

        [HttpPost]
        public async Task<ActionResult> AddOption(int id, string value)
        {
            DefaultJsonResponse response = new DefaultJsonResponse
            {
                status = ResultCodes.FAIL,
                message = Configs.DefaultErrorMessage
            };

            try
            {

                if (id == 0 || string.IsNullOrWhiteSpace(value))
                {
                    response.message = "Invalid request";
                }
                else
                {
                    VariantOption variantOption = new VariantOption
                    {
                        variantId = id,
                        value = value
                    };

                    VariantOptionResponse variantOptionResponse = await _variantOptionService.SaveAsync(variantOption);

                    if (variantOptionResponse.success)
                    {
                        response.status = ResultCodes.SUCCESS;
                        response.message = "Variant option added successfully";
                    }
                    else
                    {
                        response.message = "Failed to create variant option.";
                    }

                }              



            }catch(Exception e)
            {
                Utils.HandleException(e);
            }

            return Json(response);
        }
        [HttpPost]
        public async Task<ActionResult> EditOption(int id, string value)
        {
            DefaultJsonResponse response = new DefaultJsonResponse
            {
                status = ResultCodes.FAIL,
                message = Configs.DefaultErrorMessage
            };

            try
            {

                if (id == 0 || string.IsNullOrWhiteSpace(value))
                {
                    response.message = "Invalid request";
                }
                else
                {

                    IEnumerable<VariantOption> editableVOs = await _variantOptionService.ListAsync();

                    var edittableVO = editableVOs.FirstOrDefault(vo => vo.id == id);

                    if(edittableVO != null)
                    {
                        VariantOption variantOption = new VariantOption
                        {
                            id = id,
                            variantId = edittableVO.variantId,
                            value = value
                        };

                        VariantOptionResponse variantOptionResponse = await _variantOptionService.UpdateAsync(id,variantOption);

                        if (variantOptionResponse.success)
                        {
                            response.status = ResultCodes.SUCCESS;
                            response.message = "Variant option updated successfully";
                        }
                        else
                        {
                            response.message = "Failed to updated variant option.";
                        }
                    }
                    else
                    {
                        response.message = "Variant option not found.";
                    }
                                       

                }              



            }catch(Exception e)
            {
                Utils.HandleException(e);
            }

            return Json(response);
        }

        public async Task<ActionResult> Delete(int id)
        {

            DefaultJsonResponse response = new DefaultJsonResponse
            {
                status = ResultCodes.FAIL,
                message = Configs.DefaultErrorMessage
            };

            try
            {

                VariantResponse variant = await _variantService.DeleteAsync(id);

                if (variant != null && variant.Variant != null && variant.Variant.id > 0)
                {
                    response.status = ResultCodes.SUCCESS;
                    response.message = "Variant deleted successfully";
                }

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
        public async Task<ActionResult> DeleteOption(int id)
        {

            DefaultJsonResponse response = new DefaultJsonResponse
            {
                status = ResultCodes.FAIL,
                message = Configs.DefaultErrorMessage
            };

            try
            {

                VariantOptionResponse variantOption = await _variantOptionService.DeleteAsync(id);

                if (variantOption != null && variantOption.VariantOption != null && variantOption.VariantOption.id > 0)
                {
                    response.status = ResultCodes.SUCCESS;
                    response.message = "Variant Option deleted successfully";
                }

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
