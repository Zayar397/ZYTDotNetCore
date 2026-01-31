using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using Microsoft.EntityFrameworkCore;
using ZYTDotNetCore.ConsoleApp.Models;

namespace ZYTDotNetCore.ConsoleApp
{
    public class EFCORE
    {
        public void READ()
        {
            DotNetTrainingBatch5DbContext db = new DotNetTrainingBatch5DbContext();
            var blogList = db.Blogs.Where(x => x.DeleteFlage == false).ToList();
            foreach (var item in blogList)
            {
                Console.WriteLine("Blog Id: " + item.BlogId);
                Console.WriteLine("Blog Title: " + item.BlogTitle);
                Console.WriteLine("Blog Author: " + item.BlogAuthor);
                Console.WriteLine("Blog Content: " + item.BlogContent);
                Console.WriteLine();
            }
        }
        public void CREATE(string title, string author, string content)
        {
            BlogDataModel blogDataModel = new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            DotNetTrainingBatch5DbContext db = new DotNetTrainingBatch5DbContext();
            db.Blogs.Add(blogDataModel);
            int recordCount = db.SaveChanges();
            Console.WriteLine(recordCount == 1 ? "Record inserted successful" : "Record inserted fail");
        }
        public void EDIT(int blogId)
        {
            DotNetTrainingBatch5DbContext db = new DotNetTrainingBatch5DbContext();
            var item = db.Blogs.Where(x => x.BlogId == blogId).FirstOrDefault();
            if (item == null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            Console.WriteLine("Blog ID: " + item.BlogId);
            Console.WriteLine("Blog Title: " + item.BlogTitle);
            Console.WriteLine("Blog Author: " + item.BlogAuthor);
            Console.WriteLine("Blog Content: " + item.BlogContent);
            Console.WriteLine();
        }
        public void UPDATE(int blogId, string blogTitle, string blogAuthor, string blogContent)
        {
            DotNetTrainingBatch5DbContext db = new DotNetTrainingBatch5DbContext();
            var item = db.Blogs.
                        AsNoTracking().
                        FirstOrDefault(x => x.BlogId == blogId);
            if (item == null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            if (!string.IsNullOrEmpty(blogTitle))
            {
                item.BlogTitle = blogTitle;
            }
            if (!string.IsNullOrEmpty(blogAuthor))
            {
                item.BlogAuthor = blogAuthor;
            }
            if (!string.IsNullOrEmpty(blogContent))
            {
                item.BlogContent = blogContent;
            }
            db.Entry(item).State = EntityState.Modified;
            int recordCount = db.SaveChanges();
            Console.WriteLine(recordCount == 1 ? "Record updated successful" : "Record updated fail");
 
        }
        public void DELETE(int blogId)
        {
            DotNetTrainingBatch5DbContext db = new DotNetTrainingBatch5DbContext();
            var item = db.Blogs
                        .AsNoTracking()
                        .FirstOrDefault(x => x.BlogId == blogId);
            if (item == null)
            {
                Console.WriteLine("No data found.");
                return;
            }
            db.Entry(item).State = EntityState.Deleted;
            int recordCount = db.SaveChanges();
            Console.WriteLine(recordCount == 1 ? "Record deleted successful" : "Record deleted fail");

        }
    }
}
