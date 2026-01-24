using System.Data;
using System.Data.SqlClient;

namespace ZYTDotNetCore.ConsoleApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ADO_DOT_NET adoDotNet = new ADO_DOT_NET();
            adoDotNet.Update();
        }
    }
}
