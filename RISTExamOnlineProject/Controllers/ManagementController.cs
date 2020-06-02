﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RISTExamOnlineProject.Models.db;
using RISTExamOnlineProject.Models.TSQL;

namespace RISTExamOnlineProject.Controllers
{
    public class ManagementController : Controller
    {
       
        private readonly IConfiguration _configuration;
        private readonly SPTODbContext _sptoDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
         

        public ManagementController(SPTODbContext context, IConfiguration configuration,IHttpContextAccessor httpContextAccessor )
        {
            
            _sptoDbContext = context;
            _configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
        }

        #region UserDetail

        public IActionResult ManagementUser(string opno)
        {
            ViewBag.opno = opno;
            var data = _sptoDbContext.vewOperatorAll.FirstOrDefault(x => x.OperatorID == opno);

            //Get Position to Dropdown
            var queryPosition = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno)
                .Select(c => new { c.OperatorID, c.JobTitle });
            ViewBag.CategoryPosition = new SelectList(queryPosition.AsEnumerable(), "OperatorID", "JobTitle");

            //Get Division to Dropdown
            var queryDivision = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno)
                .Select(c => new { c.OperatorID, c.Division });
            ViewBag.CategoryDivision = new SelectList(queryDivision.AsEnumerable(), "OperatorID", "Division");

