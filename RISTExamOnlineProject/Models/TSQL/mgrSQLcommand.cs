using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RISTExamOnlineProject.Models.TSQL;

namespace RISTExamOnlineProject.Models.TSQL
{
    public class mgrSQLcommand
    {

        private IConfiguration _configuration;

        public mgrSQLcommand(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        DataTable dt = new DataTable();
        DataSet ds = new DataSet();
        string strSQL = "";

        public DataTable GetPosition_()
        {
            mgrSQLConnect ObjRun = new mgrSQLConnect(_configuration);
            dt = new DataTable();
            strSQL = "";

            strSQL += "SELECT  [Position] FROM [SPTOSystem].[dbo].[Operator]  group by [Position]  order by [Position]";

            dt = ObjRun.GetDatatables(strSQL);

            return dt;
        }
    }
}
