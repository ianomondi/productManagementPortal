using AutoMapper;
using supermarketFrontEnd.Communication.VariantOption;
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
    public class VariantOptionService : IVariantOptionService
    {
        private readonly IMapper _mapper;

        public VariantOptionService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<VariantOptionResponse> DeleteAsync(int id)
        {
            string endpoint = $"{Configs.Endpoints.variantoptions_delete}{id}";


            VariantOptionResponse response = new VariantOptionResponse();

            try
            {

                Task<HttpContent> variantOptionResponse = APICall.DELETE(endpoint);

                VariantOption variantOption = await variantOptionResponse.Result.ReadAsAsync<VariantOption>();


                APIError error = new APIError();
                //VariantOption variantOption = _mapper.Map<dynamic, VariantOption>(objectResponse);

                if (variantOption == null || variantOption.value == null)
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
                    response.VariantOption = variantOption;
                }

                return response;


            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }
            return null;
        }

        public async Task<IEnumerable<VariantOption>> ListAsync()
        {
            try
            {

                string endpoint = Configs.Endpoints.variantoptions_list;

                Task<HttpContent> variantoptionsResult = APICall.GET(endpoint);

                List<VariantOption> variantoptions = await variantoptionsResult.Result.ReadAsAsync<List<VariantOption>>();

                return variantoptions;
            }
            catch (Exception e)
            {
                Utils.HandleException(e);

                return null;
            }

        }

        public async Task<VariantOptionResponse> SaveAsync(VariantOption variantOption)
        {
            string endpoint = Configs.Endpoints.variantoptions_save;

            try
            {
                VariantOptionResponse response = new VariantOptionResponse();

                var saveCat = new SaveVariantOptionResource { variantId = variantOption.variantId, value = variantOption.value };

                Task<HttpContent> variantoptionsResult = APICall.POST(endpoint, saveCat);

                VariantOption saveVariantOption = await variantoptionsResult.Result.ReadAsAsync<VariantOption>();

                if (saveVariantOption == null || saveVariantOption.value == null)
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
                    response.VariantOption = saveVariantOption;
                }

                return response;

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }

            throw new NotImplementedException();
        }

        public async Task<VariantOptionResponse> UpdateAsync(int id, VariantOption variantOption)
        {
            string endpoint = $"{Configs.Endpoints.variantoptions_update}{id}";


            VariantOptionResponse response = new VariantOptionResponse();

            try
            {

                SaveVariantOptionResource saveVariantOptionResource = new SaveVariantOptionResource
                {
                    variantId = variantOption.variantId,
                    value = variantOption.value
                };

                Task<HttpContent> variantOptionResponse = APICall.PUT(endpoint, saveVariantOptionResource);

                VariantOption updatedVariantOption = await variantOptionResponse.Result.ReadAsAsync<VariantOption>();

                if (updatedVariantOption == null || updatedVariantOption.value == null)
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
                    response.VariantOption = updatedVariantOption;
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