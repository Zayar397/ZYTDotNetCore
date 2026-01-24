using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZYTDotNetCore.ConsoleApp
{
    public class ADO_DOT_NET
    {
        private readonly string _connectionString = "Data Source = DESKTOP-BPF6HTF\\SQL2022; Initial Catalog = DotNetTrainingBatch5; User Id = sa; Password = p@ssw0rd;";
        public void Read()
        {
            //string connStr = "Data Source = DESKTOP-BPF6HTF\\SQL2022; Initial Catalog = DotNetTrainingBatch5; User Id = sa; Password = p@ssw0rd;";
            //Console.WriteLine("Connection String: " + connStr);
            SqlConnection conn = new SqlConnection(_connectionString);

            Console.WriteLine("Connection is opening...");
            conn.Open();
            Console.WriteLine("Connection is opened.");

            // select query
            string selectQuery = @"SELECT [BlogId]
                                          ,[BlogTitle]
                                          ,[BlogAuthor]
                                          ,[BlogContent]
                                          ,[DeleteFlage]
                                      FROM [dbo].[TBL_BLOG] where DeleteFlage = 0";

            SqlCommand cmd = new SqlCommand(selectQuery, conn);
            //SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            //DataTable dt = new DataTable();
            //adapter.Fill(dt);
            SqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                Console.WriteLine("BlogId: " + reader["BlogId"]);
                Console.WriteLine("BlogTitle: " + reader["BlogTitle"]);
                Console.WriteLine("BlogAuthor: " + reader["BlogAuthor"]);
                Console.WriteLine("BlogContent: " + reader["BlogContent"]);
                Console.WriteLine();
            }

            Console.WriteLine("Connection is closing...");
            conn.Close();
            Console.WriteLine("Connection is closed.");
        }
        public void Create()
        {
            //string connStr = "Data Source = DESKTOP-BPF6HTF\\SQL2022; Initial Catalog = DotNetTrainingBatch5; User Id = sa; Password = p@ssw0rd;";
            //Console.WriteLine("Connection String: " + connStr);
            SqlConnection conn = new SqlConnection(_connectionString);

            Console.WriteLine("Connection is opening...");
            conn.Open();
            Console.WriteLine("Connection is opened.");

            // insert query
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
                                           ,@DeleteFlage)";

            //string insertQuery = $@"INSERT INTO [dbo].[TBL_BLOG]
            //                               ([BlogTitle]
            //                               ,[BlogAuthor]
            //                               ,[BlogContent]
            //                               ,[DeleteFlage])
            //                         VALUES
            //                               ('{title}'
            //                               ,'{author}'
            //                               ,'{content}'
            //                               ,0)";

            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            cmd.Parameters.AddWithValue("@DeleteFlage", 0);
            int recordCount = cmd.ExecuteNonQuery();

            Console.WriteLine("Connection is closing...");
            conn.Close();
            Console.WriteLine("Connection is closed.");

            Console.WriteLine(recordCount == 1 ? "Record inserted successful" : "Record inserted fail");
        }
        public void Edit()
        {
            Console.WriteLine("Enter Blog Id:");
            string blogId = Console.ReadLine();

            SqlConnection conn = new SqlConnection(_connectionString);

            Console.WriteLine("Connection is opening...");
            conn.Open();
            Console.WriteLine("Connection is opened.");

            string selectQuery = @"SELECT [BlogId]
                                  ,[BlogTitle]
                                  ,[BlogAuthor]
                                  ,[BlogContent]
                                  ,[DeleteFlage]
                              FROM [dbo].[TBL_BLOG] where BlogId = @BlogId";

            SqlCommand cmd = new SqlCommand(selectQuery, conn);
            cmd.Parameters.AddWithValue("@BlogId", blogId);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            Console.WriteLine("Connection is closing...");
            conn.Close();
            Console.WriteLine("Connection is closed.");

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("Record not found.");
                return;
            }

            DataRow dr = dt.Rows[0];
            Console.WriteLine("Blog Title:" + dr["BlogTitle"]);
            Console.WriteLine("Blog Author:" + dr["BlogAuthor"]);
            Console.WriteLine("Blog Content:" + dr["BlogContent"]);
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

            SqlConnection conn = new SqlConnection(_connectionString);

            Console.WriteLine("Connection is opening...");
            conn.Open();
            Console.WriteLine("Connection is opened.");

            string insertQuery = @"UPDATE [dbo].[TBL_BLOG]
                                       SET [BlogTitle] = @BlogTitle
                                          ,[BlogAuthor] = @BlogAuthor
                                          ,[BlogContent] = @BlogContent
                                          ,[DeleteFlage] = 0
                                     WHERE BlogId = @BlogID";

            SqlCommand cmd = new SqlCommand(insertQuery, conn);
            cmd.Parameters.AddWithValue("@BlogTitle", title);
            cmd.Parameters.AddWithValue("@BlogAuthor", author);
            cmd.Parameters.AddWithValue("@BlogContent", content);
            cmd.Parameters.AddWithValue("@BlogID", blogId);
            int recordCount = cmd.ExecuteNonQuery();

            Console.WriteLine("Connection is closing...");
            conn.Close();
            Console.WriteLine("Connection is closed.");

            Console.WriteLine(recordCount == 1 ? "Record updated successful" : "Record updated fail");
        }
    }
}
