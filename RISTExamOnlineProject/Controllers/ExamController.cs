using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using RISTExamOnlineProject.Models.db;
using RISTExamOnlineProject.Models.TSQL;

namespace RISTExamOnlineProject.Controllers
{
    public class ExamController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SPTODbContext _sptoDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;


        public ExamController(SPTODbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {

            _sptoDbContext = context;
            _configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;

        }





        public IActionResult New_Exam()
        {
            return View();
        }

        public IActionResult Index()
        {
            return View();
        }



        public IActionResult Exam_maintenance(string Itemcode)
        {

            ViewBag.Itemcode = Itemcode;
            return View();

        }



        public IActionResult Examination(string Itemcode)
        {


            ViewBag.Itemcode = Itemcode;

            return View();

        }


        public IActionResult GetExamDetail(string Itemcode)
        {
            DataTable dt = new DataTable();
            string ValueCodeQuestion = "";
            string ValueCodeAnswer;
            int QuestionCount = 0;
  
            string ItemName;
            int Max_Seq;

            mgrSQLcommand_Exam ObjRun = new mgrSQLcommand_Exam(_configuration);



            dt = ObjRun.Get_ExamDetail(Itemcode);
        
            if (dt.Rows.Count != 0)
            {
                Max_Seq = Convert.ToInt16(dt.Rows[0]["Max_Seq"].ToString());

                List<Exam_QuestionDetail> Detail = new List<Exam_QuestionDetail>();

                if (Max_Seq != 0)
                {

                    foreach (DataRow row in dt.Rows)
                    {
                        Detail.Add(new Exam_QuestionDetail()
                        {

                            ItemCode = row["ItemCode"].ToString(),
                            ItemCategName = row["ItemCategName"].ToString(),
                            ValueCodeQuestion = row["ValueCodeQuestion"].ToString(),
                            ValueCodeAnswer = row["ValueCodeAnswer"].ToString(),
                            Seq = Convert.ToInt16(row["Seq"].ToString()),
                            Question = row["Question"].ToString(),
                            Ans_Count = row["Ans_Count"].ToString(),
                            Max_Seq = row["Max_Seq"].ToString(),

                        });

                    }


                }
                else { 
                
                
                }




        





                ValueCodeQuestion = dt.Rows[0]["ValueCodeQuestion"].ToString();
                ValueCodeAnswer = dt.Rows[0]["ValueCodeAnswer"].ToString();

                dt = ObjRun.Get_ValueCount(ValueCodeQuestion);
                //   QuestionCount = Convert.ToInt32(dt.Rows.Count);         


             
                ItemName = dt.Rows[0]["ItemName"].ToString();
                ItemName = Itemcode + "-" + ItemName;

                if (Max_Seq == 0)
                {
                    QuestionCount = 0;
        
                }            
                else
                {
                    QuestionCount = Convert.ToInt32(dt.Rows.Count);
                   
                }


                return Json(new { success = true, ValueCodeQuestion = ValueCodeQuestion, ValueCodeAnswer = ValueCodeAnswer, QuestionCount = QuestionCount, Max_Seq = Max_Seq, ItemName = ItemName  , Detail = Detail });



            }
            else
            {
          
                return Json(new { success = false });
            }









        }


        public IActionResult GetHTML(string ItemCateg, string ItemCode)
        {
            mgrSQLcommand_Exam ObjRun = new mgrSQLcommand_Exam(_configuration);
            string HTMLTEXT = ObjRun.GetExamHTML(ItemCateg, ItemCode);
            return Json(new { success = true, HTMLTEXT = HTMLTEXT });
        }

        public IActionResult GetCategory()
        {
            mgrSQLcommand_Exam ObjRun = new mgrSQLcommand_Exam(_configuration);
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems = ObjRun.GetCategory();
            return Json(new SelectList(listItems, "Value", "Text"));

        }


        public IActionResult GetExamname(string Category)
        {
            mgrSQLcommand_Exam ObjRun = new mgrSQLcommand_Exam(_configuration);
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems = ObjRun.GetExamName(Category);
            return Json(new SelectList(listItems, "Value", "Text"));
        }



        public IActionResult Valueslist(int Max_Seq , int QuestionCount, string ValueCodeQuestion, string ValueCodeAnswer, string[] Ans_TextDisplay, string[] Ans_Text_HTML_Display
          , string[] Ans_Value, string Need_value, string Text_Question, string TextHTML_Question,string Job,string OP_UPD,int DisplayOrder)
        {

            mgrSQLcommand_Exam ObjRun = new mgrSQLcommand_Exam(_configuration);

   
            string IP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            string MS;
             
            try
            {
                if (Job == "UPD" || Job == "DEL")
                {

                    //---------------------------- BK Data  and Delete ------------

                    ObjRun.Valueslist_Management(Job, "", DisplayOrder, "", "", "0", "", IP, OP_UPD, ValueCodeQuestion.Trim(), ValueCodeAnswer.Trim());
                    Max_Seq = DisplayOrder;
                }
                else {
                    Max_Seq = Max_Seq + 1;
                }

                if (Job != "DEL") {

                    //----------- inseart Qeustion ----  
                    MS = ObjRun.Valueslist_Management("NEW", ValueCodeQuestion, Max_Seq, TextHTML_Question, Text_Question, "0", Need_value, IP, OP_UPD,"","");
                    if (MS !="OK") {
                        return Json(new { success = false, responseText = MS });
                    }
                    //----------- inseart Anser ----

                    for (int i = 0; i < Ans_TextDisplay.Length; i++)
                    {
                        MS = ObjRun.Valueslist_Management("NEW", ValueCodeAnswer, Max_Seq, Ans_Text_HTML_Display[i].Trim(), Ans_TextDisplay[i].Trim(), Ans_Value[i].Trim(), "0", IP, OP_UPD, "", "");

                        // ObjRun.InseartExam(ValueCodeAnswer, Max_Seq, Ans_Text_HTML_Display[i].Trim(), Ans_TextDisplay[i].Trim(), Ans_Value[i].Trim(), "0", IP, OP_UPD);

                        if (MS != "OK")
                        {
                            return Json(new { success = false, responseText = MS });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message.ToString() });
                throw;
            }



            return Json(new { success = true });

        }


        public JsonResult Get_HTML_Question_Detail(string ValueCodeAnswer, string ValueCodeQuestion, int Seq,string Job) {
            mgrSQLcommand_Exam ObjRun = new mgrSQLcommand_Exam(_configuration);
            string HTML_Text = ObjRun.HTML_Question_Detail(ValueCodeQuestion, ValueCodeAnswer, Seq, Job);
            return Json(new { success = true ,HTML = HTML_Text});            
        }




    }
}