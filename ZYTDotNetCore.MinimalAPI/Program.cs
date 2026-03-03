using Microsoft.EntityFrameworkCore;
using ZYTDotNetCore.Database.Models;
using ZYTDotNetCore.MinimalAPI.Endpoints.Blog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

//app.MapGet("/weatherforecast", () =>
//{
//    var forecast = Enumerable.Range(1, 5).Select(index =>
//        new WeatherForecast
//        (
//            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//            Random.Shared.Next(-20, 55),
//            summaries[Random.Shared.Next(summaries.Length)]
//        ))
//        .ToArray();
//    return forecast;
//})
//.WithName("GetWeatherForecast")
//.WithOpenApi();

//app.MapGet("/blogs",()=>
//{
//    AppDbContext db = new AppDbContext();
//    var item = db.TblBlogs.AsNoTracking().ToList();
//    return Results.Ok(item);
//})
//.WithName("GetBlogs")
//.WithOpenApi();
//app.MapGet("/blogs/{id}", (int id) =>
//{
//    AppDbContext db = new AppDbContext();
//    var item = db.TblBlogs
//                    .AsNoTracking()
//                    .FirstOrDefault(x => x.BlogId == id);
//    if (item is null)
//    {
//        return Results.BadRequest("Data not found");
//    }
//    return Results.Ok(item);
//})
// .WithName("GetBlogsById")
// .WithOpenApi();
//app.MapPost("/blogs",(TblBlog blog)=>
//{
//    AppDbContext db = new AppDbContext();
//    db.TblBlogs.Add(blog);
//    db.SaveChanges();
//    return Results.Ok("Record inserted successfully.");
//})
//.WithName("CreateBlogs")
//.WithOpenApi();
//app.MapPut("/blogs/{id}", (int id,TblBlog blog) =>
//{
//    AppDbContext db = new AppDbContext();
//    var item = db.TblBlogs
//                    .AsNoTracking()
//                    .FirstOrDefault(x => x.BlogId == id);
//    if (item is null)
//    {
//        return Results.BadRequest("Data not found.");
//    }
//    item.BlogTitle = blog.BlogTitle;
//    item.BlogAuthor = blog.BlogAuthor;
//    item.BlogContent = blog.BlogContent;
//    db.Entry(item).State = EntityState.Modified;
//    db.SaveChanges();
//    return Results.Ok("Record updated successfully.");
//})
//.WithName("UpdateBlogs")
//.WithOpenApi();
//app.MapPatch("/blogs/{id}", (TblBlog blog,int id) =>
//{
//    AppDbContext db = new AppDbContext();
//    var item = db.TblBlogs
//                    .AsNoTracking()
//                    .FirstOrDefault(x => x.BlogId == id);
//    if (item is null)
//    {
//        return Results.BadRequest("Data not found.");
//    }
//    if (blog.BlogTitle is not null)
//    {
//        item.BlogTitle = blog.BlogTitle;
//    }
//    if (blog.BlogAuthor is not null)
//    {
//        item.BlogAuthor = blog.BlogAuthor;
//    }
//    if (blog.BlogContent is not null)
//    {
//        item.BlogContent = blog.BlogContent;
//    }
//    db.Entry(item).State = EntityState.Modified;
//    db.SaveChanges();
//    return Results.Ok("Record updated successfully.");
//})
//    .WithName("PatchBlogs")
//    .WithOpenApi();
//app.MapDelete("/blogs/{id}",(int id) =>
//{
//    AppDbContext db = new AppDbContext();
//    var item = db.TblBlogs
//                    .AsNoTracking()
//                    .FirstOrDefault(x => x.BlogId == id);
//    if (item is null)
//    {
//        return Results.BadRequest("Data not found.");
//    }
//    item.DeleteFlage = true;
//    db.Entry(item).State = EntityState.Modified;
//    db.SaveChanges();
//    return Results.Ok("Record deleted successfully.");
//})
//.WithName("DeleteBlogs")
//.WithOpenApi();
app.MapBlogEndpoint();
app.Run();

//BLOG_ENDPOINT.Test(9);

//internal record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
//{
//    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
//}
