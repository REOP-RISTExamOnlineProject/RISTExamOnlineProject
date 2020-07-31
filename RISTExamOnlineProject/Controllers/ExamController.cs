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
            int LastSeq = 0;
            string ItemName;
            int Max_Seq;

            mgrSQLcommand_Exam ObjRun = new mgrSQLcommand_Exam(_configuration);



            dt = ObjRun.Get_ExamDetail(Itemcode);
            Max_Seq = Convert.ToInt16(dt.Rows[0]["Max_Seq"].ToString());
            if (Max_Seq != 0)
            {


                List<Exam_QuestionDetail> Detail = new List<Exam_QuestionDetail>();

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





                ValueCodeQuestion = dt.Rows[0]["ValueCodeQuestion"].ToString();
                ValueCodeAnswer = dt.Rows[0]["ValueCodeAnswer"].ToString();

                dt = ObjRun.Get_ValueCount(ValueCodeQuestion);
                //   QuestionCount = Convert.ToInt32(dt.Rows.Count);


           


                LastSeq = Convert.ToInt32(dt.Rows[0]["Seq"].ToString());
                ItemName = dt.Rows[0]["ItemName"].ToString();
                ItemName = Itemcode + "-" + ItemName;

                if (LastSeq == 0)
                {
                    QuestionCount = 1;
                    LastSeq = 1;
                }
                else
                {

                    QuestionCount = Convert.ToInt32(dt.Rows.Count) + 1;
                    LastSeq = LastSeq + 1;
                }


                return Json(new { success = true, ValueCodeQuestion = ValueCodeQuestion, ValueCodeAnswer = ValueCodeAnswer, QuestionCount = QuestionCount, LastSeq = LastSeq, ItemName = ItemName  , Detail = Detail });



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



        public IActionResult InseartExam(string LastSeq, int QuestionCount, string ValueCodeQuestion, string ValueCodeAnswer, string[] Ans_TextDisplay, string[] Ans_Text_HTML_Display
          , string[] Ans_Value, string Need_value, string Text_Question, string TextHTML_Question,string Job,string OP_UPD,int DisplayOrder)
        {

            mgrSQLcommand_Exam ObjRun = new mgrSQLcommand_Exam(_configuration);

   
            string IP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            try
            {


                if (Job == "Edit") {
                    //---------------------------- Delete Data ------------

                    ObjRun.DeleteQuestion(ValueCodeQuestion.Trim(), ValueCodeAnswer.Trim(), Convert.ToInt32(DisplayOrder));
                    LastSeq = DisplayOrder.ToString();

                }


                //----------- inseart Qeustion ----



                ObjRun.InseartExam(ValueCodeQuestion, LastSeq, TextHTML_Question, Text_Question, "0", Need_value, IP, OP_UPD);




                //----------- inseart Anser ----

                for (int i = 0; i < Ans_TextDisplay.Length; i++)
                {
                    
                    ObjRun.InseartExam(ValueCodeAnswer, LastSeq, Ans_Text_HTML_Display[i].Trim(), Ans_TextDisplay[i].Trim(), Ans_Value[i].Trim(), "0", IP, OP_UPD);


                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message.ToString() });
                throw;
            }



            return Json(new { success = true });

        }


        public JsonResult Get_EditQuestion_Detail(string ValueCodeAnswer, string ValueCodeQuestion, int Seq) {
            mgrSQLcommand_Exam ObjRun = new mgrSQLcommand_Exam(_configuration);

            string HTML_Text = ObjRun.EditQuestion_Detail(ValueCodeQuestion, ValueCodeAnswer, Seq);
            return Json(new { success = true ,HTML = HTML_Text});

        }


        public JsonResult Delete_Question(string ValueCodeAnswer, string ValueCodeQuestion, int Seq) {



            mgrSQLcommand_Exam ObjRun = new mgrSQLcommand_Exam(_configuration);

            ObjRun.DeleteQuestion(ValueCodeQuestion.Trim(), ValueCodeAnswer.Trim(), Seq);


            return Json(new { success = true });
        
        }


    }
}