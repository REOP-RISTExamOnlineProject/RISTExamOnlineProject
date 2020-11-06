using Microsoft.Extensions.Configuration;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace RISTExamOnlineProject.Models.TSQL
{
    public class mgrSQLcommand_Practical
    {


        private readonly IConfiguration _configuration;


        public mgrSQLcommand_Practical(IConfiguration configuration)
        {
            this._configuration = configuration;
        }

        DataTable dt = new DataTable();


        public DataTable sprPracticalSnapshot(string Flag,string Staffcode,string PlanID,string LicenseName,
            int ItemID,int QuestionNo, int HearingJudge,TimeSpan ActualTime,int PracticalJudge,int Judge,string OPID) {

            mgrSQL_ObjCommand ObjRun = new mgrSQL_ObjCommand(_configuration);

            SqlCommand SqlCMD = new SqlCommand();

            SqlCMD = new SqlCommand();
            SqlCMD.CommandType = CommandType.StoredProcedure;
            SqlCMD.CommandText = "sprPracticalSnapshot";
            SqlCMD.Parameters.Add("@Flag", SqlDbType.Char).Value = Flag;
            SqlCMD.Parameters.Add("@Staffcode", SqlDbType.NVarChar).Value = Staffcode;
            SqlCMD.Parameters.Add("@PlanID", SqlDbType.NVarChar).Value = PlanID;
            SqlCMD.Parameters.Add("@LicenseName", SqlDbType.NVarChar).Value = LicenseName;
            SqlCMD.Parameters.Add("@ItemID", SqlDbType.Int).Value = ItemID;
            SqlCMD.Parameters.Add("@QuestionNo", SqlDbType.Int).Value = QuestionNo;
            SqlCMD.Parameters.Add("@HearingJudge", SqlDbType.Bit).Value = HearingJudge;
            SqlCMD.Parameters.Add("@ActualTime", SqlDbType.Time).Value = ActualTime;
            SqlCMD.Parameters.Add("@PracticalJudge", SqlDbType.Bit).Value = PracticalJudge;
            SqlCMD.Parameters.Add("@Judge", SqlDbType.Bit).Value = Judge;
            SqlCMD.Parameters.Add("@UserName", SqlDbType.NVarChar).Value = OPID;
            dt = new DataTable();
            dt = ObjRun.GetDataTable(SqlCMD);

            return dt;
        
        
        }


        public DataTable sprMakeDisplayPractical(string Staffcode, string PlanID, string ItemID, string LicenseName) {
            
            mgrSQL_ObjCommand ObjRun = new mgrSQL_ObjCommand(_configuration);
            SqlCommand SqlCMD = new SqlCommand();
            SqlCMD = new SqlCommand();
            SqlCMD.CommandType = CommandType.StoredProcedure;
            SqlCMD.CommandText = "sprMakeDisplayPractical";
            SqlCMD.Parameters.Add("@Staffcode", SqlDbType.NVarChar).Value = Staffcode.Trim();
            SqlCMD.Parameters.Add("@Plan_ID", SqlDbType.NVarChar).Value = PlanID.Trim();
            SqlCMD.Parameters.Add("@ItemID", SqlDbType.NVarChar).Value = ItemID.Trim();
            SqlCMD.Parameters.Add("@LicenseName", SqlDbType.NVarChar).Value = LicenseName.Trim();
            dt = ObjRun.GetDataTable(SqlCMD);



            return dt;
        }





    }
}
