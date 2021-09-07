using AutoMapper;
using supermarketFrontEnd.Communication.CompositeProduct;
using supermarketFrontEnd.Communication.Product;
using supermarketFrontEnd.Communication.ProductVariant;
using supermarketFrontEnd.Helpers;
using supermarketFrontEnd.Models;
using supermarketFrontEnd.Models.Domain.Services;
using supermarketFrontEnd.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace supermarketFrontEnd.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;
        private readonly IProductVariantService _productVariantService;
        private readonly ICompositeProductService _compositeProductService;
        private readonly ICategoryService _categoryService;
        private readonly ISKUService _skuService;
        private readonly IVariantService _variantService;
        private readonly IVariantOptionService _variantOptionService;
        private IMapper _mapper;


        public ProductsController(IProductService productService, 
            ICategoryService categoryService, 
            IVariantService variantService,
            IVariantOptionService variantOptionService,
            IProductVariantService productVariantService,
            ISKUService skuService,
            ICompositeProductService compositeProductService,
            IMapper mapper)
        {
            _productService = productService;
            _categoryService = categoryService;
            _variantService = variantService;
            _variantOptionService = variantOptionService;
            _productVariantService = productVariantService;
            _compositeProductService = compositeProductService;
            _skuService = skuService;
            _mapper = mapper;
        }

        // GET: Products
        public async Task<ActionResult> Index()
        {

            ProductListResponse productListResponse = null;

            List<Product> products = new List<Product>();

            try
            {

                productListResponse = await _productService.ListAsync();

                if(productListResponse != null)
                {
                    products.AddRange(productListResponse.items);
                }

            }
            catch (Exception e)
            {
                TempData["Message"] = Utils.GenerateToastError(e.Message);
                Utils.HandleException(e);
            }

            return View(products);
        }

        // GET: Products/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Products/Create
        public async Task<ActionResult> Create()
        {
            CreateProductViewModel createProductViewModel = null;
            Product product = new Product();

            SKU sku = new SKU();

            product.sku = sku;

            try
            {
                IEnumerable<Category> categories = await _categoryService.ListAsync();

                ViewBag.Categories = categories;

            }catch(Exception e)
            {
                TempData["Message"] = Utils.GenerateToastError(e.Message);
                Utils.HandleException(e);
            }

            try
            {
                createProductViewModel = _mapper.Map<Product, CreateProductViewModel>(product);

                IEnumerable<Variant> variants = await _variantService.ListAsync();
                IEnumerable<VariantOption> variantOptions = await _variantOptionService.ListAsync();

                ProductListResponse products = await _productService.ListAsync();

                List<SelectListItem> relatedProducts = new List<SelectListItem>();

                if (products != null && products.items != null)
                {

                    relatedProducts = products.items.Select(p => new SelectListItem { Text = p.name, Value = $"{p.id}" }).ToList();

                    
                }

                createProductViewModel.variants = variants != null ? variants.ToList() : null;
                createProductViewModel.variantOptions = variantOptions != null ? variantOptions.ToList() : null;
                createProductViewModel.relatedProducts = relatedProducts;


            }catch(Exception e)
            {
                TempData["Message"] = Utils.GenerateToastError(e.Message);
                Utils.HandleException(e);
            }

            return View(createProductViewModel);
        }

        // POST: Products/Create
        [HttpPost]
        public async Task<ActionResult> Create(CreateProductViewModel viewModel, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    List<ProductVariant> variants = viewModel.productVariants;

                    Product product = _mapper.Map<CreateProductViewModel, Product>(viewModel);

                    product.sku = viewModel.sku;
                    product.category = viewModel.category;

                    List<ProductVariant> productVariants = viewModel.productVariants;

                    List <CompositeProduct> compositeProducts= null;

                    ProductResponse productResponse = await _productService.SaveAsync(product);

                    if (productResponse != null && productResponse.Product != null && productResponse.Product.id > 0)
                    {
                        //TODO: Handle adding Composite Product

                        int productId = productResponse.Product.id;

                        try
                        {
                            if (productVariants != null)
                            {

                                int index = 0;
                                productVariants.ToList().ForEach(async (pv) =>
                                {
                                    productVariants[index].productId = productId;

                                    ProductVariantResponse productVariantResponse = await _productVariantService.SaveAsync(productVariants[index]);



                                    index++;
                                });


                            }
                        }
                        catch(Exception e)
                        {
                            TempData["Message"] = Utils.GenerateToastError("Product variant failed to create");
                        }


                        try
                        {

                            if(viewModel.compositeProductIds != null && viewModel.compositeProductIds.Count > 0)
                            {

                                compositeProducts = viewModel.compositeProductIds.Select(rid => new CompositeProduct
                                {
                                    productId = productId,
                                    relatedId = rid
                                }).ToList();

                                List<CompositeProductResponse> compositeProductResponses = await _compositeProductService.SaveRangeAsync(compositeProducts);

                            }

                        }
                        catch(Exception e)
                        {
                            TempData["Message"] = Utils.GenerateToastError("Composite product failed to create");
                        }


                        TempData["Message"] = Utils.GenerateToastSuccess("Product added successfully.");

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Message"] = Utils.GenerateToastError("Failed to create product");
                    }

                }


            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }

            try
            {
                IEnumerable<Category> categories = await _categoryService.ListAsync();

                ViewBag.Categories = categories;

            }
            catch (Exception e)
            {
                TempData["Message"] = Utils.GenerateToastError(e.Message);
                Utils.HandleException(e);
            }

            try
            {
                IEnumerable<Variant> variants = await _variantService.ListAsync();
                IEnumerable<VariantOption> variantOptions = await _variantOptionService.ListAsync();

                ProductListResponse products = await _productService.ListAsync();

                List<SelectListItem> relatedProducts = new List<SelectListItem>();

                if (products != null && products.items != null)
                {

                    relatedProducts = products.items.Select(p => new SelectListItem { Text = p.name, Value = $"{p.id}" }).ToList();


                }

                viewModel.variants = variants != null ? variants.ToList() : null;
                viewModel.variantOptions = variantOptions != null ? variantOptions.ToList() : null;
                viewModel.relatedProducts = relatedProducts;

            }
            catch(Exception e)
            {
                Utils.HandleException(e);
            }

            return View(viewModel);
        }

        // GET: Products/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            CreateProductViewModel createProductViewModel = null;
            Product product = null;

            try
            {

                


                product = await _productService.GetByIdAsync(id);

                if (product != null)
                {
                    IEnumerable<ProductVariant> variantsForProduct = await _productVariantService.GetAsync(id);
                    CompositeProductListResponse compositesForProduct = await _compositeProductService.GetAsync(id);

                    ViewBag.ProductVariants = variantsForProduct;
                    ViewBag.CompositeProducts = compositesForProduct.items;

                    createProductViewModel = _mapper.Map<Product, CreateProductViewModel>(product);

                    IEnumerable<Variant> variants = await _variantService.ListAsync();
                    IEnumerable<VariantOption> variantOptions = await _variantOptionService.ListAsync();

                    ProductListResponse products = await _productService.ListAsync();

                    List<SelectListItem> relatedProducts = new List<SelectListItem>();

                    if (products != null && products.items != null)
                    {

                        relatedProducts = products.items.Select(p => new SelectListItem { Text = p.name, Value = $"{p.id}" }).ToList();


                    }

                    //SKU sku = await _skuService.GetAsync(id);

                    createProductViewModel.sku = product.sku ?? await _skuService.GetAsync(id);

                    createProductViewModel.variants = variants != null ? variants.ToList() : null;
                    createProductViewModel.variantOptions = variantOptions != null ? variantOptions.ToList() : null;
                    createProductViewModel.relatedProducts = relatedProducts;

                    IEnumerable<Category> categories = await _categoryService.ListAsync();

                    ViewBag.Categories = categories;

                    return View(createProductViewModel);
                }


                TempData["Message"] = Utils.GenerateToastError($"Product with id: {id} not found");

            }
            catch (Exception e)
            {
                Utils.HandleException(e);

                TempData["Message"] = Utils.GenerateToastError(e.Message);
            }

            return RedirectToAction("Index");
        }

        // POST: Products/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(int id, CreateProductViewModel viewModel, FormCollection collection)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Product product = _mapper.Map<CreateProductViewModel, Product>(viewModel);

                    Product product1 = await _productService.GetByIdAsync(id);

                    if (product1 == null)
                    {
                        TempData["Message"] = Utils.GenerateToastError($"Product with id: {id} not found");
                        return RedirectToAction("Index");
                    }

                    product.sku = viewModel.sku;
                    product.category = viewModel.category;

                    List<ProductVariant> productVariants = viewModel.productVariants;

                    List<CompositeProduct> compositeProducts = null;

                    ProductResponse productResponse = await _productService.UpdateAsync(id, product);

                    if (productResponse != null && productResponse.Product != null && productResponse.Product.id > 0)
                    {
                        int productId = id;

                        

                        //TODO: Handle updating Composite Product
                        try
                        {

                            IEnumerable<ProductVariant> variantsForProduct = await _productVariantService.GetAsync(id);

                            if(variantsForProduct != null && variantsForProduct.Count() > 0)
                            {
                                foreach (var vo in variantsForProduct)
                                {
                                    ProductVariantResponse productVariantResponse = await _productVariantService.DeleteAsync(vo.id);
                                }
                            }

                            


                            if (productVariants != null)
                            {

                                int index = 0;
                                productVariants.ToList().ForEach(async (pv) =>
                                {
                                    productVariants[index].productId = productId;

                                    ProductVariantResponse productVariantResponse = await _productVariantService.SaveAsync(productVariants[index]);



                                    index++;
                                });


                            }
                        }
                        catch (Exception e)
                        {
                            TempData["Message"] = Utils.GenerateToastError("Product variant failed to update");
                        }


                        try
                        {

                            CompositeProductListResponse compositesForProduct = await _compositeProductService.GetAsync(id);

                            if(compositesForProduct != null && compositesForProduct.totalItems > 0)
                            {
                                foreach(var cp in compositesForProduct.items)
                                {
                                    CompositeProductResponse compositeProductResponse = await _compositeProductService.DeleteAsync(cp.id);
                                }
                            }


                            if (viewModel.compositeProductIds != null && viewModel.compositeProductIds.Count > 0)
                            {

                                compositeProducts = viewModel.compositeProductIds.Select(rid => new CompositeProduct
                                {
                                    productId = productId,
                                    relatedId = rid
                                }).ToList();

                                List<CompositeProductResponse> compositeProductResponses = await _compositeProductService.SaveRangeAsync(compositeProducts);

                            }

                        }
                        catch (Exception e)
                        {
                            TempData["Message"] = Utils.GenerateToastError("Composite product failed to update");
                        }


                        TempData["Message"] = Utils.GenerateToastSuccess("Product updated successfully.");

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["Message"] = Utils.GenerateToastError("Failed to edit product");
                    }


                    return RedirectToAction("Index");
                }
                

                
            }
            catch (Exception e)
            {
                Utils.HandleException(e);
                TempData["Message"] = Utils.GenerateToastError(e.Message);
                
            }


            try
            {

                IEnumerable<ProductVariant> variantsForProduct = await _productVariantService.GetAsync(id);
                CompositeProductListResponse compositesForProduct = await _compositeProductService.GetAsync(id);

                ViewBag.ProductVariants = variantsForProduct;
                ViewBag.CompositeProducts = compositesForProduct.items;


                IEnumerable<Variant> variants = await _variantService.ListAsync();
                IEnumerable<VariantOption> variantOptions = await _variantOptionService.ListAsync();

                ProductListResponse products = await _productService.ListAsync();

                List<SelectListItem> relatedProducts = new List<SelectListItem>();

                if (products != null && products.items != null)
                {

                    relatedProducts = products.items.Select(p => new SelectListItem { Text = p.name, Value = $"{p.id}" }).ToList();


                }


                viewModel.variants = variants != null ? variants.ToList() : null;
                viewModel.variantOptions = variantOptions != null ? variantOptions.ToList() : null;
                viewModel.relatedProducts = relatedProducts;

                IEnumerable<Category> categories = await _categoryService.ListAsync();

                ViewBag.Categories = categories;



                return View(viewModel);


            }
            catch(Exception e)
            {
                Utils.HandleException(e);

                TempData["Message"] = Utils.GenerateToastError(e.Message);

                return RedirectToAction("Index");
            }

        }

        // POST: Products/Delete/5
        public async Task<ActionResult> Delete(int id, FormCollection collection)
        {
            DefaultJsonResponse response = new DefaultJsonResponse
            {
                status = ResultCodes.FAIL,
                message = Configs.DefaultErrorMessage
            };

            try
            {

                ProductResponse product = await _productService.DeleteAsync(id);

                if (product != null && product.Product != null && product.Product.id > 0)
                {
                    response.status = ResultCodes.SUCCESS;
                    response.message = "Product deleted successfully";
                }

            }
            catch (Exception e)
            {
                response.message = e.Message;
                Utils.HandleException(e);
            }

            return Json(response, JsonRequestBehavior.AllowGet);
        }
    }
}
