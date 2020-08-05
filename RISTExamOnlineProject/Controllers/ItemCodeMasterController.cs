using System;
using System.Collections.Generic;
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



        public IActionResult GetCategory()
        {
            mgrSQLcommand_ItemCode ObjRun = new mgrSQLcommand_ItemCode(_configuration);
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems = ObjRun.GetCategory();
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



    }
}