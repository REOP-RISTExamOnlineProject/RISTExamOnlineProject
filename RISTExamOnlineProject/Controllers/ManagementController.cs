using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using RISTExamOnlineProject.Models.db;
using RISTExamOnlineProject.Models.TSQL;

namespace RISTExamOnlineProject.Controllers
{
    public class ManagementController : Controller
    {
        private readonly SPTODbContext _sptoDbContext;
        private readonly IConfiguration _configuration;
        public ManagementController(SPTODbContext context, IConfiguration configuration)
        {
            _sptoDbContext = context;
            this._configuration = configuration;
        }

        
        public IActionResult ManagementUser(string opno)
        {


            ViewBag.opno = opno;
            var data = _sptoDbContext.vewOperatorAll.FirstOrDefault(x => x.OperatorID == opno);

            //Get Position to Dropdown
            var queryPosition = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno).
                Select(c => new { c.OperatorID, c.JobTitle });
            ViewBag.CategoryPosition = new SelectList(queryPosition.AsEnumerable(), "OperatorID", "JobTitle");

            //Get Division to Dropdown
            var queryDivision = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno).
                Select(c => new { c.OperatorID, c.Division });
            ViewBag.CategoryDivision = new SelectList(queryDivision.AsEnumerable(), "OperatorID", "Division");

            //Get Department to Dropdown
            var queryDepartment = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno).
                Select(c => new { c.OperatorID, c.Department });
            ViewBag.CategoryDepartment = new SelectList(queryDepartment.AsEnumerable(), "OperatorID", "Department");

            //Get Section to Dropdown
            var querySection = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno).
                Select(c => new { c.OperatorID, c.Section });
            ViewBag.CategorySection = new SelectList(querySection.AsEnumerable(), "OperatorID", "Section");

            //Get Shift to Dropdown
            var queryShift = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno).
                Select(c => new { c.OperatorID, c.GroupName });
            ViewBag.CategoryShift = new SelectList(queryShift.AsEnumerable(), "OperatorID", "GroupName");

            //Get License to Dropdown
            var queryLicense = _sptoDbContext.vewOperatorLicense.Where(x => x.OperatorID == opno).
                Select(c => new { c.OperatorID, c.License });
            ViewBag.CategoryLicense = new MultiSelectList(queryLicense.AsEnumerable(), "OperatorID", "License");



            return View(data);

        }


        //public IActionResult UserDetailMaintenance(string opno)
        //{


        //    ViewBag.opno = opno;
        //    var data = _sptoDbContext.vewOperatorAll.FirstOrDefault(x => x.OperatorID == opno);

        //    //Get Position to Dropdown
        //    var queryPosition = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno).
        //        Select(c => new { c.OperatorID, c.JobTitle });
        //    ViewBag.CategoryPosition = new SelectList(queryPosition.AsEnumerable(), "OperatorID", "JobTitle");

        //    //Get Division to Dropdown
        //    var queryDivision = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno).
        //        Select(c => new { c.OperatorID, c.Division });
        //    ViewBag.CategoryDivision = new SelectList(queryDivision.AsEnumerable(), "OperatorID", "Division");

        //    //Get Department to Dropdown
        //    var queryDepartment = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno).
        //        Select(c => new { c.OperatorID, c.Department });
        //    ViewBag.CategoryDepartment = new SelectList(queryDepartment.AsEnumerable(), "OperatorID", "Department");

        //    //Get Section to Dropdown
        //    var querySection = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno).
        //        Select(c => new { c.OperatorID, c.Section });
        //    ViewBag.CategorySection = new SelectList(querySection.AsEnumerable(), "OperatorID", "Section");

        //    //Get Shift to Dropdown
        //    var queryShift = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno).
        //        Select(c => new { c.OperatorID, c.GroupName });
        //    ViewBag.CategoryShift = new SelectList(queryShift.AsEnumerable(), "OperatorID", "GroupName");

        //    //Get License to Dropdown
        //    var queryLicense = _sptoDbContext.vewOperatorLicense.Where(x => x.OperatorID == opno).
        //        Select(c => new { c.OperatorID, c.License });
        //    ViewBag.CategoryLicense = new MultiSelectList(queryLicense.AsEnumerable(), "OperatorID", "License");

        //    return View(data);

        //} 


        public IActionResult UserDetailMaintenance(string Event) 
        {
            string Event_ = Event == null?"info": Event;

            ViewBag.Event = Event_;

            return View();
        }


        public JsonResult GetDataUserdetail(string opno)
        {
            string _Result = "OK";
            string _DataResult = "";
            Boolean _ResultLabel = true;
            ViewBag.opno = opno;
               var data_ = _sptoDbContext.vewOperatorAll.FirstOrDefault(x => x.OperatorID == opno);

            vewOperatorAlls dataOperator = new vewOperatorAlls();

            dataOperator = data_;


            var jsonResult = Json(new { strResult = _Result, dataLabel = _DataResult, strboolbel = _ResultLabel, data = data_ });
           
            return jsonResult; 
        }



        public JsonResult GetPosition()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            DataTable dt = new DataTable();
            mgrSQLcommand ObjRun= new mgrSQLcommand(_configuration);
            dt = ObjRun.GetPosition_();

            if (dt.Rows.Count != 0)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = "Choose Position",
                    Value = ""
                });

                foreach (DataRow row in dt.Rows)
                {
                    listItems.Add(new SelectListItem()
                    {
                        Text = row["Position"].ToString().Trim() ,
                        Value = row["Position"].ToString().Trim(),

                    });
                }
            } 
            return Json(new MultiSelectList(listItems, "Value", "Text"));
        }
        

        public IActionResult Load_OperatorAdditional_Detail(string OPID) {



            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();        

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;

            var dataShow = _sptoDbContext.vewOperatorAdditionalDep.Where(x => x.OperatorID == OPID).ToList();
                                 


            //if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            //{
               
            //     //dataShow =  dataShow.OrderBy(sortColumn).ThenBy(so);
            //   //Test Commit
            //}


            //if (!string.IsNullOrEmpty(searchValue))
            //{
            //    searchValue = searchValue.ToUpper();
            //    DataShow = DataShow.Where(m => m.QuotationNo.Contains(searchValue) || m.RequesterName.Contains(searchValue) || m.OPID.Contains(searchValue));
            //}

            //total number of rows count     
            recordsTotal = dataShow.Count();
            //Paging     
            var data = dataShow.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
          
        }
    }
}