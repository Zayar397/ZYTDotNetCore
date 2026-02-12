// See https://aka.ms/new-console-template for more information
using ZYTDotNetCore.Database.Models;

Console.WriteLine("Hello, World!");

AppDbContext db = new AppDbContext();
var blogList = db.TblBlogs.ToList();