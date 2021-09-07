using AutoMapper;
using supermarketFrontEnd.Communication.ProductVariant;
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
    public class ProductVariantService : IProductVariantService
    {
        private readonly IMapper _mapper;

        public ProductVariantService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ProductVariantResponse> DeleteAsync(int id)
        {
            string endpoint = $"{Configs.Endpoints.productVariants_delete}{id}";


            ProductVariantResponse response = new ProductVariantResponse();

            try
            {

                Task<HttpContent> productVariantResponse = APICall.DELETE(endpoint);

                ProductVariant productVariant = await productVariantResponse.Result.ReadAsAsync<ProductVariant>();


                APIError error = new APIError();
                //ProductVariant productVariant = _mapper.Map<dynamic, ProductVariant>(objectResponse);

                if (productVariant == null || productVariant.variantOptionId == null)
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
                    response.ProductVariant = productVariant;
                }

                return response;


            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }
            return null;
        }

        public async Task<IEnumerable<ProductVariant>> GetAsync(int productId)
        {
            try
            {

                string endpoint = Configs.Endpoints.productVariants_list;

                Task<HttpContent> productVariantsResult = APICall.GET(endpoint);

                List<ProductVariant> productVariants = await productVariantsResult.Result.ReadAsAsync<List<ProductVariant>>();

                return productVariants != null ? productVariants.Where(x => x.productId == productId).ToList() : null;
            }
            catch (Exception e)
            {
                Utils.HandleException(e);

                return null;
            }
        }

        public async Task<ProductVariantResponse> SaveAsync(ProductVariant productVariant)
        {
            string endpoint = Configs.Endpoints.productVariants_save;

            try
            {
                ProductVariantResponse response = new ProductVariantResponse();

                var saveCat = new SaveProductVariantResource
                {
                    productId = productVariant.productId,
                    variantId = productVariant.variantId,
                    variantOptionId = productVariant.variantOptionId,
                };

                Task<HttpContent> productVariantsResult = APICall.POST(endpoint, saveCat);

                ProductVariant saveProductVariant = await productVariantsResult.Result.ReadAsAsync<ProductVariant>();

                if (saveProductVariant == null || saveProductVariant.variantOptionId == null)
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
                    response.ProductVariant = saveProductVariant;
                }

                return response;

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }

            return null;
        }

        public async Task<ProductVariantResponse> UpdateAsync(int id, ProductVariant productVariant)
        {
            string endpoint = $"{Configs.Endpoints.productVariants_update}{id}";


            ProductVariantResponse response = new ProductVariantResponse();

            try
            {

                SaveProductVariantResource saveProductVariantResource = new SaveProductVariantResource
                {
                    productId = productVariant.productId,
                    variantId = productVariant.variantId,
                    variantOptionId = productVariant.variantOptionId,
                };

                Task<HttpContent> productVariantResponse = APICall.PUT(endpoint, saveProductVariantResource);

                ProductVariant updatedProductVariant = await productVariantResponse.Result.ReadAsAsync<ProductVariant>();

                if (updatedProductVariant == null || updatedProductVariant.variantOptionId == null)
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
                    response.ProductVariant = updatedProductVariant;
                }

                return response;


            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }
            return null;
        }

        public async Task<List<ProductVariantResponse>> SaveRangeAsync(List<ProductVariant> productVariants)
        {
            List<ProductVariantResponse> productVariantResponses = new List<ProductVariantResponse>();
            foreach(var pv in productVariants)
            {
                try
                {
                    ProductVariantResponse productVariantResponse = await SaveAsync(pv);

                    if (productVariantResponse != null)
                        productVariantResponses.Add(productVariantResponse);
                }
                catch(Exception e)
                {
                    Utils.HandleException(e);
                }
                               
            }

            return productVariantResponses;
        }

    }
}