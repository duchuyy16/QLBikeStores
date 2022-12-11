using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace QLBikeStores.Helpers
{
    public class Utilities
    {
        public static T SendDataRequest<T>(string APIUrl,object input=null)
        {
            HttpClient client = new();
            client.BaseAddress=new System.Uri("https://localhost:44306/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync(APIUrl, input).Result;
            T kq = default(T);
            if(response.IsSuccessStatusCode)
            {
                kq= response.Content.ReadFromJsonAsync<T>().Result;
            }
            return kq;
        }

        public static object SendDataRequest(string APIUrl, object input = null)
        {
            HttpClient client = new();
            client.BaseAddress = new System.Uri("https://localhost:44306/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.PostAsJsonAsync(APIUrl, input).Result;
            object kq = null;
            if (response.IsSuccessStatusCode)
            {
                kq = response.Content.ReadFromJsonAsync<object>().Result;
            }
            return kq;
        }
    }
}
