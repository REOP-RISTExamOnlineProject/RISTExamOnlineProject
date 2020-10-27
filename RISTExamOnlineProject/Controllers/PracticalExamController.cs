using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RISTExamOnlineProject.Models.db;
using RISTExamOnlineProject.Models.TSQL;

namespace RISTExamOnlineProject.Controllers
{
    public class PracticalExamController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly SPTODbContext _sptoDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;


        public PracticalExamController(SPTODbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {

            _sptoDbContext = context;
            _configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;

        }






        [Authorize]
        public IActionResult Index()
        {

            return View();

        }
        [Authorize]
        public IActionResult ParcticalCategory()
        {
            return View();
        }
        [Authorize]
        public IActionResult ParcticalDispaly(string OPID, string Staffcode, string ItemID, string PlanID, string LicenseName)
        {



            DataTable dt = new DataTable();
            SqlCommand SqlCMD = new SqlCommand();
            try
            {
                mgrSQL_ObjCommand ObjRun = new mgrSQL_ObjCommand(_configuration);
                //------------ check permit ----------
                string Strsql = "select *   FROM [SPTOSystem].[dbo].[vewPlan_Trainee] where [Staffcode] = '" + Staffcode + "' and [Trianer] = '" + OPID + "' and  [Plan_ID] = '" + PlanID + "' and License_Name = '" + LicenseName + "' ";
                SqlCMD = new SqlCommand();
                SqlCMD.CommandType = CommandType.Text;
                SqlCMD.CommandText = Strsql;
                dt = ObjRun.GetDataTable(SqlCMD);
            }
            catch (Exception)
            {

                return RedirectToAction(nameof(PracticalExamController.Index), "PracticalExam");
                throw;
            }


            if (dt.Rows.Count > 0)
            {
                return View();
            }
            else
            {

                return RedirectToAction(nameof(PracticalExamController.Index), "PracticalExam");
            }



        }

        [Authorize]
        public IActionResult PraticalList(string Staffcode)
        {

            return View();
        }

        public IActionResult GetPlanID(string OPID, string Staffcode)
        {
            mgrSQL_ObjCommand ObjRun = new mgrSQL_ObjCommand(_configuration);
            List<SelectListItem> listItems = new List<SelectListItem>();
            string Strsql = "SELECT  [PlanID],[LicenseName]  FROM [dbo].[vewPracticalSnapshotRemainList] where Staffcode = '" + Staffcode + "' and Trianer = '" + OPID + "' group by [PlanID],[LicenseName]  ";
            listItems = ObjRun.GetItemDropDownList(Strsql, "PlanID");
            return Json(new SelectList(listItems, "Value", "Text"));

        }


        public JsonResult LoginPratical_Staffcode(string OPID, string Staffcode)
        {

            mgrSQL_ObjCommand ObjRun = new mgrSQL_ObjCommand(_configuration);
            mgrSQLcommand_Practical ObjRun_Practical = new mgrSQLcommand_Practical(_configuration);


            //     List<SelectListItem> listItems = new List<SelectListItem>();
            DataTable dt = new DataTable();
            DataTable dt2 = new DataTable();
            DataSet DS = new DataSet();

            SqlCommand SqlCMD = new SqlCommand();

            string Plan_ID;

            string License_Name;
            string Strsql = "SELECT *  FROM [dbo].[vewPlan_Trainee] where Staffcode = '" + Staffcode + "' and Trianer = '" + OPID + "'";

            SqlCMD = new SqlCommand();
            SqlCMD.CommandType = CommandType.Text;
            SqlCMD.CommandText = Strsql;

            dt = ObjRun.GetDataTable(SqlCMD);

            //   DateTime ActualTime = DateTime.Now;


            TimeSpan ActualTime = new TimeSpan();
            bool CheckDuplicate = true;

            if (dt.Rows.Count != 0)
            {
                //--------------  Make snapshot -------------------
                for (int i = 0; i < dt.Rows.Count; i++)
                {

                    Plan_ID = dt.Rows[i]["Plan_ID"].ToString();
                    License_Name = dt.Rows[i]["License_Name"].ToString();
                    dt2 = new DataTable();
                    dt2 = ObjRun_Practical.sprPracticalSnapshot("ADD", Staffcode, Plan_ID, License_Name, 0, 0, 0, ActualTime, 0, 0, OPID);

                    if (Convert.ToBoolean(dt2.Rows[0][0]) != true)
                    {
                        CheckDuplicate = false;
                    }


                }


                if (CheckDuplicate == true)
                {
                    return Json(new { success = true, responetext = Staffcode });
                }
                else
                {
                    return Json(new { success = false, responetext = "Data Is duplicate plase check data in below table " });

                }

            }
            else
            {

                return Json(new { success = false, responetext = "Plan Pratical Not foud " });
            }


        }



        public JsonResult MakeDisplayPractical(string OPID, string Staffcode, string PlanID, string ItemID, string LicenseName)
        {


          
            mgrSQLcommand_Practical ObjRun_Practical = new mgrSQLcommand_Practical(_configuration);
            DataTable dt = new DataTable();


            dt = ObjRun_Practical.sprMakeDisplayPractical(Staffcode, PlanID, ItemID, LicenseName);

            if (dt.Rows.Count != 0)
            {
                return Json(new { success = true, responetext = dt.Rows[0][0].ToString() });
                // return Json(new { success = true, responetext ="" });
            }
            else
            {

                return Json(new { success = false, responetext = "Plan Pratical Not foud " });
            }









        }




        public JsonResult Savepractical(string Flag, string Staffcode, string PlanID, string LicenseName,
            int ItemID, int QuestionNo ,int HearingJudge, int ActualTime_Seconds, int PracticalJudge,int ActualTimeJudge, string OPID)
        {
            DataTable dt = new DataTable();
            mgrSQLcommand_Practical ObjRun_Practical = new mgrSQLcommand_Practical(_configuration);

           // TimeSpan ts = TimeSpan.FromTicks(486000000000)

            TimeSpan ActualTime = new TimeSpan();
            ActualTime = TimeSpan.FromSeconds(ActualTime_Seconds);

            int Judge;
            if (HearingJudge == 1 && PracticalJudge == 1 && ActualTimeJudge == 1)
            {
                Judge = 1;
            }
            else
            {
                Judge = 0;
            }

            dt = new DataTable();
            dt = ObjRun_Practical.sprPracticalSnapshot(Flag, Staffcode, PlanID, LicenseName, ItemID, QuestionNo, HearingJudge, ActualTime, PracticalJudge, Judge, OPID);

            if (dt.Rows.Count != 0)
            {
                string MS = dt.Rows[0][1].ToString();
                if (MS == "OK")
                {
                    return Json(new { success = true, responetext = "Successfully saved data" , Judge  = Judge });
                }
                else
                {
                    return Json(new { success = false, responetext = MS });
                }

            }
            else {
                return Json(new { success = false, responetext = "Failed to save data" });

            }



         
        }



        public JsonResult IndexDashBoard(string OPID) {
            mgrSQL_ObjCommand ObjRun = new mgrSQL_ObjCommand(_configuration);
            DataTable dt = new DataTable();
         

            List<vewPracticalSnapshotRemainList> Detail = new List<vewPracticalSnapshotRemainList>();


            SqlCommand SqlCMD = new SqlCommand();
            string Strdql = "SELECT*   FROM[SPTOSystem].[dbo].[vewPracticalSnapshotRemainList] where[Trianer] = '"+ OPID + "'";

         
            SqlCMD.CommandType = CommandType.Text;
            SqlCMD.CommandText = Strdql;
            dt = new DataTable();
            dt = ObjRun.GetDataTable(SqlCMD);



            if (dt.Rows.Count != 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    Detail.Add(new vewPracticalSnapshotRemainList()
                    {
                        Staffcode = row["Staffcode"].ToString(),
                        PlanID = row["PlanID"].ToString(),
                        LicenseName = row["LicenseName"].ToString(),
                        LicenseType = row["LicenseType"].ToString(),
                        RemainCnt = Convert.ToInt32(row["RemainCnt"].ToString()),
                        Trianer = row["Trianer"].ToString(),


                    });



                }

                return Json(new { success = true, Detail = Detail });

            }
            else {

                return Json(new { success = false });
            }



         




        }









    }
}