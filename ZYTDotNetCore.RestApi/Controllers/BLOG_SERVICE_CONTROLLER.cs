using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZYTDotNetCore.Database.Models;
using ZYTDotNetCore.Domain.Features.Blog;

namespace ZYTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BLOG_SERVICE_CONTROLLER : ControllerBase
    {
        private readonly BLOG_SERVICE blogService;
        public BLOG_SERVICE_CONTROLLER()
        {
            blogService = new BLOG_SERVICE();
        }
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var blogList = blogService.GetBlogs();
            return Ok(blogList);
        }
        [HttpGet("{blogId}")]
        public IActionResult GetBlogs(int blogId)
        {
            var blogItem = blogService.GetBlogById(blogId);
            if (blogItem is null)
            {
                return NotFound();
            }
            return Ok(blogItem);
        }
        [HttpPost]
        public IActionResult CreateBlogs(TblBlog blog)
        {
            bool isSuccess = blogService.CreateBlog(blog);
            return Ok(isSuccess == true ? "Successed to insert record." : "Failed to insert record.");
        }
        [HttpPut("{blogId}")]
        public IActionResult UpdateBlogs(int blogId, TblBlog blog)
        {
            bool isSuccess = blogService.UpdateBlog(blogId, blog);
            return Ok(isSuccess == true ? "Successed to update record." : "Failed to update record.");
        }
        [HttpPatch("{blogId}")]
        public IActionResult PatchBlogs(int blogId, TblBlog blog)
        {
            bool isSuccess = blogService.UpdateBlogWithPatch(blogId, blog);
            return Ok(isSuccess == true ? "Successed to update record with patch." : "Failed to update record with patch.");
        }
        [HttpDelete("{blogId}")]
        public IActionResult DeleteBlogs(int blogId)
        {
            bool isSuccess = blogService.DeleteBlog(blogId);
            return Ok(isSuccess == true ? "Successed to delete record." : "Failed to delete record.");
        }
    }
}
