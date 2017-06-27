using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Plugin.Connectivity;

namespace Pos.Services
{
	public class Error
	{
		public int Code { get; set; }
		public string Message { get; set; }
	}

	public class ItemResult<T>
	{
		public int TotalCount { get; set; }
		public IEnumerable<T> Items { get; set; }
	}

	public class ApiResult<T>
	{
		public T Result { get; set; }
		public bool Success { get; set; }
		public Error Error { get; set; }
	}

	public class PosSDK
	{
		HttpClient client;

		public PosSDK()
		{
			client = new HttpClient();
			client.BaseAddress = new Uri("https://staging.p2shop.cn/");
		}

        public async Task<ApiResult<T>> CallAPI<T>(string path, dynamic param)
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

            var uri = "jan-api" + path + "?";
            StringBuilder sb = new StringBuilder();
            foreach (var p in param.GetType().GetProperties())
            {
                uri += $"{p.Name}={p.GetValue(param)}&";
            }

            try
            {                
                using (HttpResponseMessage response = await client.GetAsync(uri))
                using (HttpContent content = response.Content)
                {
                    string json = await content.ReadAsStringAsync();
                    return await Task.Run(() => JsonConvert.DeserializeObject<ApiResult<T>>(json));
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
