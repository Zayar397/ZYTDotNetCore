using System.Data;
using System.Data.SqlClient;

namespace ZYTDotNetCore.Shared
{
    public class ADO_DOT_NET_SERVICE
    {
        private readonly string _connectionString;
        public ADO_DOT_NET_SERVICE(string connectionString)
        {
            _connectionString = connectionString;
        }
        public DataTable Query(string query, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(query,conn);
            if (sqlParameters is not null)
            {
                foreach (var sqlParameter in sqlParameters)
                {
                    cmd.Parameters.AddWithValue(sqlParameter.ParameterName, sqlParameter.Value);
                }
            }
            
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            conn.Close();
            return dt;
        }
        public int Execute(string sql, params SqlParameterModel[] sqlParameters)
        {
            SqlConnection conn = new SqlConnection(_connectionString);
            conn.Open();
            SqlCommand cmd = new SqlCommand(sql,conn);
            if (sqlParameters is not null)
            {
                foreach (var sqlParameter in sqlParameters)
                {
                    cmd.Parameters.AddWithValue(sqlParameter.ParameterName,sqlParameter.Value);
                }
            }
            int result = cmd.ExecuteNonQuery();
            conn.Close();
            return result;
        }
    }
    public class SqlParameterModel
    {
        public string ParameterName { get; set; }
        public object Value { get; set; }
        public SqlParameterModel(string parameterName, object value)
        {
            ParameterName = parameterName;
            Value = value;
        }
    }
}
