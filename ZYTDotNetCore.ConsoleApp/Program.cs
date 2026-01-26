using System.Data;
using System.Data.SqlClient;

namespace ZYTDotNetCore.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //ADO_DOT_NET adoDotNet = new ADO_DOT_NET();
            //adoDotNet.Delete();
            DAPPER dapper = new DAPPER();
            //dapper.CREATE("B012520","ThetP","Testing...");
            dapper.Edit(1);
            dapper.Edit(100);
        }
    }
}
