using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZYTDotNetCore.Database.Models;

namespace ZYTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : ControllerBase
    {
        private readonly AppDbContext _db = new AppDbContext();
        [HttpGet]
        public IActionResult GetBlogs()
        {
            var blogList = _db.TblBlogs.AsNoTracking().ToList();
            return Ok(blogList);
        }
        [HttpGet("{blogId}")]
        public IActionResult GetBlogs(int blogId)
        {
            var blogList = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == blogId);
            if (blogList is null)
            {
                return NotFound();
            }
            return Ok(blogList);
        }
        [HttpPost]
        public IActionResult CreateBlogs(TblBlog blog)
        {
            _db.TblBlogs.Add(blog);
            _db.SaveChanges();
            return Ok(new {Message = "Record inserted successfully. "+blog});
        }
        [HttpPut("{blogId}")]
        public IActionResult UpdateBlogs(int blogId, TblBlog blog)
        {
            var blogList = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == blogId);
            if (blogList is null)
            {
                return NotFound();
            }
            blogList.BlogTitle = blog.BlogTitle;
            blogList.BlogAuthor = blog.BlogAuthor;
            blogList.BlogContent = blog.BlogContent;
            return Ok(new {Message = "Record Updated Successfully. "+blogList});
        }
        [HttpPatch("{blogId}")]
        public IActionResult PatchBlogs(int blogId, TblBlog blog)
        {
            var blogList = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == blogId);
            if (blogList is null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                blogList.BlogTitle = blog.BlogTitle;
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                blogList.BlogAuthor = blog.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                blogList.BlogContent = blog.BlogContent;
            }
            _db.Entry(blogList).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(new { Message = "Record Updated Successfully(Patch)." });
        }
        [HttpDelete("{blogId}")]
        public IActionResult DeleteBlogs(int blogId)
        {
            var blogList = _db.TblBlogs.AsNoTracking().FirstOrDefault(x => x.BlogId == blogId);
            if (blogList is null)
            {
                return NotFound();
            }
            blogList.DeleteFlage = true;
            _db.Entry(blogList).State = EntityState.Modified;
            _db.SaveChanges();
            return Ok(new {Message = "Record deleted successfully."});
        }
    }
}
