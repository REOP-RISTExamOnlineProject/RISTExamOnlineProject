using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}