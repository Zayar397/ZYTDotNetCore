using Microsoft.AspNetCore.Mvc;
using ZYTDotNetCore.Domain.Features.Blog;

namespace ZYTDotNetCore.MinimalAPI.Endpoints.Blog
{
    public static class BLOG_SERVICE_ENDPOINT
    {
        public static void UseBlogServiceEndpoint(this IEndpointRouteBuilder app)
        {
            app.MapGet("/blogs", ([FromServices] IBLOG_SERVICE blogService) =>
            {
                //BLOG_SERVICE blogService = new BLOG_SERVICE();
                var blogList = blogService.GetBlogs();
                return Results.Ok(blogList);
            })
    .WithName("GetBlogs")
    .WithOpenApi();
            app.MapGet("/blogs/{id}", ([FromServices] IBLOG_SERVICE blogService, int id) =>
            {
                //BLOG_SERVICE blogService = new BLOG_SERVICE();
                var item = blogService.GetBlogById(id);
                if (item is null)
                {
                    return Results.BadRequest("Data not found");
                }
                return Results.Ok(item);
            })
             .WithName("GetBlogsById")
             .WithOpenApi();
            app.MapPost("/blogs", ([FromServices] IBLOG_SERVICE blogService, TblBlog blog) =>
            {
                //BLOG_SERVICE blogService = new BLOG_SERVICE();
                bool isOk = blogService.CreateBlog(blog);
                return Results.Ok(isOk == true ? "Successed to insert record." : "Failed to insert record.");
            })
            .WithName("CreateBlogs")
            .WithOpenApi();
            app.MapPut("/blogs/{id}", ([FromServices] IBLOG_SERVICE blogService, int id, TblBlog blog) =>
            {
                //BLOG_SERVICE blogService = new BLOG_SERVICE();
                bool isOk = blogService.UpdateBlog(id, blog);
                
                return Results.Ok(isOk == true ? "Successed to update record." : "Failed to update record.");
            })
            .WithName("UpdateBlogs")
            .WithOpenApi();
            app.MapPatch("/blogs/{id}", ([FromServices] IBLOG_SERVICE blogService, TblBlog blog, int id) =>
            {
                //BLOG_SERVICE blogService = new BLOG_SERVICE();
                bool isOk = blogService.UpdateBlogWithPatch(id, blog);
                return Results.Ok(isOk == true ? "Successed to update record." : "Failed to update record.");
            })
                .WithName("PatchBlogs")
                .WithOpenApi();
            app.MapDelete("/blogs/{id}", ([FromServices] IBLOG_SERVICE blogService, int id) =>
            {
                //BLOG_SERVICE blogService = new BLOG_SERVICE();
                bool isOk = blogService.DeleteBlog(id);
                return Results.Ok(isOk == true ? "Successed to delete record." : "Failed to delete record.");
            })
            .WithName("DeleteBlogs")
            .WithOpenApi();
        }
    }
}
