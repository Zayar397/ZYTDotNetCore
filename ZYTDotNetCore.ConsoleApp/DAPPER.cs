using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using ZYTDotNetCore.ConsoleApp.Models;

namespace ZYTDotNetCore.ConsoleApp
{
    public class DAPPER
    {
        private readonly string _connectionString = "Data Source = DESKTOP-BPF6HTF\\SQL2022; Initial Catalog = DotNetTrainingBatch5; User Id = sa; Password = p@ssw0rd;";
        public void READ()
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string selectQuery = "Select * from TBL_BLOG where DeleteFlage = 0";
                var list = db.Query<BlogDataModel>(selectQuery).ToList();
                foreach (var item in list)
                {
                    Console.WriteLine("Blog ID: " + item.BlogId);
                    Console.WriteLine("Blog Title: " + item.BlogTitle);
                    Console.WriteLine("Blog Author: " + item.BlogAuthor);
                    Console.WriteLine("Blog Content: " + item.BlogContent);
                    Console.WriteLine();
                }
            }
        }
        public void Edit(int blogId)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
                string selectQuery = "Select * from TBL_BLOG where DeleteFlage = 0 and BlogId = @blogId";
                var item = db.Query<BlogDataModel>(selectQuery, new BlogDataModel
                {
                    BlogId = blogId
                }).FirstOrDefault();
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
        }
        public void CREATE(string title, string author, string content)
        {
            using (IDbConnection db = new SqlConnection(_connectionString))
            {
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
                int recordCount = db.Execute(insertQuery, new BlogDataModel
                {
                    BlogTitle = title,
                    BlogAuthor = author,
                    BlogContent = content,
                });
                Console.WriteLine(recordCount == 1 ? "Record inserted successful" : "Record inserted fail");
            }
        }
    }
}
