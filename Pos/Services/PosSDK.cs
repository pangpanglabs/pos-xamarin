using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;
using Pos.Models;

namespace Pos.Services
{
	public class PosSDK
	{
		HttpClient client;

		public PosSDK()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri("https://staging.p2shop.cn/");
		}

        public async Task<ApiResult<T>> CallAPI<T>(string path, dynamic param = null)
        {
            if (CrossConnectivity.Current.IsConnected == false)
            {
                return new ApiResult<T>
                {
                    Success = false,
                    Error = new Error
                    {
                        Code = 109,
                        Message = "Network Error",
                    }
                };
            }

            var uri = "jan-api-retail" + path + "?";

            if (param != null)
            {
                foreach (var p in param.GetType().GetProperties())
                {
                    uri += $"{p.Name}={p.GetValue(param)}&";
                }
            }

            try
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", Settings.AuthToken);
                using (HttpResponseMessage response = await client.GetAsync(uri))
                using (HttpContent content = response.Content)
                {
                    string json = await content.ReadAsStringAsync();
                    var result = await Task.Run(() => JsonConvert.DeserializeObject<ApiResult<T>>(json));

                    if (path.StartsWith("/account/login"))
                    {
                        Settings.AuthToken = (result.Result as Account).Token;
                    }

                    return result;
                }
            }
            catch (Exception e)
            {
                return new ApiResult<T>
                {
                    Success = false,
                    Error = new Error
                    {
                        Code = 100,
                        Message = e.ToString(),
                    }
                };
            }
        }
	}
}
