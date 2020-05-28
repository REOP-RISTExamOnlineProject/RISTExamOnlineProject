using System.Data;
using Microsoft.Extensions.Configuration;

namespace RISTExamOnlineProject.Models.TSQL
{
    public class mgrSQLcommand
    {
        private readonly IConfiguration _configuration;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private string strSQL = "";

        public mgrSQLcommand(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DataTable GetPosition_()
        {
            var ObjRun = new mgrSQLConnect(_configuration);
            dt = new DataTable();
            strSQL = "";

            strSQL += "SELECT  [Position] FROM [SPTOSystem].[dbo].[Operator]  group by [Position]  order by [Position]";

            dt = ObjRun.GetDatatables(strSQL);

            return dt;
        }
    }
}