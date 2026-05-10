using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace ZYTDotNetCore.ConsoleApp1
{
    public class REFIT_EXAMPLE
    {
        public async Task RunAsync()
        {
            Console.WriteLine("Get...");
            var blogApi = RestService.For<IBlogApi>("https://localhost:7160");
            var blogList = await blogApi.GetBlogs();
            foreach (var item in blogList)
            {
                Console.WriteLine("Author: " + item.BlogAuthor);
                Console.WriteLine("Title: " + item.BlogContent);
                Console.WriteLine("Content: " + item.BlogContent);
                Console.WriteLine();
            }

            Console.WriteLine("Get by id...");
            try
            {
                var blog = await blogApi.GetBlog(1000);
                Console.WriteLine("Author: " + blog.BlogAuthor);
                Console.WriteLine("Title: " + blog.BlogContent);
                Console.WriteLine("Content: " + blog.BlogContent);
                Console.WriteLine();
            }
            catch(ApiException ex)
            {
                if (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    Console.WriteLine("No data found.");
                }
            }
        }
    }
}
