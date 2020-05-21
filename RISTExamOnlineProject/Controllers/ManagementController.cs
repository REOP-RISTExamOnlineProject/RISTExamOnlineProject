using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
        public IActionResult Index()
        {
            return View();
        }
       
        public IActionResult ManagementUser(string opno)
        {
            ViewBag.opno = opno;
            var data = _sptoDbContext.Operator.Where(x => x.OperatorID == opno).ToList();

            foreach (var itemOperator in data)
            {
                ViewBag.NameEng = itemOperator.NameEng;
            }

            return View(data);
        }









        public IActionResult Training_Record()
        {
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


            var DataShow = (from tempcustomer in _sptoDbContext.Training_Record.Where(x => x.StaffCode == OPID)
                                select tempcustomer);        


            if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDir)))
            {
                DataShow = DataShow.OrderBy(sortColumn + " " + sortColumnDir);

            }


            //if (!string.IsNullOrEmpty(searchValue))
            //{
            //    searchValue = searchValue.ToUpper();
            //    DataShow = DataShow.Where(m => m.QuotationNo.Contains(searchValue) || m.RequesterName.Contains(searchValue) || m.OPID.Contains(searchValue));
            //}

            //total number of rows count     
            recordsTotal = DataShow.Count();
            //Paging     
            var data = DataShow.Skip(skip).Take(pageSize).ToList();
            //Returning Json Data    
            return Json(new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data });

          
        }
    }
}