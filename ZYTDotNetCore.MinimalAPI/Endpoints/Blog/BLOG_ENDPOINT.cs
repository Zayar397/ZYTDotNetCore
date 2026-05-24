//using Microsoft.EntityFrameworkCore;
//using ZYTDotNetCore.Database.Models;

using Microsoft.AspNetCore.Mvc;

namespace ZYTDotNetCore.MinimalAPI.Endpoints.Blog;

public static class BLOG_ENDPOINT
{
    public static void UseBlogEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/blogs", ([FromServices] AppDbContext db) =>
        {
            //AppDbContext db = new AppDbContext();
            var item = db.TblBlogs.AsNoTracking().ToList();
            return Results.Ok(item);
        })
.WithName("GetBlogs")
.WithOpenApi();
        app.MapGet("/blogs/{id}", ([FromServices] AppDbContext db, int id) =>
        {
            //AppDbContext db = new AppDbContext();
            var item = db.TblBlogs
                            .AsNoTracking()
                            .FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return Results.BadRequest("Data not found");
            }
            return Results.Ok(item);
        })
         .WithName("GetBlogsById")
         .WithOpenApi();
        app.MapPost("/blogs", ([FromServices] AppDbContext db, TblBlog blog) =>
        {
            //AppDbContext db = new AppDbContext();
            db.TblBlogs.Add(blog);
            db.SaveChanges();
            return Results.Ok("Record inserted successfully.");
        })
        .WithName("CreateBlogs")
        .WithOpenApi();
        app.MapPut("/blogs/{id}", ([FromServices] AppDbContext db, int id, TblBlog blog) =>
        {
            //AppDbContext db = new AppDbContext();
            var item = db.TblBlogs
                            .AsNoTracking()
                            .FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return Results.BadRequest("Data not found.");
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return Results.Ok("Record updated successfully.");
        })
        .WithName("UpdateBlogs")
        .WithOpenApi();
        app.MapPatch("/blogs/{id}", ([FromServices] AppDbContext db, TblBlog blog, int id) =>
        {
            //AppDbContext db = new AppDbContext();
            var item = db.TblBlogs
                            .AsNoTracking()
                            .FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return Results.BadRequest("Data not found.");
            }
            if (blog.BlogTitle is not null)
            {
                item.BlogTitle = blog.BlogTitle;
            }
            if (blog.BlogAuthor is not null)
            {
                item.BlogAuthor = blog.BlogAuthor;
            }
            if (blog.BlogContent is not null)
            {
                item.BlogContent = blog.BlogContent;
            }
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return Results.Ok("Record updated successfully.");
        })
            .WithName("PatchBlogs")
            .WithOpenApi();
        app.MapDelete("/blogs/{id}", ([FromServices] AppDbContext db, int id) =>
        {
            //AppDbContext db = new AppDbContext();
            var item = db.TblBlogs
                            .AsNoTracking()
                            .FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return Results.BadRequest("Data not found.");
            }
            item.DeleteFlage = true;
            db.Entry(item).State = EntityState.Modified;
            db.SaveChanges();
            return Results.Ok("Record deleted successfully.");
        })
        .WithName("DeleteBlogs")
        .WithOpenApi();
    }
}
