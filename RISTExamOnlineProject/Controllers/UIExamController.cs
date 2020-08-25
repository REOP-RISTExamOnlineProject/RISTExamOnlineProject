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

        public IActionResult Examexamination(string ItemInput , string ItemCateg)
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
            ResultOPcateg = ObjRun.GetInputItemList(itemCateg);


            var jsonResult = Json(new { data = ResultOPcateg._listOpCateg, _strResult = ResultOPcateg.strResult }); 
            return jsonResult;
        }  
        public JsonResult GetExamList(string itemCateg, string InputItem)
        {
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration); 
            string Result = ObjRun.MakingExam(itemCateg, InputItem);
            TempData["GG"] = DateTime.Now.ToString();
            var jsonResult = Json(new { data = "OK", _strResult = Result }); 
            return jsonResult;
        }

        public JsonResult CommitExam(List<_ExamQuestionAnswer> ArrAns,string strItemCateg,string strItemInput,string OPID)
        {
            string ItemCateg = strItemCateg;
            string ItemInput = strItemInput;
            string strOPID = OPID;
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
            string strStartTime = TempData["GG"].ToString(); 
            string strEndTime = DateTime.Now.ToString();
            string IP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            _ExamCommitResult dt = new _ExamCommitResult();
            dt = ObjRun.CommitExam(strOPID, ItemCateg, ItemInput, strStartTime, strEndTime, ArrAns, IP); 




            var jsonResult = Json(new { data = dt.strResult,dataResult = dt.strMgs, dataBool = dt.BoolResult  });
            return jsonResult;
        }
        
    }
} 