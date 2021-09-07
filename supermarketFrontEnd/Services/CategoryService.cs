using AutoMapper;
using supermarketFrontEnd.Communication;
using supermarketFrontEnd.Communication.Category;
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
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public async Task<CategoryResponse> DeleteAsync(int id)
        {
            string endpoint = $"{Configs.Endpoints.categories_delete}{id}";


            CategoryResponse response = new CategoryResponse();

            try
            {

                Task<HttpContent> categoryResponse = APICall.DELETE(endpoint);

                Category category = await categoryResponse.Result.ReadAsAsync<Category>();


                APIError error = new APIError();
                //Category category = _mapper.Map<dynamic, Category>(objectResponse);

                if (category == null || category.name == null)
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
                    response.Category = category;
                }

                return response;


            }catch(Exception e)
            {
                Utils.HandleException(e);
            }
            return null;
        }

        public async Task<IEnumerable<Category>> ListAsync()
        {
            try
            {

                string endpoint = Configs.Endpoints.categories_list;

                Task<HttpContent> categoriesResult = APICall.GET(endpoint);

                List<Category> categories = await categoriesResult.Result.ReadAsAsync<List<Category>>();

                return categories;
            }
            catch(Exception e)
            {
                Utils.HandleException(e);

                return null;
            }

        }

        public async Task<CategoryResponse> SaveAsync(Category category)
        {
            string endpoint = Configs.Endpoints.categories_save;

            try
            {
                CategoryResponse response = new CategoryResponse();

                var saveCat = new SaveCategoryResource { name = category.name };

                Task<HttpContent> categoriesResult = APICall.POST(endpoint, saveCat);

                Category saveCategory = await categoriesResult.Result.ReadAsAsync<Category>();

                if (saveCategory == null || saveCategory.name == null)
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
                    response.Category = saveCategory;
                }

                return response;

            }
            catch(Exception e)
            {
                Utils.HandleException(e);
            }

            throw new NotImplementedException();
        }

        public async Task<CategoryResponse> UpdateAsync(int id, Category category)
        {
            string endpoint = $"{Configs.Endpoints.categories_update}{id}";


            CategoryResponse response = new CategoryResponse();

            try
            {

                SaveCategoryResource saveCategoryResource = new SaveCategoryResource
                {
                    name = category.name
                };

                Task<HttpContent> categoryResponse = APICall.PUT(endpoint, saveCategoryResource);

                Category updatedCategory = await categoryResponse.Result.ReadAsAsync<Category>();

                if (updatedCategory == null || updatedCategory.name == null)
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
                    response.Category = updatedCategory;
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