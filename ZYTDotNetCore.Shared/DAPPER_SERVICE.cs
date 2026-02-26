using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;

namespace ZYTDotNetCore.Shared
{
    public class DAPPER_SERVICE
    {
        private readonly string _connectionString;
        public DAPPER_SERVICE(string connectionString)
        {
            _connectionString = connectionString;
        }
        public List<T> Query<T>(string query)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                //Console.WriteLine("Query>> "+query);
                var blogList = db.Query<T>(query).ToList();
                return blogList;
            }
        }
        public T QueryFirstOrDefault<T>(string query, object? param = null)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                var item = db.QueryFirstOrDefault<T>(query,param);
                return item;
            }
        }
        public int Execute(string query, object? param = null)
        {
            using(IDbConnection db = new SqlConnection(_connectionString))
            {
                int result = db.Execute(query,param);
                return result;
            }
        }
    }
}
