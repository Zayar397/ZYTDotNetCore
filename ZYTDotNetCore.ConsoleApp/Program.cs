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
            //DAPPER dapper = new DAPPER();
            //dapper.CREATE("B012520","ThetP","Testing...");
            //dapper.Edit(1);
            //dapper.Edit(100);
            //dapper.UPDATE(7,"B012610","xxx","Update Testing...");
            //dapper.DELETE(7);
            EFCORE efCore = new EFCORE();
            //efCore.READ();
            //efCore.CREATE("A012910","Yoon Waddy","Testing...");
            efCore.EDIT(1006);
        }
    }
}
