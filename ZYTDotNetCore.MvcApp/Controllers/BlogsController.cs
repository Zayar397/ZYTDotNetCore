using Microsoft.AspNetCore.Mvc;
using ZYTDotNetCore.Domain.Features.Blog;
using ZYTDotNetCore.MvcApp.Models;

namespace ZYTDotNetCore.MvcApp.Controllers
{
    public class BlogsController : Controller
    {
        private readonly IBLOG_SERVICE _blogService;

        public BlogsController(IBLOG_SERVICE blogService)
        {
            _blogService = blogService;
        }

        public IActionResult Index()
        {
            var blogList = _blogService.GetBlogs();
            return View(blogList);
        }
        [ActionName("Create")]
        public IActionResult BlogCreate()
        {
            return View("BlogCreate");
        }
        [HttpPost]
        [ActionName("Save")]
        public IActionResult BlogSave(BlogRequestModel model)
        {
            try
            {
                _blogService.CreateBlog(new Database.Models.TblBlog
                {
                    BlogTitle = model.Title,
                    BlogAuthor = model.Author,
                    BlogContent = model.Content
                });

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Record created successfully.";
            }
            catch(Exception ex)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = ex.ToString();
            }
            return RedirectToAction("Index");
        }
        [ActionName("Delete")]
        public IActionResult BlogDelete(int id)
        {
            try
            {
                _blogService.DeleteBlog(id);
                TempData["IsSuccess"] = true;
                TempData["Message"] = "Record deleted successfully.";
            }
            catch(Exception ex)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = ex.ToString();
            }
            return RedirectToAction("Index");
        }
        [ActionName("Edit")]
        public IActionResult BlogEdit(int id)
        {
            var blogItem = _blogService.GetBlogById(id);
            BlogRequestModel model = new BlogRequestModel
            {
                Id = blogItem.BlogId,
                Title = blogItem.BlogTitle,
                Author = blogItem.BlogAuthor,
                Content = blogItem.BlogContent
            };
            return View("BlogEdit",model);
        }
        [HttpPost]
        [ActionName("Update")]
        public IActionResult BlogUpdate(int id, BlogRequestModel model)
        {
            try
            {
                _blogService.UpdateBlog(id, new Database.Models.TblBlog
                {
                    BlogTitle = model.Title,
                    BlogAuthor = model.Author,
                    BlogContent = model.Content
                });

                TempData["IsSuccess"] = true;
                TempData["Message"] = "Record updated successfully.";
            }
            catch (Exception ex)
            {
                TempData["IsSuccess"] = false;
                TempData["Message"] = ex.ToString();
            }
            return RedirectToAction("Index");
        }
    }
}
