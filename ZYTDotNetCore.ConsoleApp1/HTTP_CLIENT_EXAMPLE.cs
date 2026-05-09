using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ZYTDotNetCore.ConsoleApp1
{
    public class HTTP_CLIENT_EXAMPLE
    {
        private readonly HttpClient _client;
        private readonly string _postEndpoint = "https://jsonplaceholder.typicode.com/posts";
        public HTTP_CLIENT_EXAMPLE()
        {
            _client = new HttpClient();
        }
        public async Task Read()
        {
            var response = await _client.GetAsync(_postEndpoint);
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }
        public async Task Edit(int id)
        {
            var response = await _client.GetAsync($"{_postEndpoint}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found.");
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
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
            string jsonStr = JsonConvert.SerializeObject(postModel);
            StringContent content = new StringContent(jsonStr,Encoding.UTF8,"application/json");
            var response = await _client.PostAsync(_postEndpoint,content);
            if (response.IsSuccessStatusCode)
            {
                string contentStr = await response.Content.ReadAsStringAsync();
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
            string jsonStr = JsonConvert.SerializeObject(postModel);
            StringContent content = new StringContent (jsonStr,Encoding.UTF8,"application/json");
            var response  = await _client.PutAsync($"{_postEndpoint}/{id}",content);
            if (response.IsSuccessStatusCode)
            {
                string httpStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(httpStr);
            }
        }
        public async Task Delete(int id)
        {
            var response = await _client.DeleteAsync($"{_postEndpoint}/{id}");
            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                Console.WriteLine("No data found.");
                return;
            }
            if (response.IsSuccessStatusCode)
            {
                string jsonStr = await response.Content.ReadAsStringAsync();
                Console.WriteLine(jsonStr);
            }
        }
    }


    public class PostModel
    {
        public int userId { get; set; }
        public int id { get; set; }
        public string title { get; set; }
        public string body { get; set; }
    }

}
