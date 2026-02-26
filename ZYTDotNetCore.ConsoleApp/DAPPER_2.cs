using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZYTDotNetCore.ConsoleApp.Models;
using ZYTDotNetCore.Shared;

namespace ZYTDotNetCore.ConsoleApp
{
    public class DAPPER_2
    {
        private readonly string _connectionString= "Data Source = DESKTOP-BPF6HTF\\SQL2022; Initial Catalog = DotNetTrainingBatch5; User Id = sa; Password = p@ssw0rd;";
        private readonly DAPPER_SERVICE _dapperService;
        public DAPPER_2()
        {
            _dapperService = new DAPPER_SERVICE(_connectionString);
        }
        public void Read()
        {
            string selectQuery = "Select * from TBL_BLOG where DeleteFlage = 0";
            var list = _dapperService.Query<BlogDataModel>(selectQuery).ToList();
            foreach (var item in list)
            {
                Console.WriteLine("Blog ID: " + item.BlogId);
                Console.WriteLine("Blog Title: " + item.BlogTitle);
                Console.WriteLine("Blog Author: " + item.BlogAuthor);
                Console.WriteLine("Blog Content: " + item.BlogContent);
                Console.WriteLine();
            }
        }
        public void Edit(int blogId)
        {
            string selectQuery = "Select * from TBL_BLOG where DeleteFlage = 0 and BlogId = @blogId";
            var item = _dapperService.QueryFirstOrDefault<BlogDataModel>(selectQuery, new BlogDataModel
            {
                BlogId = blogId
            });
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

        public void Create(string title, string author, string content)
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
            int recordCount = _dapperService.Execute(insertQuery, new BlogDataModel
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content,
            });
            Console.WriteLine(recordCount == 1 ? "Record inserted successful" : "Record inserted fail");
        }
    }
}
