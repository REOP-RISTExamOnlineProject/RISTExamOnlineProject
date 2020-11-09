using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using FastReport.Data;
using FastReport.Export.PdfSimple;
using FastReport.Utils;
using FastReport.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
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
        public IActionResult ForTest()
        {

            return View();

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



            var dt = new DataTable();
            var SqlCMD = new SqlCommand();
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
        public IActionResult PracticalList(string Staffcode)
        {

            return View();
        }

        public IActionResult GetPlanID(string OPID, string Staffcode)
        {
            mgrSQL_ObjCommand ObjRun = new mgrSQL_ObjCommand(_configuration);
            DataTable dt = new DataTable();
            string Strsql = "SELECT  [PlanID],[LicenseName]  FROM [dbo].[vewPracticalSnapshotRemainList] where Staffcode = '" + Staffcode + "' and Trianer = '" + OPID + "' group by [PlanID],[LicenseName]  ";
           
            SqlCommand SqlCMD = new SqlCommand();
            SqlCMD = new SqlCommand();
            SqlCMD.CommandType = CommandType.Text;
            SqlCMD.CommandText = Strsql;

            dt = ObjRun.GetDataTable(SqlCMD);


            if (dt.Rows.Count != 0) {




                return Json(new { success = true, planID = dt.Rows[0]["PlanID"].ToString(), licenseName = dt.Rows[0]["LicenseName"].ToString() });
            }
            
            return Json(new { success = false });

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
                    return Json(new { success = false, responetext = "รหัสพนักงานนี้ ถูกทดสอบไปแล้ว  หรือ ถูกสร้างแผนข้อสอบไปแล้ว กรุณเช็คข้อมูลที่ตารางด้านล่าง   " });

                }

            }
            else
            {

                return Json(new { success = false, responetext = "ไม่พบแผนการสอบ" });
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

                return Json(new { success = false, responetext = "ไม่พบแผนการสอบ" });
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
                    return Json(new { success = true, responetext = "บันทึกข้อมูลเรียบร้อยแล้ว", Judge = Judge, finish = false });
                }
                else if (MS == "Finish") {
                    return Json(new { success = true, responetext = " บันทึกข้อมูลเรียบร้อยแล้ว", Judge = Judge , finish = true});
                }
                else
                {
                    return Json(new { success = false, responetext = MS });
                }

            }
            else {
                return Json(new { success = false, responetext = "บันทึกข้อมูลไม่สำเร็จ" });

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
        [HttpGet]
        public IActionResult PracticalReport() {
           

            return View();
        }

        public WebReport GetReport(string OPID, string DDL_License_Name, string DDL_PlanID)
        {

            RegisteredObjects.AddConnection(typeof(MsSqlDataConnection));
            var webReport = new WebReport();

            var sqlConnection = new MsSqlDataConnection
            {
                ConnectionString = "Data Source=10.29.1.116;Initial Catalog=sptosystem;Persist Security Info=True;User ID=sa;Password=pwpolicy;Application Name=SPTO_SYSTEM"
            };
            //sqlConnection.ConnectionString = sqlConnection;
            sqlConnection.CreateAllTables();
            webReport.Report.Dictionary.Connections.Add(sqlConnection);
            webReport.Report.Load($@"Reports/Untitled.frx");
            //var string staffop = OPID ;
            //const string planid = "T2220-0001";
            //const string license = "CO1001";
            webReport.Report.SetParameterValue("PAM_OP", OPID);
            webReport.Report.SetParameterValue("PAM_PLANID", DDL_PlanID);
            webReport.Report.SetParameterValue("PAM_License", DDL_License_Name);

            ViewBag.WebReport = webReport;
            ViewBag.OPID = OPID;
            return webReport;
        }
        [HttpPost]


        [ActionName("PracticalReport")]
        public IActionResult PracticalReportPost(string OPID, string DDL_License_Name, string DDL_PlanID)
        {
            var webReport = GetReport(OPID,DDL_License_Name,DDL_PlanID);
            ViewBag.WebReport = webReport;
            ViewBag.StaffID = OPID;
            ViewBag.PlanID = DDL_PlanID;
            ViewBag.License_Name = DDL_License_Name;
            return View();
        }




        public IActionResult Pdf(string OPID, string DDL_License_Name, string DDL_PlanID)
        {
            var webReport = GetReport(OPID, DDL_License_Name, DDL_PlanID);
            webReport.Report.Prepare();

            using (var ms = new MemoryStream())
            {
                var pdfExport = new PDFSimpleExport();
                pdfExport.Export(webReport.Report, ms);
                ms.Flush();
                return File(ms.ToArray(), "application/pdf", Path.GetFileNameWithoutExtension("Report_") + ".pdf");
            }
        }

        public IActionResult GetPlanIDReport(string Staffcode)
        {

            mgrSQLcommand_Exam ObjRun = new mgrSQLcommand_Exam(_configuration);
            List<SelectListItem> listItems = new List<SelectListItem>();
            string Strsql = "select[PlanID],[PlanID] from  (select[PlanID] FROM [SPTOSystem].[dbo].[Z_001]  where[Staffcode] = '"+ Staffcode + "' group by [PlanID]) as e";
            listItems = ObjRun.GetItemDropDownList(Strsql, "PlanID");
            

            return Json(new SelectList(listItems, "Value", "Text"));
        }
        public IActionResult GetLicense_NameReport(string Staffcode,string PlanID) {

            mgrSQLcommand_Exam ObjRun = new mgrSQLcommand_Exam(_configuration);
            List<SelectListItem> listItems = new List<SelectListItem>();
            string Strsql = "select [LicenseName],[LicenseName] from (select[LicenseName] FROM [SPTOSystem].[dbo].[Z_001]  where[Staffcode]= '" + Staffcode + "' and [PlanID] = '"+ PlanID + "' group by [LicenseName]) as e";
            listItems = ObjRun.GetItemDropDownList(Strsql, "LicenseName");


            return Json(new SelectList(listItems, "Value", "Text"));

        }

        
    }


}