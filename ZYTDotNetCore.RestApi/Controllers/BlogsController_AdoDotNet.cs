using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using ZYTDotNetCore.Database.Models;
using ZYTDotNetCore.RestApi.ViewModel;

namespace ZYTDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController_AdoDotNet : ControllerBase
    {
        private readonly string _connectionString = "Data Source = DESKTOP-BPF6HTF\\SQL2022; Initial Catalog = DotNetTrainingBatch5; User Id = sa; Password = p@ssw0rd; TrustServerCertificate = True;";
        [HttpGet]
        public IActionResult GetBlogs()
        {
            List<BlogViewModel> blogList = new List<BlogViewModel>();
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string selectQuery = @"SELECT [BlogId]
                                          ,[BlogTitle]
                                          ,[BlogAuthor]
                                          ,[BlogContent]
                                          ,[DeleteFlage]
                                      FROM [dbo].[TBL_BLOG]
                                      where DeleteFlage = 0";
            SqlCommand cmd = new SqlCommand(selectQuery, conn);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                blogList.Add(new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlage = Convert.ToBoolean(reader["DeleteFlage"])
                });
            }
            return Ok(blogList);
            conn.Close();
        }
        [HttpGet("{blogId}")]
        public IActionResult GetBlogs(int blogId)
        {
            List<BlogViewModel> blogList = new List<BlogViewModel>();
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string selectQuery = @"Select * from TBL_BLOG
                                    where BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(selectQuery, conn);
            cmd.Parameters.AddWithValue("@BlogId", blogId);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                blogList.Add(new BlogViewModel
                {
                    Id = Convert.ToInt32(reader["BlogId"]),
                    Title = Convert.ToString(reader["BlogTitle"]),
                    Author = Convert.ToString(reader["BlogAuthor"]),
                    Content = Convert.ToString(reader["BlogContent"]),
                    DeleteFlage = Convert.ToBoolean(reader["DeleteFlage"])
                });
            }
            return Ok(blogList);
            conn.Close();
        }
        [HttpPost]
        public IActionResult CreateBlogs(BlogViewModel blogViewModel)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string insertQuery = @"INSERT INTO [dbo].[TBL_BLOG]
                                           ([BlogTitle]
                                           ,[BlogAuthor]
                                           ,[BlogContent]
                                           ,[DeleteFlage])
                                     VALUES
                                           (@BlogTitle
                                           ,@BlogAuthor
                                           ,@BlogContent
                                           ,0)";
            SqlCommand cmd = new SqlCommand(insertQuery,conn);
            if (!string.IsNullOrEmpty(blogViewModel.Title))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blogViewModel.Title);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BlogTitle", "");
            }
            if (!string.IsNullOrEmpty(blogViewModel.Author))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blogViewModel.Author);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", "");
            }
            if (!string.IsNullOrEmpty(blogViewModel.Content))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blogViewModel.Content);
            }
            else
            {
                cmd.Parameters.AddWithValue("@BlogContent", "");
            }
            int recordCount = cmd.ExecuteNonQuery();
            conn.Close();
            return Ok(recordCount == 1 ? "Record inserted successfully." : "Record inserted failed.");
        }
        [HttpPut("{blogId}")]
        public IActionResult UpdateBlogs(int blogId, BlogViewModel blogViewModel)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string updateQuery = @"UPDATE [dbo].[TBL_BLOG]
                                       SET [BlogTitle] = @BlogTitle
                                          ,[BlogAuthor] = @BlogAuthor
                                          ,[BlogContent] = @BlogContent
                                          ,[DeleteFlage] = @DeleteFlage
                                     WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(updateQuery,conn);
            cmd.Parameters.AddWithValue("@BlogTitle",blogViewModel.Title);
            cmd.Parameters.AddWithValue("@BlogAuthor", blogViewModel.Author);
            cmd.Parameters.AddWithValue("@BlogContent", blogViewModel.Content);
            cmd.Parameters.AddWithValue("@DeleteFlage", blogViewModel.DeleteFlage);
            cmd.Parameters.AddWithValue("@BlogId",blogId);
            int recordCount = cmd.ExecuteNonQuery();
            conn.Close();
            return Ok(recordCount == 1 ? "Record updated successfully." : "Record updated failed.");
        }
        [HttpPatch("{blogId}")]
        public IActionResult PatchBlogs(int blogId, BlogViewModel blogViewModel)
        {
            string condition = "";
            if (!string.IsNullOrEmpty(blogViewModel.Title))
            {
                condition += " [BlogTitle] = @BlogTitle, ";
            }
            if (!string.IsNullOrEmpty(blogViewModel.Author))
            {
                condition += " [BlogAuthor] = @BlogAuthor, ";
            }
            if (!string.IsNullOrEmpty(blogViewModel.Content))
            {
                condition += " [BlogContent] = @BlogContent, ";
            }
            if (condition.Length == 0)
            {
                return BadRequest("Invalid parameters.");
            }
            condition = condition.Substring(0, condition.Length - 2);

            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string updateQuery = $@"UPDATE [dbo].[TBL_BLOG]
                                   SET {condition}
                                 WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(updateQuery, conn);
            if (!string.IsNullOrEmpty(blogViewModel.Title))
            {
                cmd.Parameters.AddWithValue("@BlogTitle", blogViewModel.Title);
            }
            if (!string.IsNullOrEmpty(blogViewModel.Author))
            {
                cmd.Parameters.AddWithValue("@BlogAuthor", blogViewModel.Author);
            }
            if (!string.IsNullOrEmpty(blogViewModel.Content))
            {
                cmd.Parameters.AddWithValue("@BlogContent", blogViewModel.Content);
            }
            cmd.Parameters.AddWithValue("@BlogId", blogId);
            int recordCount = cmd.ExecuteNonQuery();
            conn.Close();
            return Ok(recordCount > 0 ? "Record updated successfully." : "Record updated fail");
        }
        [HttpDelete("{blogId}")]
        public IActionResult DeleteBlogs(int blogId)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            string deleteQuery = @"UPDATE [dbo].[TBL_BLOG]
                                       SET [DeleteFlage] = 1
                                     WHERE BlogId = @BlogId";
            SqlCommand cmd = new SqlCommand(deleteQuery,conn);
            cmd.Parameters.AddWithValue("@BlogId", blogId);
            int recordCount = cmd.ExecuteNonQuery();
            conn.Close();
            return Ok(recordCount == 1 ? "Record deleted successfully." : "Record deleted failed.");
        }
    }
}
