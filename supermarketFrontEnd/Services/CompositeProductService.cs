using AutoMapper;
using supermarketFrontEnd.Communication.CompositeProduct;
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
    public class CompositeProductService : ICompositeProductService
    {
        private readonly IMapper _mapper;

        public CompositeProductService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<CompositeProductResponse> DeleteAsync(int id)
        {
            string endpoint = $"{Configs.Endpoints.compositeProducts_delete}{id}";


            CompositeProductResponse response = new CompositeProductResponse();

            try
            {

                Task<HttpContent> compositeProductResponse = APICall.DELETE(endpoint);

                CompositeProduct compositeProduct = await compositeProductResponse.Result.ReadAsAsync<CompositeProduct>();


                APIError error = new APIError();
                //CompositeProduct compositeProduct = _mapper.Map<dynamic, CompositeProduct>(objectResponse);

                if (compositeProduct == null || compositeProduct.relatedId == null)
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
                    response.CompositeProduct = compositeProduct;
                }

                return response;


            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }
            return null;
        }

        public async Task<CompositeProductListResponse> GetAsync(int productId)
        {
            try
            {

                string endpoint = $"{Configs.Endpoints.compositeProducts_list}{productId}";

                Task<HttpContent> compositeProductsResult = APICall.GET(endpoint);

                CompositeProductListResponse compositeProducts = await compositeProductsResult.Result.ReadAsAsync<CompositeProductListResponse>();

                return compositeProducts;
            }
            catch (Exception e)
            {
                Utils.HandleException(e);

                return null;
            }
        }

        public async Task<CompositeProductResponse> SaveAsync(CompositeProduct compositeProduct)
        {
            string endpoint = Configs.Endpoints.compositeProducts_save;

            try
            {
                CompositeProductResponse response = new CompositeProductResponse();

                var saveCat = new SaveCompositeProductResource
                {
                    productId = compositeProduct.productId,
                    relatedId = compositeProduct.relatedId
                };

                Task<HttpContent> compositeProductsResult = APICall.POST(endpoint, saveCat);

                CompositeProduct saveCompositeProduct = await compositeProductsResult.Result.ReadAsAsync<CompositeProduct>();

                if (saveCompositeProduct == null || saveCompositeProduct.relatedId == null)
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
                    response.CompositeProduct = saveCompositeProduct;
                }

                return response;

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }
            return null;
        }

        public async Task<List<CompositeProductResponse>> SaveRangeAsync(List<CompositeProduct> compositeProducts)
        {
            List<CompositeProductResponse> compositeProductResponses = new List<CompositeProductResponse>();
            foreach (var pv in compositeProducts)
            {
                try
                {
                    CompositeProductResponse compositeProductResponse = await SaveAsync(pv);

                    if (compositeProductResponse != null)
                        compositeProductResponses.Add(compositeProductResponse);
                }
                catch (Exception e)
                {
                    Utils.HandleException(e);
                }

            }

            return compositeProductResponses;
        }

        public async Task<CompositeProductResponse> UpdateAsync(int id, CompositeProduct compositeProduct)
        {
            string endpoint = $"{Configs.Endpoints.compositeProducts_update}{id}";


            CompositeProductResponse response = new CompositeProductResponse();

            try
            {

                SaveCompositeProductResource saveCompositeProductResource = new SaveCompositeProductResource
                {
                    productId = compositeProduct.productId,
                    relatedId = compositeProduct.relatedId
                };

                Task<HttpContent> compositeProductResponse = APICall.PUT(endpoint, saveCompositeProductResource);

                CompositeProduct updatedCompositeProduct = await compositeProductResponse.Result.ReadAsAsync<CompositeProduct>();

                if (updatedCompositeProduct == null || updatedCompositeProduct.relatedId == null)
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
                    response.CompositeProduct = updatedCompositeProduct;
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