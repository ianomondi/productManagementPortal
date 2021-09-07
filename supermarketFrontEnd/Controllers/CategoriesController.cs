using supermarketFrontEnd.Communication;
using supermarketFrontEnd.Communication.Category;
using supermarketFrontEnd.Helpers;
using supermarketFrontEnd.Models;
using supermarketFrontEnd.Models.Domain.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace supermarketFrontEnd.Controllers
{
    public class CategoriesController : Controller
    {
        readonly ICategoryService _categoryService;
        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        // GET: Categories
        public async Task<ActionResult> Index()
        {
            IEnumerable<Category> categories = await _categoryService.ListAsync();   

            return View(categories);
        }

        public async Task<ActionResult> Create()
        {
            Category category = new Category
            {
                name = "Mens' Fashion"
            };

            try
            {
                CategoryResponse cat = await _categoryService.SaveAsync(category);

            }catch(Exception e)
            {
                Utils.HandleException(e);
            }

            return View();
        }
        
        [HttpPost]
        public async Task<ActionResult> Create(Category category)
        {
            TempData["Message"] = Utils.GenerateToastSuccess("Error adding category");
            try
            {
                CategoryResponse cat = await _categoryService.SaveAsync(category);

                if (cat.success)
                {
                    TempData["Message"] = Utils.GenerateToastSuccess("Category added successfully");
                    return RedirectToAction("Index");

                }

            }catch(Exception e)
            {
                Utils.HandleException(e);
            }

            return View(category);
        }
        public async Task<ActionResult> Edit(int id)
        {
            try
            {

                IEnumerable<Category> categories = await _categoryService.ListAsync();

                Category category = categories.FirstOrDefault(y => y.id == id);

                return View(category);

            }
            catch(Exception e)
            {
                Utils.HandleException(e);
            }

            TempData["Message"] = Utils.GenerateToastError("Bad Request");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> Edit(int id, Category category)
        {
            TempData["Message"] = Utils.GenerateToastError("Error updating category");
            try
            {

                CategoryResponse categoryResponse = await _categoryService.UpdateAsync(id,category);

                if (categoryResponse.success)
                {
                    TempData["Message"] = Utils.GenerateToastSuccess("Category updated successfully");
                    return RedirectToAction("Index");
                }

                return View(category);

            }
            catch(Exception e)
            {
                Utils.HandleException(e);
            }

            TempData["Message"] = Utils.GenerateToastError("Bad Request");
            return View(category);
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

                CategoryResponse category = await _categoryService.DeleteAsync(id);

                if(category != null && category.Category != null && category.Category.id > 0)
                {
                    response.status = ResultCodes.SUCCESS;
                    response.message = "Category deleted successfully";
                }

            }
            catch(Exception e)
            {
                Utils.HandleException(e);
            }

            return Json(response,JsonRequestBehavior.AllowGet);
        }

        private dynamic Test()
        {
            return new { };
        }
    }
}