using AutoMapper;
using supermarketFrontEnd.Communication.Product;
using supermarketFrontEnd.Helpers;
using supermarketFrontEnd.Models;
using supermarketFrontEnd.Models.Domain.Services;
using supermarketFrontEnd.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace supermarketFrontEnd.Services
{
    public class ProductService : IProductService
    {
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ProductResponse> DeleteAsync(int id)
        {
            string endpoint = $"{Configs.Endpoints.products_delete}{id}";


            ProductResponse response = new ProductResponse();

            try
            {

                Task<HttpContent> productResponse = APICall.DELETE(endpoint);

                Product product = await productResponse.Result.ReadAsAsync<Product>();


                APIError error = new APIError();
                //Product product = _mapper.Map<dynamic, Product>(objectResponse);

                if (product == null || product.name == null)
                {

                    if (!error.success)
                    {
                        response.success = false;
                        response.messages = error.messages;
                    }
                }
                else
                {
                    response.success = true;
                    response.messages = null;
                    response.Product = product;
                }

                return response;


            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }
            return null;
        }

        public async Task<Product> GetByIdAsync(int productId)
        {
            try
            {

                ProductListResponse productListResponse = await ListAsync();

                if(productListResponse != null && productListResponse.items != null)
                {
                    return productListResponse.items[0];
                }


            }
            catch(Exception e)
            {
                Utils.HandleException(e);
            }

            return null;
        }

        public async Task<ProductListResponse> ListAsync(int categoryId = 0)
        {
            try
            {


                string endpoint = $"{Configs.Endpoints.products_list}{categoryId}";

                Task<HttpContent> productsResult = APICall.GET(endpoint);

                ProductListResponse products = await productsResult.Result.ReadAsAsync<ProductListResponse>();

                return products;
            }
            catch (Exception e)
            {
                Utils.HandleException(e);

                return null;
            }
        }

        public async Task<ProductResponse> SaveAsync(Product product)
        {
            string endpoint = Configs.Endpoints.products_save;

            try
            {
                ProductResponse response = new ProductResponse();

                var saveCat = new
                {
                    name = product.name,
                    categoryId = product.category != null ? product.category.id : 0,
                    saveProductSKUResource = new
                    {
                        unitOfMeasure = product.sku != null ? product.sku.unitOfMeasure : "",
                        unitPrice = product.sku != null ? Convert.ToDecimal(product.sku.unitPrice) : 0,
                        quantity = product.sku != null ? product.sku.quantity : ""
                    }
                };

                Task<HttpContent> productsResult = APICall.POST(endpoint, saveCat);

                Product saveProduct = await productsResult.Result.ReadAsAsync<Product>();

                if (saveProduct == null || saveProduct.name == null)
                {
                    APIError error = new APIError();

                    if (!error.success)
                    {
                        response.success = false;
                        response.messages = error.messages;
                    }
                }
                else
                {
                    response.success = true;
                    response.messages = null;
                    response.Product = saveProduct;
                }

                return response;

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }

            return null;
        }

        public async Task<ProductResponse> UpdateAsync(int id, Product product)
        {
            string endpoint = $"{Configs.Endpoints.products_update}{id}";


            ProductResponse response = new ProductResponse();

            try
            {

                var saveProductResource = new {
                    name = product.name,
                    categoryId = product.category != null ? product.category.id : 0,
                    saveProductSKUResource = new
                    {
                        unitOfMeasure = product.sku != null ? product.sku.unitOfMeasure : "",
                        unitPrice = product.sku != null ? Convert.ToDecimal(product.sku.unitPrice) : 0,
                        quantity = product.sku != null ? product.sku.quantity : ""
                    }
                };

                Task<HttpContent> productResponse = APICall.PUT(endpoint, saveProductResource);

                Product updatedProduct = await productResponse.Result.ReadAsAsync<Product>();

                if (updatedProduct == null || updatedProduct.name == null)
                {
                    APIError error = new APIError();

                    if (!error.success)
                    {
                        response.success = false;
                        response.messages = error.messages;
                    }
                }
                else
                {
                    response.success = true;
                    response.messages = null;
                    response.Product = updatedProduct;
                }

                return response;


            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }
            return null;
        }
    }
}