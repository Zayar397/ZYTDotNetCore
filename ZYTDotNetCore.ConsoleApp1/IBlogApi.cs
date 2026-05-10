using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace ZYTDotNetCore.ConsoleApp1
{
    public interface IBlogApi
    {
        [Get("/api/Blogs")]
        Task<List<TblBlog>> GetBlogs();

        [Get("/api/Blogs/{blogId}")]
        Task<TblBlog> GetBlog(int blogId);
    }
    public class TblBlog
    {
        public int BlogId { get; set; }

        public string BlogTitle { get; set; } = null!;

        public string BlogAuthor { get; set; } = null!;

        public string BlogContent { get; set; } = null!;

        public bool DeleteFlage { get; set; }
    }
}