            //Get Department to Dropdown
            var queryDepartment = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno)
                .Select(c => new { c.OperatorID, c.Department });
            ViewBag.CategoryDepartment = new SelectList(queryDepartment.AsEnumerable(), "OperatorID", "Department");

            //Get Section to Dropdown
            var querySection = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno)
                .Select(c => new { c.OperatorID, c.Section });
            ViewBag.CategorySection = new SelectList(querySection.AsEnumerable(), "OperatorID", "Section");

            //Get Shift to Dropdown
            var queryShift = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno)
                .Select(c => new { c.OperatorID, c.GroupName });
            ViewBag.CategoryShift = new SelectList(queryShift.AsEnumerable(), "OperatorID", "GroupName");

            //Get License to Dropdown
            var queryLicense = _sptoDbContext.vewOperatorLicense.Where(x => x.OperatorID == opno)
                .Select(c => new { c.OperatorID, c.License });
            ViewBag.CategoryLicense = new MultiSelectList(queryLicense.AsEnumerable(), "OperatorID", "License");


            return View(data);

        }
        [Authorize]
        public IActionResult UserDetailMaintenance(string Event) 
        {
            var Event_ = Event == null ? "_partsUserInfo" : Event;
             
            ViewBag.Event = Event_;
            string IPAddress = "";
            ViewBag.IPAddress = IPAddress;
             


         //   var Test = Request.Cookies["opid"].ToString();
            return View();
        }
        public JsonResult GetDataUserdetail(string opno)
        {
            var _Result = "OK";
            var _DataResult = "";
            var _ResultLabel = true;
            vewOperatorAlls dataOperator = new vewOperatorAlls();
            List<vewOperatorLicense> dataLicenses = new List<vewOperatorLicense>(); 
             
            var data_ = _sptoDbContext.vewOperatorAll.FirstOrDefault(x => x.OperatorID == opno); 
            
            dataOperator = data_;
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);

            dataLicenses = ObjRun.GetUserLicense(opno);

            _Result = dataOperator != null ? "OK" : "error";
            _DataResult = _Result != "OK" ? "Data not found" : "";

            var jsonResult = Json(new
            { strResult = _Result, dataLabel = _DataResult, strboolbel = _ResultLabel, data = dataOperator, dataLicense = dataLicenses });

            return jsonResult;
        }
        public JsonResult GetSectionCode(string strDivision, string strDepartment)
        {
            var listItems = new List<SelectListItem>();

            DataTable dt = new DataTable();
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
            dt = ObjRun.GetSectionCode(strDivision, strDepartment);

            if (dt.Rows.Count != 0)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = "Choose Section",
                    Value = ""
                });
                foreach (DataRow row in dt.Rows)
                {
                    string strText = row["SectionCode"].ToString().Trim() + " : " + row["Section"].ToString().Trim();

                    listItems.Add(new SelectListItem()
                    {
                        Text = strText,
                        Value = row["SectionCode"].ToString().Trim(),

                    });
                }
            }
            return Json(new MultiSelectList(listItems, "Value", "Text"));
        }
        public JsonResult GetDepartment(string strDivision)
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            DataTable dt = new DataTable();
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
            dt = ObjRun.GetDepartment(strDivision);

            if (dt.Rows.Count != 0)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = "Choose Department",
                    Value = ""
                });
                foreach (DataRow row in dt.Rows)
                {



                    listItems.Add(new SelectListItem()
                    {
                        Text = row["Department"].ToString().Trim(),
                        Value = row["Department"].ToString().Trim(),

                    });
                }
            }
            return Json(new MultiSelectList(listItems, "Value", "Text"));
        }
        public JsonResult GetDivision()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            DataTable dt = new DataTable();
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
            dt = ObjRun.GetDivision();

            if (dt.Rows.Count != 0)
            {
                listItems.Add(new SelectListItem
                {
                    Text = "Choose Division",
                    Value = ""
                });
                foreach (DataRow row in dt.Rows)
                {
                    listItems.Add(new SelectListItem()
                    {
                        Text = row["Division"].ToString().Trim(),
                        Value = row["Division"].ToString().Trim(),

                    });
                }
            }
            return Json(new MultiSelectList(listItems, "Value", "Text"));
        }
        public JsonResult GetGroupName()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            DataTable dt = new DataTable();
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
            dt = ObjRun.GetGroupName();

            if (dt.Rows.Count != 0)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = "Choose GroupName",
                    Value = ""
                });
                foreach (DataRow row in dt.Rows)
                {


                    listItems.Add(new SelectListItem()
                    {
                        Text = row["GroupName"].ToString().Trim(),
                        Value = row["OperatorGroup"].ToString().Trim(),

                    });
                }
            }
            return Json(new MultiSelectList(listItems, "Value", "Text"));
        }

        public JsonResult GetAuthority()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            DataTable dt = new DataTable();
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
            // dt = ObjRun.GetDivision();

           // if (dt.Rows.Count != 0)
           // {
                listItems.Add(new SelectListItem
                {
                    Text = "Choose Authority",
                    Value = ""
                });
                listItems.Add(new SelectListItem
                {
                    Text = "Administrator",
                    Value = "9"
                });
                listItems.Add(new SelectListItem
                {
                    Text = "Employee",
                    Value = "0"
                });


            //}
            return Json(new MultiSelectList(listItems, "Value", "Text"));
        }

        public JsonResult GetActive()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            DataTable dt = new DataTable();
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
            // dt = ObjRun.GetDivision();

            //if (dt.Rows.Count != 0)
            //{
                listItems.Add(new SelectListItem
                {
                    Text = "Choose Active",
                    Value = ""
                });
                listItems.Add(new SelectListItem
                {
                    Text = "Active job",
                    Value = "true"
                });
                listItems.Add(new SelectListItem
                {
                    Text = "Resign",
                    Value = "false"
                });


            //}
            return Json(new MultiSelectList(listItems, "Value", "Text"));
        }



        [HttpGet]
        public ActionResult switchMenu(string param)
        {
            //Your logic, switch or some and return :

            ViewBag.Event = param;



            var asdas = param;
            return PartialView("_partsUserManage/"+ param);
        }


        public JsonResult GetUpdateUserdetail(vewOperatorAlls dataDetail, List<vewOperatorLicense> dataLicenses, string OpNo)
        {
            
            var _Result = "OK";
            var _DataResult = "";
            var _ResultLabel = true;

            //string strIPAddress = httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();
            //var data_ = _sptoDbContext.vewOperatorAll.FirstOrDefault(x => x.OperatorID == opno);
            try
            {
            //    String hostName = string.Empty;
            //hostName = Dns.GetHostName();
            //IPHostEntry myIP = Dns.GetHostEntry(hostName);
            //IPAddress[] address = myIP.AddressList;
             

            var dataOperator = new vewOperatorAlls();
             
            mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
            string[] Result = ObjRun.GetUpdUserdetail(dataDetail, dataLicenses, OpNo, "");

            _ResultLabel = Convert.ToBoolean(Result[0]);
            _Result = Result[1];
            _DataResult = _Result != "OK" ? _Result : "";

            }
            catch(Exception e )
            {
                _ResultLabel = false;
                _Result = "Error";
                _DataResult = e.Message;
            }
            var jsonResult = Json(new
            { strResult = _Result, dataLabel = _DataResult, strboolbel = _ResultLabel, data = "" });

            return jsonResult;


        }


        #endregion

        public IActionResult Load_OperatorAdditional_Detail(string OPID)
        {



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
            return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
        }
 

        public JsonResult GetDivision_Addition()
        {
            List<SelectListItem> listItems = new List<SelectListItem>();

            DataTable dt = new DataTable();
            mgrSQLcommand_Additional ObjRun = new mgrSQLcommand_Additional(_configuration);
            dt = ObjRun.GetDivision_Additional();

            if (dt.Rows.Count != 0)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = "Choose Division",
                    Value = "0"
                });
                foreach (DataRow row in dt.Rows)
                {


                    listItems.Add(new SelectListItem()
                    {
                        Text = row["Division"].ToString().Trim(),
                        Value = row["Division"].ToString().Trim(),

                    });
                }
            }
            return Json(new MultiSelectList(listItems, "Value", "Text"));

        }

        public JsonResult GetDepartment_Addition(string DIV)
        {

            List<SelectListItem> listItems = new List<SelectListItem>();

            DataTable dt = new DataTable();
            mgrSQLcommand_Additional ObjRun = new mgrSQLcommand_Additional(_configuration);
            dt = ObjRun.GetDepartment_Additional(DIV);

            if (dt.Rows.Count != 0)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = "Choose Department",
                    Value = "0"
                });
                foreach (DataRow row in dt.Rows)
                {


                    listItems.Add(new SelectListItem()
                    {
                        Text = row["Department"].ToString().Trim(),
                        Value = row["Department"].ToString().Trim(),

                    });
                }
            }
            return Json(new MultiSelectList(listItems, "Value", "Text"));

        }

        public JsonResult GetSection_Addition(string DIV, string DEP)
        {

            List<SelectListItem> listItems = new List<SelectListItem>();

            DataTable dt = new DataTable();
            mgrSQLcommand_Additional ObjRun = new mgrSQLcommand_Additional(_configuration);
            dt = ObjRun.GetSection_Additional(DIV, DEP);

            if (dt.Rows.Count != 0)
            {
                listItems.Add(new SelectListItem()
                {
                    Text = "Choose Section",
                    Value = "0"
                });
                foreach (DataRow row in dt.Rows)
                {

                    listItems.Add(new SelectListItem()
                    {
                        Text = row["Section"].ToString().Trim(),
                        Value = row["SectionCode"].ToString().Trim(),

                    });
                }
            }
            return Json(new MultiSelectList(listItems, "Value", "Text"));

        }

       

        public IActionResult UserInCharge(string opno)
        {
            ViewBag.opno = opno;
          
            var queryuser = _sptoDbContext.sprOperatorShowListInChang.FromSql($"sprOperatorShowListInChang {opno}").ToList();
            return View(queryuser);
        }
        public async Task<IActionResult> EditUserInCharge(string id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Getuser = await _sptoDbContext.vewOperatorAll.FirstOrDefaultAsync(x => x.OperatorID == id);

            //if (Getuser == null)
            //{
            //    return NotFound();
            //}

            List<string> userdetail =  new List<string>();
            userdetail.Add(Getuser.NameEng);
            userdetail.Add(Getuser.Section);
            userdetail.Add(Getuser.GroupName);
            TempData["Userdetail"] = userdetail;



            List<vewDivisionMaster> CatagoryDiv = new List<vewDivisionMaster>();



            CatagoryDiv = (from vewTSectionMaster in _sptoDbContext.vewT_Section_Master select vewTSectionMaster).ToList();
           

            //Get Section to Dropdown
            var querySection = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == id)
                .Select(c => new { c.OperatorID, c.Section });

            ViewBag.CategorySection = new SelectList(querySection.AsEnumerable(), "OperatorID", "Section");
            //var suppliers = await _context.Suppliers.FindAsync(id);
            //if (suppliers == null)
            //{
            //    return NotFound();
            //}
            return View();
        }

        public IActionResult TempDataExample()
        {
            List<string> mobileList = new List<string>();
            mobileList.Add("Oneplus 8 Pro");
            mobileList.Add("Xperia 1 II");
            mobileList.Add("POCO F2");
            mobileList.Add("Xperia X 10");

            TempData["MobileList"] = mobileList;
            return View();
        }
    }
}