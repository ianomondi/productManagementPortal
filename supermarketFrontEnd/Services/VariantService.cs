using AutoMapper;
using supermarketFrontEnd.Communication.Variant;
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
    public class VariantService : IVariantService
    {
        private readonly IMapper _mapper;

        public VariantService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<VariantResponse> DeleteAsync(int id)
        {
            string endpoint = $"{Configs.Endpoints.variants_delete}{id}";


            VariantResponse response = new VariantResponse();

            try
            {

                Task<HttpContent> variantResponse = APICall.DELETE(endpoint);

                Variant variant = await variantResponse.Result.ReadAsAsync<Variant>();


                APIError error = new APIError();
                //Variant variant = _mapper.Map<dynamic, Variant>(objectResponse);

                if (variant == null || variant.name == null)
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
                    response.Variant = variant;
                }

                return response;


            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }
            return null;
        }

        public async Task<IEnumerable<Variant>> ListAsync()
        {
            try
            {

                string endpoint = Configs.Endpoints.variants_list;

                Task<HttpContent> categoriesResult = APICall.GET(endpoint);

                List<Variant> categories = await categoriesResult.Result.ReadAsAsync<List<Variant>>();

                return categories;
            }
            catch (Exception e)
            {
                Utils.HandleException(e);

                return null;
            }

        }

        public async Task<VariantResponse> SaveAsync(Variant variant)
        {
            string endpoint = Configs.Endpoints.variants_save;

            try
            {
                VariantResponse response = new VariantResponse();

                var saveCat = new SaveVariantResource{ 
                    name = variant.name,
                    frontEndName = variant.frontEndName,
                    displayName = variant.displayName
                };

                Task<HttpContent> categoriesResult = APICall.POST(endpoint, saveCat);

                Variant saveVariant = await categoriesResult.Result.ReadAsAsync<Variant>();

                if (saveVariant == null || saveVariant.name == null)
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
                    response.Variant = saveVariant;
                }

                return response;

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }

            throw new NotImplementedException();
        }

        public async Task<VariantResponse> UpdateAsync(int id, Variant variant)
        {
            string endpoint = $"{Configs.Endpoints.variants_update}{id}";


            VariantResponse response = new VariantResponse();

            try
            {

                SaveVariantResource saveVariantResource = new SaveVariantResource
                {
                    name = variant.name,
                    displayName = variant.displayName,
                    frontEndName = variant.frontEndName
                };

                Task<HttpContent> variantResponse = APICall.PUT(endpoint, saveVariantResource);

                Variant updatedVariant = await variantResponse.Result.ReadAsAsync<Variant>();

                if (updatedVariant == null || updatedVariant.name == null)
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
                    response.Variant = updatedVariant;
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