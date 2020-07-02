using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RISTExamOnlineProject.Models.db;
using RISTExamOnlineProject.Models.TSQL;


namespace RISTExamOnlineProject.Controllers
{
    public class TEC_ManagementController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly SPTODbContext _sptoDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;


        public TEC_ManagementController(SPTODbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {

            _sptoDbContext = context;
            _configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;

        }

        [HttpGet]
        public IActionResult TEC_Approved()
        {
            return View();
        }




        public IActionResult Load_TEC_Approved_Detail()
        {


            //          try
            //            {        
            mgrSQLcommand_TEC_Approved ObjRun = new mgrSQLcommand_TEC_Approved(_configuration);

            List<vewOperatorReqChange_Groupby> DataShow = new List<vewOperatorReqChange_Groupby>();

            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"]
                .FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var pageSize = length != null ? Convert.ToInt32(length) : 10;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            var recordsTotal = 0;


            DataShow = ObjRun.Get_ApprovedDetailGroup();


            // var DataShow = _sptoDbContext.vewOperatorReqChange.Where(x => x.ChangeOperatorID == "").ToList();

            // var dataShow = _sptoDbContext.vewOperatorAdditionalDep.Where(x => x.OperatorID == OPID).ToList();

            var data = DataShow.Skip(skip).Take(pageSize).ToList();
          //  return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
              return Json(new {draw, recordsFiltered = recordsTotal, recordsTotal, data });
            //}
            //catch (Exception ex)
            //{

            //    return Json(new { success = false, responseText = ex.Message.ToString() });
            //}


        }



        public IActionResult GetFullDetail(string DocNo) {



            //          try
            //            {        
            mgrSQLcommand_TEC_Approved ObjRun = new mgrSQLcommand_TEC_Approved(_configuration);

            List<vewOperatorReqChangeCompare> DataShow = new List<vewOperatorReqChangeCompare>();

            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"]
                .FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var pageSize = length != null ? Convert.ToInt32(length) : 10;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            var recordsTotal = 0;


            DataShow = ObjRun.Get_ApprovedDetail(DocNo);


         //   var DataShow = _sptoDbContext.vewOperatorReqChange.Where(x => x.DocNo == DocNo).ToList();

            // var dataShow = _sptoDbContext.vewOperatorAdditionalDep.Where(x => x.OperatorID == OPID).ToList();

            var data = DataShow.ToList();
          //  return Json(new { success = true, data });
             return Json(new {draw, recordsFiltered = recordsTotal, recordsTotal, data });
            //}
            //catch (Exception ex)
            //{

            //    return Json(new { success = false, responseText = ex.Message.ToString() });
            //}

        }


        public IActionResult ApproveData(string [] Nbr_Array, string MakerID, string reqOperatorID) {
            string Nbr = "";

            try
            {


                string Message = "";
                mgrSQLcommand_TEC_Approved ObjRun = new mgrSQLcommand_TEC_Approved(_configuration);

                for (int i = 0; i < Nbr_Array.Length;i++) {

                    Nbr = Nbr_Array[i];
                    Message = ObjRun.OperatorReqChange("UPD", Nbr.Trim(), "", "", "", "", "", "", reqOperatorID.Trim(), MakerID.Trim());

                }
               





                if (Message == "OK")
                {

                    return Json(new { success = true, responseText = "Approved Data Success" });
                }
                else {


                    return Json(new { success = false, responseText = Message });
                }



            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message.ToString() });
               
            }

          



        }

        public JsonResult GetDataInquiry()
        {
            var _Result = "OK";
            var _DataResult = "";
            var _ResultLabel = true;
            List<reqeustInquiry> dataOperator = new List<reqeustInquiry>();
            mgrSQLcommand_TEC_Approved ObjRun = new mgrSQLcommand_TEC_Approved(_configuration);
            dataOperator = ObjRun.GetRequestList();  
            var jsonResult = Json(new
            { strResult = _Result, dataLabel = _DataResult, strboolbel = _ResultLabel, data = dataOperator});

            return jsonResult;
        }


        public JsonResult ShowComepare(int Nbr) {

            //          try
            //            {        
            mgrSQLcommand_TEC_Approved ObjRun = new mgrSQLcommand_TEC_Approved(_configuration);

            List<ReqChangeCompareData> DataShow = new List<ReqChangeCompareData>();

            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"]
                .FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();
            var pageSize = length != null ? Convert.ToInt32(length) : 10;
            var skip = start != null ? Convert.ToInt32(start) : 0;
            var recordsTotal = 0;


            DataShow = ObjRun.GetCompareData(Nbr);


            //   var DataShow = _sptoDbContext.vewOperatorReqChange.Where(x => x.DocNo == DocNo).ToList();

            // var dataShow = _sptoDbContext.vewOperatorAdditionalDep.Where(x => x.OperatorID == OPID).ToList();

            var data = DataShow.ToList();
            //  return Json(new { success = true, data });
            return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
        }

    }
}