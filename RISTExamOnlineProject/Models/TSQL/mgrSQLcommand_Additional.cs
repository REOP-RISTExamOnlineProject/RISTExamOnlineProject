using Microsoft.Extensions.Configuration;
using System.Data;
using RISTExamOnlineProject.Models.TSQL;

namespace RISTExamOnlineProject.Models.TSQL
{
    public class mgrSQLcommand_Additional
    {
        private readonly IConfiguration _configuration;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private string strSQL = "";


        public mgrSQLcommand_Additional(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public DataTable GetDivision_Additional() {
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "SELECT [Division]  FROM[SPTOSystem].[dbo].[vewT_Section_Master] with(nolock) group by[Division] order by[Division] asc";
            dt = ObjRun.GetDatatables(strSQL);
            return dt;
        }

        public DataTable GetDepartment_Additional(string DIV)
        {
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "SELECT [Department] FROM[SPTOSystem].[dbo].[vewT_Section_Master] with(nolock) ";
             strSQL +=  "where Division = '"+ DIV.Trim() + "' group by[Department] order by[Department] asc";         
            
            
            dt = ObjRun.GetDatatables(strSQL);
            return dt;
        }
        public DataTable GetSection_Additional(string DIV,string DEP)
        {
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "SELECT [Section],SectionCode FROM [SPTOSystem].[dbo].[vewT_Section_Master] with(nolock) ";
            strSQL += "where Division = '" + DIV.Trim() + "' and [Department] = '" + DEP.Trim() + "' order by[Section] asc";
            dt = ObjRun.GetDatatables(strSQL);
            return dt;
        }



    }
}
