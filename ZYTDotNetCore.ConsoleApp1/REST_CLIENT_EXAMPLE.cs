using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace ZYTDotNetCore.ConsoleApp1
{
    public class REST_CLIENT_EXAMPLE
    {
        private readonly RestClient _client;
        private readonly string _postEndpoint = "https://jsonplaceholder.typicode.com/posts";
        public REST_CLIENT_EXAMPLE()
        {
            _client = new RestClient();
        }
        public async Task Read()
        {
            RestRequest request = new RestRequest(_postEndpoint,Method.Get);
            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                Console.WriteLine(jsonStr);
            }
        }
        public async Task Edit(int id)
        {
            RestRequest request = new RestRequest($"{_postEndpoint}/{id}", Method.Get);
            var response = await _client.ExecuteAsync(request);
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found.");
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                Console.WriteLine(jsonStr);
            }
        }
        public async Task Create(int userId, string title, string body)
        {
            PostModel postModel = new PostModel
            {
                userId = userId,
                title = title,
                body = body
            };
            RestRequest request = new RestRequest(_postEndpoint,Method.Post);
            request.AddJsonBody(postModel);

            //string jsonStr = JsonConvert.SerializeObject(postModel);
            //StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string contentStr = response.Content!;
                Console.WriteLine(contentStr);
            }
        }

        public async Task Update(int id, int userId, string title, string body)
        {
            PostModel postModel = new PostModel
            {
                id = id,
                userId = userId,
                title = title,
                body = body
            };
            RestRequest request = new RestRequest(_postEndpoint,Method.Put);
            request.AddJsonBody(postModel);

            //string jsonStr = JsonConvert.SerializeObject(postModel);
            //StringContent content = new StringContent(jsonStr, Encoding.UTF8, "application/json");

            var response = await _client.ExecuteAsync(request);
            if (response.IsSuccessStatusCode)
            {
                string httpStr = response.Content!;
                Console.WriteLine(httpStr);
            }
        }
        public async Task Delete(int id)
        {
            RestRequest request = new RestRequest($"{_postEndpoint}/{id}",Method.Delete);
            var response = await _client.ExecuteAsync(request);

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found.");
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = response.Content!;
                Console.WriteLine(jsonStr);
            }
        }
    }
}
