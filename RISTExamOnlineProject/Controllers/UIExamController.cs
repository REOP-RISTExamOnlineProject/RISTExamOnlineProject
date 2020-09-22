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
    public class UIExamController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly SPTODbContext _sptoDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;


        public UIExamController(SPTODbContext context, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {

            _sptoDbContext = context;
            _configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;

        }

        public IActionResult LicenceList()
        {
            return View();
        }
        public IActionResult ExamResultMonitor()
        {
            return View();
        }


        public IActionResult ModeExemList(string ItemCateg)
        {
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
            string strItemCateName = "";
            DataTable dt = new DataTable();
            dt = ObjRun.GetItemCateg(ItemCateg);
            TempData["XX"] = ItemCateg;
            strItemCateName = dt.Rows[0][2].ToString();
            ViewBag.Itemcateg = ItemCateg;
            ViewBag.ItemCateName = strItemCateName;
            return View();
        }

        public IActionResult Examexamination(string ItemInput, string ItemCateg)
        {
            ViewBag.Itemcateg = ItemCateg;
            ViewBag.InputItem = ItemInput;

            return View();
        }

        public JsonResult GetItemCatg()
        {
            var UserName = User.Identity.Name;
            _OperatorItemCateg dataOperator = new _OperatorItemCateg();
            DataTable dt = new DataTable();
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
            //List<_OperatorItemCateg> dataList = new List<_OperatorItemCateg>();
            ResultItemCateg ResultOPcateg = new ResultItemCateg();
            ResultOPcateg = ObjRun.GetOperatorItemCateg(UserName);


            var jsonResult = Json(new { data = ResultOPcateg._listOpCateg, _strResult = ResultOPcateg.strResult });

            return jsonResult;
        }

        public JsonResult GetInputItem(string itemCateg)
        {
            var UserName = User.Identity.Name;
            _OperatorItemCateg dataOperator = new _OperatorItemCateg();
            DataTable dt = new DataTable();
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
            //List<_OperatorItemCateg> dataList = new List<_OperatorItemCateg>();
            ResultItemCateg ResultOPcateg = new ResultItemCateg();
            ResultOPcateg = ObjRun.GetInputItemList(itemCateg, UserName);


            var jsonResult = Json(new { data = ResultOPcateg._listOpCateg, _strResult = ResultOPcateg.strResult });
            return jsonResult;
        }
        public JsonResult GetExamList(string itemCateg, string InputItem)
        {
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
            DataTable dt = new DataTable();


            int strMinute = 0;

            string Result = ObjRun.MakingExam(itemCateg, InputItem);
            if (Result != "Error")
            {
                dt = ObjRun.GetInputItems(InputItem);
                if (dt.Rows.Count != 0)
                {
                    strMinute = Convert.ToInt32(dt.Rows[0]["TimeLimit"].ToString());
                }
            }
            TempData["GG"] = DateTime.Now.ToString();
            var jsonResult = Json(new { data = "OK", _strResult = Result, _strMinute = strMinute });
            return jsonResult;
        }

        public JsonResult CommitExam(List<_ExamQuestionAnswer> ArrAns, string strItemCateg, string strItemInput, string OPID, string strStart)
        {
            string ItemCateg = strItemCateg;
            string ItemInput = strItemInput;
            string strOPID = OPID;
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
            DateTime _strStartTime = Convert.ToDateTime(strStart);

            string strStartTime = strStart;


            string strEndTime = DateTime.Now.ToString();
            string IP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _ExamCommitResult dt = new _ExamCommitResult();
            dt = ObjRun.CommitExam(strOPID, ItemCateg, ItemInput, strStartTime, strEndTime, ArrAns, IP);
            var jsonResult = Json(new { data = dt.strResult, dataResult = dt.strMgs, dataBool = dt.BoolResult });
            return jsonResult;
        }

        public JsonResult GetComtrolddl(string strCriteria)
        {
            ListSelectList_ listItems = new ListSelectList_();
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
            string strresult = "";
            try
            {
                listItems = ObjRun.GetPlaning(strCriteria);
            }
            catch (Exception e)
            {
                strresult = e.Message;
            }
            var jsonResult = Json(new { data = new MultiSelectList(listItems._ListSelectList, "Value", "Text"), dataResult = listItems.strResult });
            return jsonResult;
        }



        public JsonResult GetExamResultList(_excamResultCtrl strCtrl)
        {
            DateTime test = Convert.ToDateTime(strCtrl.EndTime);
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
            _ExamResultList listItems = new _ExamResultList();




            string strresult = "";
            try
            {
                listItems = ObjRun.GetDataExamResultList(strCtrl);
            }
            catch (Exception e)
            {
                strresult = e.Message;
            }
            var jsonResult = Json(new { data = listItems.DataExamReultList, strResult = listItems.strResult });

            //var jsonResult = Json(new { data="" });
            return jsonResult;
        }

    }
}