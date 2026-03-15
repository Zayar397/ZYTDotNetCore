using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ZYTDotNetCore.Database.Models;

namespace ZYTDotNetCore.Domain.Features.Blog
{
    public class BLOG_SERVICE
    {
        private readonly AppDbContext _db = new AppDbContext();
        public List<TblBlog> GetBlogs()
        {
            var blogList = _db.TblBlogs.AsNoTracking().ToList();
            return blogList;
        }
        public TblBlog GetBlogById(int blogId)
        {
            var item = _db.TblBlogs
                            .AsNoTracking()
                            .FirstOrDefault(x => x.BlogId == blogId);
            if (item is null)
            {
                return null;
            }
            return item;
        }
        public bool CreateBlog(TblBlog blog)
        {
            _db.TblBlogs.Add(blog);
            int recordCount = _db.SaveChanges();
            return recordCount > 0;
        }

        public bool UpdateBlog(int id, TblBlog blog)
        {
            var item = _db.TblBlogs
                            .AsNoTracking()
                            .FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return false;
            }
            item.BlogTitle = blog.BlogTitle;
            item.BlogAuthor = blog.BlogAuthor;
            item.BlogContent = blog.BlogContent;
            _db.Entry(item).State = EntityState.Modified;
            int recordCount = _db.SaveChanges();
            return recordCount > 0;
        }
        public bool UpdateBlogWithPatch(int id, TblBlog blog)
        {
            var item = _db.TblBlogs
                            .AsNoTracking()
                            .FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return false;
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
            _db.Entry(item).State = EntityState.Modified;
            int recordCount = _db.SaveChanges();
            return recordCount > 0;
        }
        public bool DeleteBlog(int id)
        {
            var item = _db.TblBlogs
                            .AsNoTracking()
                            .FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                return false;
            }
            item.DeleteFlage = true;
            _db.Entry(item).State = EntityState.Modified;
            int recordCount = _db.SaveChanges();
            return recordCount > 0;
        }
    }
}
