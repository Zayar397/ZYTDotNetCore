using ZYTDotNetCore.Database.Models;

namespace ZYTDotNetCore.Domain.Features.Blog
{
    public interface IBLOG_SERVICE
    {
        bool CreateBlog(TblBlog blog);
        bool DeleteBlog(int id);
        TblBlog GetBlogById(int blogId);
        List<TblBlog> GetBlogs();
        bool UpdateBlog(int id, TblBlog blog);
        bool UpdateBlogWithPatch(int id, TblBlog blog);
    }
}