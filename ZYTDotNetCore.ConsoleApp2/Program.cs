using Newtonsoft.Json;

namespace ZYTDotNetCore.ConsoleApp2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var blogList = new BlogModel
            {
                Id = 1,
                Title = "Hello World",
                Author = "John Doe",
                Content = "This is my first blog post."
            };
            string blogJson = blogList.ToJson();
            Console.WriteLine(blogJson);
            Console.WriteLine();

            var blogJson_2 = @"
                            {
                              ""Id"": 1,
                              ""Title"": ""Hello World"",
                              ""Author"": ""John Doe"",
                              ""Content"": ""This is my first blog post.""
                            }";
            var blogStr = JsonConvert.DeserializeObject<BlogModel>(blogJson_2);
            Console.WriteLine("Id: "+blogStr.Id);
            Console.WriteLine("Title: " + blogStr.Title);
            Console.WriteLine("Author: " + blogStr.Author);
            Console.WriteLine("Content: " + blogStr.Content);
        }
    }
    public class BlogModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Content { get; set; }
    }
    public static class Extension
    {
        public static string ToJson(this object obj)
        {
            string json = JsonConvert.SerializeObject(obj, Formatting.Indented);
            return json;
        }
    }
}
