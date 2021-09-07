using AutoMapper;
using supermarketFrontEnd.Communication.SKU;
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
    public class SKUService : ISKUService
    {
        private readonly IMapper _mapper;

        public SKUService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<SKUResponse> DeleteAsync(int id)
        {
            string endpoint = $"{Configs.Endpoints.skus_delete}{id}";


            SKUResponse response = new SKUResponse();

            try
            {

                Task<HttpContent> skuResponse = APICall.DELETE(endpoint);

                SKU sku = await skuResponse.Result.ReadAsAsync<SKU>();


                APIError error = new APIError();
                //SKU sku = _mapper.Map<dynamic, SKU>(objectResponse);

                if (sku == null || sku.unitOfMeasure == null)
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
                    response.SKU = sku;
                }

                return response;


            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }
            return null;
        }

        public async Task<SKU> GetAsync(int productId)
        {
            try
            {

                string endpoint = $"{Configs.Endpoints.skus_list}{productId}";

                Task<HttpContent> skusResult = APICall.GET(endpoint);

                SKU sku = await skusResult.Result.ReadAsAsync<SKU>();

                return sku;
            }
            catch (Exception e)
            {
                Utils.HandleException(e);

                return null;
            }
        }

        public async Task<SKUResponse> SaveAsync(SKU sku)
        {
            string endpoint = Configs.Endpoints.skus_save;

            try
            {
                SKUResponse response = new SKUResponse();

                var saveCat = new SaveSKUResource
                {
                    quantity = sku.quantity,
                    productId = sku.productId,
                    unitOfMeasure = sku.unitOfMeasure,
                    unitPrice = sku.unitPrice
                };

                Task<HttpContent> skusResult = APICall.POST(endpoint, saveCat);

                SKU saveSKU = await skusResult.Result.ReadAsAsync<SKU>();

                if (saveSKU == null || saveSKU.unitOfMeasure == null)
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
                    response.SKU = saveSKU;
                }

                return response;

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }

            throw new NotImplementedException();
        }

        public async Task<SKUResponse> UpdateAsync(int id, SKU sku)
        {
            string endpoint = $"{Configs.Endpoints.skus_update}{id}";


            SKUResponse response = new SKUResponse();

            try
            {

                SaveSKUResource saveSKUResource = new SaveSKUResource
                {
                    quantity = sku.quantity,
                    productId = sku.productId,
                    unitOfMeasure = sku.unitOfMeasure,
                    unitPrice = sku.unitPrice,
                };

                Task<HttpContent> skuResponse = APICall.PUT(endpoint, saveSKUResource);

                SKU updatedSKU = await skuResponse.Result.ReadAsAsync<SKU>();

                if (updatedSKU == null || updatedSKU.unitOfMeasure == null)
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
                    response.SKU = updatedSKU;
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