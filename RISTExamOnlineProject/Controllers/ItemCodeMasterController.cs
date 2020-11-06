using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.SqlServer.Query.ExpressionTranslators.Internal;
using Microsoft.Extensions.Configuration;
using RISTExamOnlineProject.Models.db;
using RISTExamOnlineProject.Models.TSQL;

namespace RISTExamOnlineProject.Controllers
{
    public class ItemCodeMasterController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SPTODbContext _sptoDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;


        public ItemCodeMasterController(SPTODbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {

            _sptoDbContext = context;
            _configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;

        }
        public IActionResult ItemCode_Management()
        {
            return View();
        }



        public IActionResult GetCategory(string ItemCategType)
        {
            mgrSQLcommand_ItemCode ObjRun = new mgrSQLcommand_ItemCode(_configuration);
            List<SelectListItem> listItems = new List<SelectListItem>();
          string Strsql = " SELECT [ItemCateg]+'-'+[ItemCategName]  as [ItemCategName],[ItemCateg] FROM[SPTOSystem].[dbo].[ItemCategory] where ItemCategType = '" + ItemCategType.Trim()+"'group by[ItemCateg],[ItemCategName] order by ItemCateg asc";

            listItems = ObjRun.GetItemDropDownList(Strsql,"Category");
            //  listItems = ObjRun.GetCategory(ItemCategType);
            return Json(new SelectList(listItems, "Value", "Text"));

        }

        public IActionResult GetCategType()
        {
            mgrSQLcommand_ItemCode ObjRun = new mgrSQLcommand_ItemCode(_configuration);
            List<SelectListItem> listItems = new List<SelectListItem>();

         string Strsql = "  SELECT ItemCategType as TextValues ,ItemCategType as Values_  from [SPTOSystem].[dbo].[ItemCategory]  group by ItemCategType order by ItemCategType asc";
            listItems = ObjRun.GetItemDropDownList(Strsql, "Type");
            return Json(new SelectList(listItems, "Value", "Text"));

        }



        public JsonResult ItemCode_TableDetail(string ItemCateg) {

            //            {        
            mgrSQLcommand_ItemCode ObjRun = new mgrSQLcommand_ItemCode(_configuration);

            List<ItemCode_Detail> DataShow = new List<ItemCode_Detail>();

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


            DataShow = ObjRun.Get_ItemCode_TableDetail(ItemCateg);


            // var DataShow = _sptoDbContext.vewOperatorReqChange.Where(x => x.ChangeOperatorID == "").ToList();

            // var dataShow = _sptoDbContext.vewOperatorAdditionalDep.Where(x => x.OperatorID == OPID).ToList();

            var data = DataShow.Skip(skip).Take(pageSize).ToList();
            //  return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });
            return Json(new { draw, recordsFiltered = recordsTotal, recordsTotal, data });
            //}
            //catch (Exception ex)
            //{

            //    return Json(new { success = false, responseText = ex.Message.ToString() });
            //}
        }

        public JsonResult DeleteItem(int Nbr,string OPID) {
            string ms;
            string IP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            mgrSQLcommand_ItemCode ObjRun = new mgrSQLcommand_ItemCode(_configuration);
            ms = ObjRun.Itemcode_Management("del", "", "", "", 0, Nbr, IP, OPID,"","");
            if (ms.Trim() == "OK")
            {
                return Json(new { success = true });

            }
            else
            {

                return Json(new { success = false, ms = ms });

            }

        }



       
        public JsonResult Item_management(string Job, string ItemCateg ,string ItemCode, string ItemName, int time ,
            int Nbr,string OPID,string ValueCodeQuestion,string ValueCodeAnswer) {
            string ms;
            string IP = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            mgrSQLcommand_ItemCode ObjRun = new mgrSQLcommand_ItemCode(_configuration);
            ms = ObjRun.Itemcode_Management(Job, ItemCateg, ItemCode, ItemName, time, Nbr, IP, OPID, ValueCodeQuestion, ValueCodeAnswer);
            if (ms.Trim() == "OK")
            {
                return Json(new { success = true });

            }
            else {

                return Json(new { success = false ,ms =ms});

            }



           
        }

    }
}