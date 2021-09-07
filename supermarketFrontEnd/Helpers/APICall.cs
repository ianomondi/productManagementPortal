using supermarketFrontEnd.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;

namespace supermarketFrontEnd.Helpers
{
    public class APICall
    {
        private static HttpClient client;

        public static async Task<HttpContent> POST(string endpoint, object model)
        {
            try
            {
                string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                HttpContent data = new StringContent(serialized,System.Text.Encoding.UTF8,"application/json");

                return await sendApiRequest(ApiRequestTypes.POST, endpoint, data);

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
                return null;
            }
        }
        public static async Task<HttpContent> PUT(string endpoint, object model)
        {
            try
            {
                string serialized = Newtonsoft.Json.JsonConvert.SerializeObject(model);
                HttpContent data = new StringContent(serialized, System.Text.Encoding.UTF8, "application/json");

                return await sendApiRequest(ApiRequestTypes.PUT, endpoint, data);

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
                return null;
            }
        }
        public static async Task<HttpContent> GET(string endpoint)
        {
            try
            {
                return await sendApiRequest(ApiRequestTypes.GET, endpoint);

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
                return null;
            }
        }
        public static async Task<HttpContent> DELETE(string endpoint)
        {
            try
            {
                return await sendApiRequest(ApiRequestTypes.DELETE, endpoint);

            }
            catch (Exception e)
            {
                Utils.HandleException(e);
                return null;
            }
        }

        private static async Task<HttpContent> sendApiRequest(ApiRequestTypes type, string endpoint, HttpContent data = null)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(Configs.BASE_API_URL);

            try
            {
                Task<HttpResponseMessage> responseTask = null;

                if (type.Equals(ApiRequestTypes.POST))
                {

                    responseTask = client.PostAsync(endpoint, data);

                }
                else if (type.Equals(ApiRequestTypes.GET))
                {
                    responseTask = client.GetAsync(endpoint);
                }
                else if (type.Equals(ApiRequestTypes.PUT))
                {
                    responseTask = client.PutAsync(endpoint, data);
                }
                else
                {
                    responseTask = client.DeleteAsync(endpoint);
                }

                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    return responseTask.Result.Content;

                }
                else
                {
                    string reason = result.ReasonPhrase;
                    var code = result.StatusCode;

                    return responseTask.Result.Content;

                }


            }
            catch (Exception e)
            {
                Utils.HandleException(e);
            }

            return null;
        }
    }
}