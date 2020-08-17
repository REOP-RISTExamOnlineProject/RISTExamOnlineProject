using Microsoft.Extensions.Configuration;
using System.Data;
using RISTExamOnlineProject.Models.TSQL;
using System.Collections.Generic;
using RISTExamOnlineProject.Models.db;

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






        public string GetStroeTemp_Additional(string OPID,string MakerID, string SectionCode, string Job)
        {
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "EXEC [dbo].[sprOperatorSectionAttributeTemp] ";
            strSQL += " '"+ Job + "', '" + MakerID + "','" + OPID + "','"+ SectionCode.Trim() + "'   ";          
           DataTable dt = ObjRun.GetDatatables(strSQL);
            string check = dt.Rows[0][0].ToString();
            return check;
        }






        public List<vewOperatorAdditionalDepTemp> GetUserDetail_Additional(string OPID)
        {
            var ObjRun = new mgrSQLConnect(_configuration);
            strSQL = "SELECT * FROM [SPTOSystem].[dbo].[vewOperatorAdditionalDepTemp] where [OperatorID] = '"+ OPID + "'  order by  [SectionCode] asc ";
            dt = ObjRun.GetDatatables(strSQL);

            List<vewOperatorAdditionalDepTemp> Temp = new List<vewOperatorAdditionalDepTemp>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Temp.Add(new vewOperatorAdditionalDepTemp()
                    {
                        OperatorID = row["OperatorID"].ToString(),
                        SectionCode = row["SectionCode"].ToString(),
                        Section = row["Section"].ToString(),
                        Department = row["Department"].ToString(),
                        Division = row["Division"].ToString(),
                    });
                }
            }


            return Temp;
            
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
