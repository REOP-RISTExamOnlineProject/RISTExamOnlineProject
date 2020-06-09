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
    public class ManagementController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly SPTODbContext _sptoDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;


        public ManagementController(SPTODbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {

            _sptoDbContext = context;
            _configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;

        }

        #region UserDetail
        [HttpGet]
        public string GetIP()
        {
            var remoteIpAddress = HttpContext.Connection.RemoteIpAddress;
            return remoteIpAddress.ToString();
        }
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
            var UserName = User.Identity.Name;
            vewOperatorAlls dataOperator = new vewOperatorAlls();
            dataOperator  = _sptoDbContext.vewOperatorAll.FirstOrDefault(x => x.OperatorID == UserName);

            ViewBag.NameEng = dataOperator.NameEng;
            ViewBag.JobTitle = dataOperator.JobTitle;


            ViewBag.NameEngUserDetail = dataOperator.NameEng;
            ViewBag.JobTitleUserDetail = dataOperator.JobTitle;
            var varsd = "http://10.29.1.12/RAJPTrainingControlSystem/PIC/" + UserName + ".jpg";

            ViewBag.imgProfileUserDetail = varsd;
            ViewBag.imgProfile = varsd;

            ViewBag.Event = Event_;
            string IPAddress = "";
            ViewBag.IPAddress = IPAddress; 
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



            ViewBag.NameEngUserDetail = dataOperator.NameEng;
            ViewBag.JobTitleUserDetail = dataOperator.JobTitle;
            var varsd = "http://10.29.1.12/RAJPTrainingControlSystem/PIC/" + opno + ".jpg";
             
            ViewBag.imgProfileUserDetail = varsd;
            var jsonResult = Json(new
            { strResult = _Result, dataLabel = _DataResult, strboolbel = _ResultLabel, data = dataOperator, dataLicense = dataLicenses,DataProfile = varsd });

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
                        Value = row["SectionCode"].ToString().Trim(),

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
                        Value = row["sectionCode"].ToString().Trim(),

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
            ViewBag.Event = param; 
            var asdas = param;
            return PartialView("_partsUserManage/" + param);
        }


        public JsonResult GetUpdateUserdetail(vewOperatorAlls dataDetail, List<vewOperatorLicense> dataLicenses, string OpNo)
        { 
            var _Result = "OK";
            var _DataResult = "";
            var _ResultLabel = true;
            try
            {
                var dataOperator = new vewOperatorAlls(); 
                mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
                string[] Result = ObjRun.GetUpdUserdetail(dataDetail, dataLicenses, OpNo, GetIP()); 
                _ResultLabel = Convert.ToBoolean(Result[0]);
                _Result = Result[1];
                _DataResult = _Result != "OK" ? _Result : "";
            }
            catch (Exception e)
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

        public JsonResult Add_SecstionCode(string OPID, string MakerID,string SecsionCode)
        {


            var jsonResult = Json(new
            { });

            return jsonResult;
        }





      //  [HttpPost]
        public JsonResult GetMakeTemp_Additional(string OPID, string MakerID) {
            mgrSQLcommand_Additional ObjRun = new mgrSQLcommand_Additional(_configuration);

            string Message;

            Message = ObjRun.GetStroeTemp_Additional(OPID, MakerID,"","VEW");


            if (Message == "OK") {

                return Json(new { success = true });
            } else {
                return Json(new { success = false});
            }

            
        }

        [HttpPost]
        public IActionResult DeleteSectionCode_Additional(string OPID, string MakerID, string[] SectionCode)
        {
            mgrSQLcommand_Additional ObjRun = new mgrSQLcommand_Additional(_configuration);

            try
            {
                foreach (string Code in SectionCode)
                {

                    ObjRun.GetStroeTemp_Additional(OPID, MakerID, Code, "DEL");
                }
                              
            }
            catch (Exception ex)
            {

                return Json(new { success = false, responseText = ex.Message.ToString() });
            }


            return Json(new { success = true, responseText = "Delete Data success" });


        }

        [HttpPost]
        public IActionResult AddNewSectionCode_Additional(string OPID, string MakerID, string SectionCode) {

            mgrSQLcommand_Additional ObjRun = new mgrSQLcommand_Additional(_configuration);
            string Message_ = "";
            try
            {
                Message_ = ObjRun.GetStroeTemp_Additional(OPID, MakerID, SectionCode, "ADD");

                if (Message_ == "OK")
                {
                    return Json(new { success = true, responseText = "Add new Section success" });
                }
                else {
                    return Json(new { success = false, responseText = Message_ });
                }                

            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message.ToString() });
            }
            

        }
        [HttpPost]
        public IActionResult Save_Additional(string OPID, string MakerID)
        {

            mgrSQLcommand_Additional ObjRun = new mgrSQLcommand_Additional(_configuration);
            string Message_ = "";
            try
            {
                Message_ = ObjRun.GetStroeTemp_Additional(OPID, MakerID, "", "SVE");

                if (Message_ == "OK")
                {
                    return Json(new { success = true, responseText = "Save data success" });
                }
                else
                {
                    return Json(new { success = false, responseText = Message_ });
                }

            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = ex.Message.ToString() });
            }

        }

        [HttpPost]
        public IActionResult Load_OperatorAdditional_Detail(string OPID)
        {


  //          try
//            {        


            mgrSQLcommand_Additional ObjRun = new mgrSQLcommand_Additional(_configuration);

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

            DataTable dt = new DataTable();

            // var dataShow = _sptoDbContext.vewOperatorAdditionalDep.Where(x => x.OperatorID == OPID).ToList();
            List<vewOperatorAdditionalDepTemp> TempData = new List<vewOperatorAdditionalDepTemp>();


            TempData = ObjRun.GetUserDetail_Additional(OPID);

            var DataShow = (from tempdata in TempData
                            select tempdata);

           // var data = DataShow.ToList();
            var data = DataShow.Skip(skip).Take(pageSize).ToList();
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
         //   return Json(new {draw, recordsFiltered = recordsTotal, recordsTotal, data });
            //}
            //catch (Exception ex)
            //{

            //    return Json(new { success = false, responseText = ex.Message.ToString() });
            //}


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
                        Text = row["SectionCode"].ToString().Trim() + "-"   + row["Section"].ToString().Trim(),
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

            var UserName = User.Identity.Name;
            vewOperatorAlls dataOperator = new vewOperatorAlls();
            dataOperator = _sptoDbContext.vewOperatorAll.FirstOrDefault(x => x.OperatorID == UserName);

            ViewBag.NameEng = dataOperator.NameEng;
            ViewBag.JobTitle = dataOperator.JobTitle;


            ViewBag.NameEngUserDetail = dataOperator.NameEng;
            ViewBag.JobTitleUserDetail = dataOperator.JobTitle;
            var varsd = "http://10.29.1.12/RAJPTrainingControlSystem/PIC/" + UserName + ".jpg";

            ViewBag.imgProfileUserDetail = varsd;
            ViewBag.imgProfile = varsd;



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


            //Get Current Organize
            var queryOrganize = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == id)
                .Select(c => new
                {
                    division = c.Division,
                    department = c.Department,
                    section = c.Section,
                    shift = c.GroupName,
                    statusresign = (c.Active ? "Active" : "Not Active")
                })
              .ToList();

            foreach (var item in queryOrganize)
            {
                ViewBag.CategoryDivision = item.division;
                ViewBag.CategoryDepartment = item.department;
                ViewBag.CategorySection = item.section;
                ViewBag.CategoryShift = item.shift;
                ViewBag.CategoryResign = item.statusresign;

            }

            
            //ViewBag.CategoryDivision = queryDivision;

            //Get Current Department to Dropdown
            //var queryDepartment = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == id)
            //    .Select(c => c.Department)
            //    .FirstOrDefault();
            //ViewBag.CategoryDepartment = queryDepartment;

            //Get Current Section to Dropdown
            //var querySection = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == id)
            //    .Select(c => new { c.OperatorID, c.Section });
            //ViewBag.CategorySection = new SelectList(querySection.AsEnumerable(), "OperatorID", "Section");

            //Get Current Shift to Dropdown
            //var queryShift = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == id)
            //    .Select(c => new { c.OperatorID, c.GroupName });
            //ViewBag.CategoryShift = new SelectList(queryShift.AsEnumerable(), "OperatorID", "GroupName");

            //Get Current License to Dropdown
            var queryLicense = _sptoDbContext.vewOperatorLicense.Where(x => x.OperatorID == id)
                .Select(c => new { c.OperatorID, c.License });
            ViewBag.CategoryLicense = new MultiSelectList(queryLicense.AsEnumerable(), "OperatorID", "License");

            //Get Current Resign to Dropdown
            //var queryResign = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == id)
            //    .Select(s => new {StatusActive = (s.Active ? "Active" : "Not Active")})
            //    .Select(c=>c.StatusActive)
            //    .FirstOrDefault();

            //   // .Select(c => new { c.OperatorID, c.Active });


            //   ViewBag.CategoryResign = queryResign;

            List<SelectListItem> categoryResign = new List<SelectListItem>() {
                new SelectListItem {
                    Text = "Active", Value = "True"
                },
                new SelectListItem {
                    Text = "Not Active", Value = "False"
                }
            };
            ViewBag.ResignMaster = categoryResign;

            //Binding for select dropdownlist shift
            var shiftmaster = _sptoDbContext.vewOperatorGroupMaster
                .Select(c => new {c.Shift, c.GroupName}).ToList();
            ViewBag.CategoryShiftmaster = new SelectList(shiftmaster.AsEnumerable(), "Shift", "GroupName");


            //Binding for select dropdownlist Division
            List<vewDivisionMaster> catagoryDivlist = new List<vewDivisionMaster>();

            catagoryDivlist = _sptoDbContext.vewDivisionMaster.ToList();


            // ------- Inserting Select Item in Division List -------
            catagoryDivlist.Insert(0, new vewDivisionMaster() { row_num = 0, DivisionName = "Select" });
            ViewBag.listofCatagoryDiv = catagoryDivlist;

            //Binding for select dropdownlist License
            var licensecatagory = _sptoDbContext.vewLicenseMaster.ToList();
            ViewBag.licensecatagory = new MultiSelectList(licensecatagory.AsEnumerable(), "LicenseID", "License");
           
            var queryAdditional = _sptoDbContext.vewOperatorAdditionalDep.Where(x => x.OperatorID == id)
                .Select(c => new
                {
                    c.Division,
                    c.Department,
                    c.Section,
                    c.SectionCode
                });

           
           
            //var employeeModel = new List<EmployeeModel>();

            //employeeModel.Add(new EmployeeModel() { EmpID = 101, EmpFirstName = "Surya", EmpLastName = "Kranthi" });
            //employeeModel.Add(new EmployeeModel() { EmpID = 102, EmpFirstName = "Aditya", EmpLastName = "Roy" });
            //employeeModel.Add(new EmployeeModel() { EmpID = 103, EmpFirstName = "Ravi", EmpLastName = "Kanth" });
            //employeeModel.Add(new EmployeeModel() { EmpID = 104, EmpFirstName = "Anshuman", EmpLastName = "Sagara" });
            //employeeModel.Add(new EmployeeModel() { EmpID = 105, EmpFirstName = "Jhansi", EmpLastName = "Naari" });

            ////Example 1 Using ViewBag
            //ViewBag.EmployeeList1 = employeeModel;

            ViewBag.AdditionalCurrent = queryAdditional.AsEnumerable() ;


            var userName = User.Identity.Name;
            vewOperatorAlls dataOperator = new vewOperatorAlls();
            dataOperator = _sptoDbContext.vewOperatorAll.FirstOrDefault(x => x.OperatorID == userName);

            ViewBag.NameEng = dataOperator.NameEng;
            ViewBag.JobTitle = dataOperator.JobTitle;


            ViewBag.NameEngUserDetail = dataOperator.NameEng;
            ViewBag.JobTitleUserDetail = dataOperator.JobTitle;
            var varsd = "http://10.29.1.12/RAJPTrainingControlSystem/PIC/" + userName + ".jpg";

            ViewBag.imgProfileUserDetail = varsd;
            ViewBag.imgProfile = varsd;

            
           


            return View();

           


        }

        public JsonResult GetDepartmentCategory(long row_num)
        {
            List<vewDepartmentMaster> subCategorylist = new List<vewDepartmentMaster>();

            // ------- Getting Data from Database Using EntityFrameworkCore -------
           
            subCategorylist = _sptoDbContext.vewDepartmentMaster
                .Where(x => x.row_num == row_num)
                .GroupBy(g => new
                {
                    department = g.Department,
                    Row_num = g.row_num,
                    Row_dept_id = g.row_dept_id
                })
                .Select(s => new vewDepartmentMaster()
                {
                    Department = s.Key.department,
                    row_num = s.Key.Row_num,
                    row_dept_id = s.Key.Row_dept_id
                }).ToList();




            // ------- Inserting Select Item in List -------
            //subCategorylist.Insert(0, new SubCategory { SubCategoryID = 0, SubCategoryName = "Select" });
            subCategorylist.Insert(0, new vewDepartmentMaster() { row_dept_id = 0, Department = "Select" });

            return Json(new SelectList(subCategorylist, "row_dept_id", "Department"));
        }


        public JsonResult GetSectionCategory(long row_dept_id)
        {
            List<vewSectionMaster> sectionList = new List<vewSectionMaster>();

            // ------- Getting Data from Database Using EntityFrameworkCore -------

            sectionList = _sptoDbContext.vewSectionMaster
                .Where(x => x.row_dept_id == row_dept_id).ToList();
            //productList = (from product in _context.MainProduct
            //    where product.SubCategoryID == SubCategoryID
            //    select product).ToList();

            // ------- Inserting Select Item in List -------
            //productList.Insert(0, new MainProduct { ProductID = 0, ProductName = "Select" });
            sectionList.Insert(0, new vewSectionMaster() { SectionCodeID = "0", Section = "Select" });
            return Json(new SelectList(sectionList, "SectionCodeID", "Section"));
        }





        //    public IActionResult TempDataExample()
        //    {
        //        mgrSQLcommand_Additional ObjRun = new mgrSQLcommand_Additional(_configuration);


        //        DataTable dt = new DataTable();




        //        dt = ObjRun.GetUserDetail_Additional("000702");

        //        List<string> mobileList = new List<string>();
        //        string Strdata = "";

        //        if (dt.Rows.Count != 0)
        //        {


        //            for (int i = 0; i > dt.Rows.Count; i++) {

        //                Strdata += "{[ data:";
        //                if (i != dt.Rows.Count)
        //                {

        //                    Strdata += "{},";


        //                }
        //                else {
        //                    Strdata += "{}";

        //                }

        //                Strdata += "]}";



        //            }


        //            foreach (DataRow row in dt.Rows)
        //            {
        //                mobileList.Add("" + row["OperatorID"].ToString() + "," + row["SectionCode"].ToString() + "," +
        //                    " " + row["Division"].ToString() + ", " + row["Department"].ToString() + "," + row["Section"].ToString() + " ");

        //            }

        //        }


        //        TempData["MobileList"] = mobileList;
        //        return View();
        //    }
    }
}