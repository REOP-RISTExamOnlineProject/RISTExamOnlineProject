using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using RISTExamOnlineProject.Models.db;



namespace RISTExamOnlineProject.Controllers
{
    public class ManagementController : Controller
    {
        private readonly SPTODbContext _sptoDbContext;

        public ManagementController(SPTODbContext context)
        {
            _sptoDbContext = context;
        }


        //public IActionResult ManagementUser(string opno)
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






        public IActionResult Load_Training_Record(string OPID) {



            var draw = HttpContext.Request.Form["draw"].FirstOrDefault();
            var start = Request.Form["start"].FirstOrDefault();
            var length = Request.Form["length"].FirstOrDefault();
            var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
            var sortColumnDir = Request.Form["order[0][dir]"].FirstOrDefault();
            var searchValue = Request.Form["search[value]"].FirstOrDefault();        

            int pageSize = length != null ? Convert.ToInt32(length) : 0;
            int skip = start != null ? Convert.ToInt32(start) : 0;
            int recordsTotal = 0;


            var dataShow = _sptoDbContext.Training_Record.Where(x => x.StaffCode == OPID).ToList();
                                 


            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
               
                 //dataShow =  dataShow.OrderBy(sortColumn).ThenBy(so);
               //Test Commit
            }


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