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

        public DataTable GetSectionCode(string strDivision, string strDepartment)
        {
            var ObjRun = new mgrSQLConnect(_configuration);
            dt = new DataTable();
            strSQL = "";
            int Chk = 0;
            strSQL += "SELECT [SectionCode],[Section],[Department],[Division]";
            strSQL += "FROM [SPTOSystem].[dbo].[vewT_Section_Master] ";
            if (strDivision != "" && strDivision != null)
            {
                strSQL += Chk == 0 ? " Where  " : " and  ";

                strSQL += "Division = '" + strDivision + "'";
                Chk++;
            }
            if (strDepartment != "" && strDepartment != null)
            {
                strSQL += Chk == 0 ? " Where  " : " and  ";

                strSQL += "Department = '" + strDepartment + "'";
                Chk++;
            }


            strSQL += "group by  [SectionCode],[Section],[Department],[Division]  ";

            dt = ObjRun.GetDatatables(strSQL);

            return dt;
        }

        public DataTable GetDepartment(string strDivision)
        {
            mgrSQLConnect ObjRun = new mgrSQLConnect(_configuration);
            dt = new DataTable();
            strSQL = "";
            int Chk = 0;
            strSQL += "SELECT [Department]" +
                "FROM [SPTOSystem].[dbo].[vewT_Section_Master] ";

            if (strDivision != "" && strDivision != null)
            {
                strSQL += Chk == 0 ? " Where  " : " and  "; 
                strSQL += "Division = '" + strDivision + "'"; 
                Chk++;
            }
            strSQL += "group by [Department] ";

            dt = ObjRun.GetDatatables(strSQL);

            return dt;
        }
        public DataTable GetDivision()
        {
            mgrSQLConnect ObjRun = new mgrSQLConnect(_configuration);
            dt = new DataTable();
            strSQL = "";

            strSQL += "SELECT [Division]" +
                "FROM [SPTOSystem].[dbo].[vewT_Section_Master] ";
            strSQL += "group by [Division] ";

            dt = ObjRun.GetDatatables(strSQL);

            return dt;
        }

        public DataTable GetGroupName()
        {
            mgrSQLConnect ObjRun = new mgrSQLConnect(_configuration);
            dt = new DataTable();
            strSQL = "";

            strSQL += "SELECT [OperatorGroup] ,[GroupName] FROM [SPTOSystem].[dbo].[OperatorGroup]";

            dt = ObjRun.GetDatatables(strSQL);

            return dt;
        }
    }
}