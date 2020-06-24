using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RISTExamOnlineProject.Models.db;
using RISTExamOnlineProject.Models.TSQL;
using X.PagedList;
namespace RISTExamOnlineProject.Controllers
{
    public class ManagementController : Controller
    {

        private readonly IConfiguration _configuration;
        private readonly SPTODbContext _sptoDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;


        public ManagementController(SPTODbContext context, IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {

            _sptoDbContext = context;
            _configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;

        }

        #region UserDetail

        [HttpGet]
        public string GetIp()
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
                .Select(c => new {c.OperatorID, c.JobTitle});
            ViewBag.CategoryPosition = new SelectList(queryPosition.AsEnumerable(), "OperatorID", "JobTitle");

            //Get Division to Dropdown
            var queryDivision = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno)
                .Select(c => new {c.OperatorID, c.Division});
            ViewBag.CategoryDivision = new SelectList(queryDivision.AsEnumerable(), "OperatorID", "Division");

            //Get Department to Dropdown
            var queryDepartment = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno)
                .Select(c => new {c.OperatorID, c.Department});
            ViewBag.CategoryDepartment = new SelectList(queryDepartment.AsEnumerable(), "OperatorID", "Department");

            //Get Section to Dropdown
            var querySection = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno)
                .Select(c => new {c.OperatorID, c.Section});
            ViewBag.CategorySection = new SelectList(querySection.AsEnumerable(), "OperatorID", "Section");

            //Get Shift to Dropdown
            var queryShift = _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opno)
                .Select(c => new {c.OperatorID, c.GroupName});
            ViewBag.CategoryShift = new SelectList(queryShift.AsEnumerable(), "OperatorID", "GroupName");

            //Get License to Dropdown
            var queryLicense = _sptoDbContext.vewOperatorLicense.Where(x => x.OperatorID == opno)
                .Select(c => new {c.OperatorID, c.License});
            ViewBag.CategoryLicense = new MultiSelectList(queryLicense.AsEnumerable(), "OperatorID", "License");


            return View(data);

        }

        [Authorize]
        public IActionResult UserDetailMaintenance(string Event)
        {

            var Event_ = Event == null ? "_partsUserInfo" : Event;
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
            {
                strResult = _Result, dataLabel = _DataResult, strboolbel = _ResultLabel, data = dataOperator,
                dataLicense = dataLicenses, DataProfile = varsd
            });

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


        public JsonResult GetUpdateUserdetail(vewOperatorAlls dataDetail, List<vewOperatorLicense> dataLicenses,
            string OpNo)
        {
            var _Result = "OK";
            var _DataResult = "";
            var _ResultLabel = true;
            try
            {
                var dataOperator = new vewOperatorAlls();
                mgrSQLcommand ObjRun = new mgrSQLcommand(_configuration);
                string[] Result = ObjRun.GetUpdUserdetail(dataDetail, dataLicenses, OpNo, GetIp());
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
                {strResult = _Result, dataLabel = _DataResult, strboolbel = _ResultLabel, data = ""});
            return jsonResult;
        }


        #endregion

        public JsonResult Add_SecstionCode(string OPID, string MakerID, string SecsionCode)
        {


            var jsonResult = Json(new
                { });

            return jsonResult;
        }





        //  [HttpPost]
        public JsonResult GetMakeTemp_Additional(string OPID, string MakerID)
        {
            mgrSQLcommand_Additional ObjRun = new mgrSQLcommand_Additional(_configuration);

            string Message;

            Message = ObjRun.GetStroeTemp_Additional(OPID, MakerID, "", "VEW");


            if (Message == "OK")
            {

                return Json(new {success = true});
            }

            return Json(new {success = false});


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

                return Json(new {success = false, responseText = ex.Message.ToString()});
            }


            return Json(new {success = true, responseText = "Delete Data success"});


        }

        [HttpPost]
        public IActionResult AddNewSectionCode_Additional(string OPID, string MakerID, string SectionCode)
        {

            mgrSQLcommand_Additional ObjRun = new mgrSQLcommand_Additional(_configuration);
            string Message_ = "";
            try
            {
                Message_ = ObjRun.GetStroeTemp_Additional(OPID, MakerID, SectionCode, "ADD");

                if (Message_ == "OK")
                {
                    return Json(new {success = true, responseText = "Add new Section success"});
                }
                else
                {
                    return Json(new {success = false, responseText = Message_});
                }

            }
            catch (Exception ex)
            {
                return Json(new {success = false, responseText = ex.Message.ToString()});
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
                    return Json(new {success = true, responseText = "Save data success"});
                }
                else
                {
                    return Json(new {success = false, responseText = Message_});
                }

            }
            catch (Exception ex)
            {
                return Json(new {success = false, responseText = ex.Message.ToString()});
            }

        }

        [HttpPost]
        public IActionResult Load_OperatorAdditional_Detail(string OPID)
        {

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
            return Json(new {draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data});
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
                        Text = row["SectionCode"].ToString().Trim() + "-" + row["Section"].ToString().Trim(),
                        Value = row["SectionCode"].ToString().Trim(),

                    });
                }
            }

            return Json(new MultiSelectList(listItems, "Value", "Text"));

        }



        public async Task<IActionResult> UserInCharge(int? page,string opno)
        {
            ViewBag.opno = opno;

            if (ViewBag.opno == null)
            {
                opno = TempData["opno"].ToString();
            }

            const int padgeSize = 5;
           
            var queryuser =  _sptoDbContext.sprOperatorShowListInChang.FromSql($"sprOperatorShowListInChang {opno}").ToList().ToPagedList(page ?? 1, padgeSize);
            //var tempval = TempData.Peek<TempReqChange>("datareqchange");


            // Get data list user request
            var queryvalue = await _sptoDbContext.TempReqChange.Where(x => x.ReqOperatorID == opno & x.SendReqFlag == false).ToListAsync();


            if (queryvalue.Count != 0)
            {
               
                ViewBag.Collection = queryvalue;

            }

            

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
        
        public async Task<IActionResult> EditUserInCharge(int? pageaddition, string opnoedit)
        {
            //if (opnoedit == null)
            //{
            //    return NotFound();
            //}

            //var sectadditionid = HttpContext.Request.Form["sectListAdditional"].ToString();



            var Getuser = await _sptoDbContext.vewOperatorAll.FirstOrDefaultAsync(x => x.OperatorID == opnoedit);

            if (Getuser != null)
            {
                ViewBag.opnoreq = Getuser.OperatorID;
                ViewBag.namereq = Getuser.NameEng;

            }
           

            //Get Current Organize
            var queryOrganize = await _sptoDbContext.vewOperatorAll.Where(x => x.OperatorID == opnoedit)
                .Select(c => new
                {
                    division = c.Division,
                    department = c.Department,
                    section = c.Section,
                    shift = c.GroupName,
                    statusresign = (c.Active ? "Active" : "Not Active")
                })
                .ToListAsync();

            foreach (var item in queryOrganize)
            {
                ViewBag.CategoryDivision = item.division;
                ViewBag.CategoryDepartment = item.department;
                ViewBag.CategorySection = item.section;
                ViewBag.CategoryShift = item.shift;
                ViewBag.CategoryResign = item.statusresign;

            }



            //Get Current License to Dropdown
            var queryLicense = await _sptoDbContext.vewOperatorLicense.Where(x => x.OperatorID == opnoedit)
                .Select(c => new {c.OperatorID, c.License}).ToListAsync();
            ViewBag.CategoryLicense = new MultiSelectList(queryLicense.AsEnumerable(), "OperatorID", "License");



            List<SelectListItem> categoryResign = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "Active", Value = "True"
                },
                new SelectListItem
                {
                    Text = "Not Active", Value = "False"
                }
            };
            ViewBag.ResignMaster = categoryResign;

            //Binding for select dropdownlist shift
            var shiftmaster = await _sptoDbContext.vewOperatorGroupMaster
                .Select(c => new {c.OperatorGroup, c.GroupName}).ToListAsync();

            ViewBag.CategoryShiftmaster = new SelectList(shiftmaster.AsEnumerable(), "OperatorGroup", "GroupName");


            //Binding for select dropdownlist Division


            var catagoryDivlist = await _sptoDbContext.vewDivisionMaster
                .Select(v => new
                {
                    DivisionID = v.row_num,
                    Divisionname = v.DivisionName
                }).ToListAsync();



            // ------- Inserting Select Item in Division List -------
            
            ViewBag.listofCatagoryDiv = new SelectList(catagoryDivlist, "DivisionID", "Divisionname");


            //Binding for select dropdownlist License
            var licensecatagory = await _sptoDbContext.vewLicenseMaster
                .Select(v => new {License = v.License.ToString().Trim(), v.LicenseID}).ToListAsync();

            ViewBag.licensecatagory = new MultiSelectList(licensecatagory.AsEnumerable(), "License", "License");

            
            const int padgeSizeAddition = 5;
            var listadditional = await _sptoDbContext.vewOperatorAdditionalDep
                .Where(x => x.OperatorID == opnoedit)
                .Select(s => new additionalist()
                {
                    Division = s.Division,
                    Department = s.Department,
                    Section = s.Section,
                    SectionCode = s.SectionCode
                }).ToPagedListAsync(pageaddition ?? 1, padgeSizeAddition);
            ViewBag.opnoedit = opnoedit;
            ViewBag.AdditionalCurrent = listadditional;


            // Div. Additional 
            var divlistAdditional = await _sptoDbContext.vewDivisionMaster
                .Select(v => new
                {
                    DivisionID = v.row_num,
                    Divisionname = v.DivisionName
                }).ToListAsync();



            // ------- Inserting Select Item in Division List -------

            ViewBag.listofDivAdditional = new SelectList(divlistAdditional, "DivisionID", "Divisionname");



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

        //GetDepartment Category
        public async Task<JsonResult> GetDepartmentCategory(long divisionList)
        {
            // ------- Getting Data from Database Using EntityFrameworkCore -------

            
           List<vewDepartmentMaster> departmentCategory = await _sptoDbContext.vewDepartmentMaster
                 .Where(x => x.row_num == divisionList)
                 .GroupBy(g => new { g.row_dept_id, g.Department })
                 .Select(g => new vewDepartmentMaster()
                 {
                     DepartmentID = g.Key.row_dept_id,
                     Departmentname = g.Key.Department
                 })
                 .ToListAsync();
           // ------- Inserting Select Item in List -------
           departmentCategory.Insert(0, new vewDepartmentMaster() {row_dept_id = 0, Departmentname = "Select" });
           
            return Json(new SelectList(departmentCategory, "DepartmentID", "Departmentname"));
        }


        public async Task<JsonResult> GetSectionCategory(long departmentList)
        {
            // ------- Getting Data from Database Using EntityFrameworkCore -------

            var sectionList = await _sptoDbContext.vewSectionMaster
                .Where(x => x.row_dept_id == departmentList).ToListAsync();

            // ------- Inserting Select Item in List -------
            sectionList.Insert(0, new vewSectionMaster {SectionCodeID = "-", Section = "Select"});
            return Json(new SelectList(sectionList, "SectionCodeID", "Section"));
        }


        //GetDepartment Category additional
        public async Task<JsonResult> GetDeptlistadditional(long divListAddition)
        {
            // ------- Getting Data from Database Using EntityFrameworkCore -------
            List<vewDepartmentMaster> deptlistqurey = await _sptoDbContext.vewDepartmentMaster
                .Where(x => x.row_num == divListAddition)
                .GroupBy(g => new { g.row_dept_id, g.Department })
                .Select(g => new vewDepartmentMaster
                {
                    DepartmentID = g.Key.row_dept_id,
                    Departmentname = g.Key.Department
                })
                .ToListAsync();
            // ------- Inserting Select Item in List -------
            deptlistqurey.Insert(0, new vewDepartmentMaster() { row_dept_id = 0, Departmentname = "Select" });

            return Json(new SelectList(deptlistqurey, "DepartmentID", "Departmentname"));
        }
        //GetSection Category additional
        public async Task<JsonResult> GetSectlistadditional(long deptListAdditional)
        {
            // ------- Getting Data from Database Using EntityFrameworkCore -------

            var sectlistquery = await _sptoDbContext.vewSectionMaster
                .Where(x => x.row_dept_id == deptListAdditional).ToListAsync();

            // ------- Inserting Select Item in List -------
            sectlistquery.Insert(0, new vewSectionMaster { SectionCodeID = "-", Section = "Select" });
            return Json(new SelectList(sectlistquery, "SectionCodeID", "Section"));
        }


       
        public async Task<JsonResult> GetAdditionValue(string sectListAdditional,string opnovalue)
        {
            //string[] additionList = { };
            if (sectListAdditional != null)
            {
                var listAddition = new TempListAddition()
                {

                    OPNO = opnovalue, //opno for change
                    SectionSelect = sectListAdditional, //sectioncode change
                    FlagSend = false // Senq Req Flag
                };
                _sptoDbContext.TempListAddition.Add(listAddition);
                _sptoDbContext.SaveChanges();
            }
            //else
            //{

            //}
            //var opnovalue = HttpContext.Request.Form["opnoreq"].ToString();



            var listAdditionSelect = await _sptoDbContext.TempListAddition
                .Where(x => x.OPNO == opnovalue && x.FlagSend == false).ToListAsync();

            if (listAdditionSelect.Count != 0)
            {
                ViewBag.AdditionValue = listAdditionSelect;
            }

            // return Json(new SelectList(listAdditionSelect, "SectionCodeID", "Section"));
            return Json( ViewBag.AdditionValue);
        }
        public IActionResult UserReqeustInqury()
        {

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



            return View();
        }

        [HttpPost]
        public IActionResult EditUserInChargeSubmit()
        {
            var sectioncodeid = HttpContext.Request.Form["SectionCodeID"].ToString();
            var shiftchange = HttpContext.Request.Form["Shift"].ToString();
            var licenselist = HttpContext.Request.Form["LicenseList"].ToString().Replace(",", ";");
            var resignstatus = HttpContext.Request.Form["ResignMaster"].ToString();
            var opnoreq = HttpContext.Request.Form["opnoreq"].ToString().Trim();
            var selectAddition = HttpContext.Request.Form["ValueSect1"].ToString();


           

            var datareqchange = new TempReqChange()
            {
                
                OperatorID = opnoreq, //opno for change
                SectionCode = sectioncodeid, //sectioncode change
                Shift = shiftchange, // shift for change
                License = licenselist, // license list for change
                ReqOperatorID = User.Identity.Name, // opno request
                ResignStatus = resignstatus,  // resign status
                SectionAttribute = selectAddition,
                DateReq = DateTime.Now, // date request
                SendReqFlag = false // Send Req Flag
            };
            _sptoDbContext.TempReqChange.Add(datareqchange);
            _sptoDbContext.SaveChanges();

            
            
            TempData["opno"] = User.Identity.Name;
           
            return RedirectToAction("UserInCharge", "Management", new { opno = TempData["opno"] });
            
        }

        public IActionResult TestTempdata()
        {
            if (TempData["CurrTime"] != null)
            {
                string tempkeep = TempData["CurrTime"].ToString();
          
                TempData["CurrTime"] = DateTime.Now;
            }
            
            return View();
        }

       
        public async Task<ActionResult> SaveForm( string opno)
        {
            // Get data list user request
            var queryvalue = await _sptoDbContext.TempReqChange.Where(x => x.ReqOperatorID == opno & x.SendReqFlag == false).ToListAsync();


            if (queryvalue.Count != 0)
            {


                //Get Running No.
                string runningno;
                var progdesc = new SqlParameter("Progdesc", "OPUPD");
                var runningNo = new SqlParameter("RunningNo", "")
                {
                    Direction = ParameterDirection.Output,
                    SqlDbType = SqlDbType.NChar
                };


                var sql = "EXEC sprRunningNo @Progdesc,@RunningNo = @RunningNo OUTPUT SELECT @RunningNo as 'RunningNo'";
                var queryRunningNo = await _sptoDbContext.sprRunningNo.FromSql(sql,progdesc, runningNo).FirstOrDefaultAsync();
               
                var resultDocNo = queryRunningNo.RunningNo.Trim();
                // Initialization.  
               
                foreach (var item in queryvalue)
                {




                    // Settings.  
                    var flag = new SqlParameter("@Flag", "Add");
                    var docNo = new SqlParameter("@DocNo", resultDocNo);
                    //var flag = new SqlParameter("@Flag", "ADD"){}
                    //var docNo = new SqlParameter("@DocNo", "TE-XXXX65");
                    var operatorId = new SqlParameter("@OperatorID",item.OperatorID.Trim());
                    var sectionCode = new SqlParameter("@SectionCode", item.SectionCode.Trim());
                    var sectionAttribute = new SqlParameter("@SectionAttribute",item.SectionAttribute.Trim());
                    var operatorGroup = new SqlParameter("@OperatorGroup",item.Shift.Trim());
                    var license = new SqlParameter("@License", item.License.Trim());
                    var active = new SqlParameter("@Active",item.ResignStatus.Trim());
                    var reqOperatorId = new SqlParameter("@ReqOperatorID",item.ReqOperatorID.Trim());
                    var changeOperatorId = new SqlParameter("@ChangeOperatorID","");


               
                    var sqlsprOperatorReqChange = "EXEC sprOperatorReqChange @Flag, @DocNo, @OperatorID, @SectionCode, @SectionAttribute, @OperatorGroup, @License, @Active, @ReqOperatorID, @ChangeOperatorID";
                    var lst = await _sptoDbContext.Query<sprOperatorReqChange>().FromSql(sqlsprOperatorReqChange, flag,
                        docNo, operatorId, sectionCode, sectionAttribute, operatorGroup, license, active, reqOperatorId,
                        changeOperatorId).ToListAsync();

                  
                }


                var list = await _sptoDbContext.TempReqChange.ToListAsync();
                _sptoDbContext.TempReqChange.RemoveRange(list);
                await _sptoDbContext.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));

            }
            //ViewBag.DataReq = dataCollection;
            return RedirectToAction("UserInCharge", "Management", new {opno});
        }

    }
}