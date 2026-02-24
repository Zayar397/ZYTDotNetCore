using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZYTDotNetCore.Shared;

namespace ZYTDotNetCore.ConsoleApp
{
    class ADO_DOT_NET_2
    {
        private readonly string _connectionString = "Data Source = DESKTOP-BPF6HTF\\SQL2022; Initial Catalog = DotNetTrainingBatch5; User Id = sa; Password = p@ssw0rd;";
        private readonly ADO_DOT_NET_SERVICE _adoDotNetService;
        public ADO_DOT_NET_2()
        {
            _adoDotNetService = new ADO_DOT_NET_SERVICE(_connectionString);
        }
        public void Read()
        {
            string selectQuery = @"SELECT [BlogId]
                                          ,[BlogTitle]
                                          ,[BlogAuthor]
                                          ,[BlogContent]
                                          ,[DeleteFlage]
                                      FROM [dbo].[TBL_BLOG] where DeleteFlage = 0";
            DataTable dt = _adoDotNetService.Query(selectQuery);
            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine("Id: " + dr["BlogId"]);
                Console.WriteLine("Title: " + dr["BlogId"]);
                Console.WriteLine("Author: " + dr["BlogAuthor"]);
                Console.WriteLine("Content: " + dr["BlogContent"]);
                Console.WriteLine();
            }
        }
        public void Edit()
        {
            Console.WriteLine("Enter Id");
            string id = Console.ReadLine();

            string selectQuery = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                                  ,[DeleteFlage]
                              FROM [dbo].[TBL_BLOG] where BlogId = @BlogId";
            //SqlParameterModel[] sqlParameters = new SqlParameterModel[1];
            //sqlParameters[0] = new SqlParameterModel()
            //{
            //    ParameterName = "BlogId",
            //    Value = id
            //};
            DataTable dt = _adoDotNetService.Query( selectQuery,
                                                    new SqlParameterModel("@BlogId",id)
                                                  );
            DataRow dr = dt.Rows[0];
            Console.WriteLine("Id: " + dr["BlogId"]);
            Console.WriteLine("Title: " + dr["BlogId"]);
            Console.WriteLine("Author: " + dr["BlogAuthor"]);
            Console.WriteLine("Content: " + dr["BlogContent"]);
            Console.WriteLine();
        }
        public void Create()
        {
            Console.WriteLine("Enter Title:");
            string title = Console.ReadLine();

            Console.WriteLine("Enter Author:");
            string author = Console.ReadLine();

            Console.WriteLine("Enter Content:");
            string content = Console.ReadLine();

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
            int result = _adoDotNetService.Execute( insertQuery,
                                                    new SqlParameterModel("@BlogTitle",title),
                                                    new SqlParameterModel("@BlogAuthor",author),
                                                    new SqlParameterModel("@BlogContent",content)
                                                   );
            Console.WriteLine(result == 1 ? "Record inserted successful" : "Record inserted fail");

        }
        public void Update()
        {
            Console.WriteLine("Enter Blog Id:");
            string blogId = Console.ReadLine();

            Console.WriteLine("Enter Title:");
            string title = Console.ReadLine();

            Console.WriteLine("Enter Author:");
            string author = Console.ReadLine();

            Console.WriteLine("Enter Content:");
            string content = Console.ReadLine();

            string updateQuery = @"UPDATE [dbo].[TBL_BLOG]
                                       SET [BlogTitle] = @BlogTitle
                                          ,[BlogAuthor] = @BlogAuthor
                                          ,[BlogContent] = @BlogContent
                                          ,[DeleteFlage] = 0
                                     WHERE BlogId = @BlogID";
            int recordCount = _adoDotNetService.Execute(  updateQuery,
                                        new SqlParameterModel("@BlogTitle",title),
                                        new SqlParameterModel("@BlogAuthor", author),
                                        new SqlParameterModel("@BlogContent", content),
                                        new SqlParameterModel("@BlogID", blogId)
                                     );
            Console.WriteLine(recordCount == 1 ? "Record updated successful" : "Record updated fail");
        }
        public void Delete()
        {
            Console.WriteLine("Enter Blog Id:");
            string blogId = Console.ReadLine();

            string deleteQuery = @"UPDATE [dbo].[TBL_BLOG]
                                       SET [DeleteFlage] = 0
                                     WHERE BlogId = @BlogID";
            int recordCount = _adoDotNetService.Execute(  deleteQuery,
                                        new SqlParameterModel("@BlogID",blogId)
                                      );
            Console.WriteLine(recordCount == 1 ? "Record deleted successful" : "Record deleted fail");
        }
    }
}
