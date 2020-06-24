using System;
using System.Collections.Generic;
using System.Data;
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
    public class ExamController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SPTODbContext _sptoDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;


        public ExamController(SPTODbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {

            _sptoDbContext = context;
            _configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;

        }



        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Examination_Modal()
        {

            return View();


        }

        public IActionResult Examination()
        { 
        
        return View();
        
        
        }


        public IActionResult GetHTML(string ItemCateg, string ItemCode) {


            mgrSQLcommand_Exam ObjRun = new mgrSQLcommand_Exam(_configuration);

            string HTMLTEXT = ObjRun.GetExamHTML(ItemCateg, ItemCode);

            return Json(new { success = true , HTMLTEXT = HTMLTEXT });

        }

        public IActionResult GetCategory()
        {

            mgrSQLcommand_Exam ObjRun = new mgrSQLcommand_Exam(_configuration);
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems = ObjRun.GetCategory();
            return Json(new MultiSelectList(listItems, "Value", "Text"));

        }
        

        public IActionResult GetExamname(string Category)
        {

            mgrSQLcommand_Exam ObjRun = new mgrSQLcommand_Exam(_configuration);
            List<SelectListItem> listItems = new List<SelectListItem>();
            listItems = ObjRun.GetExamName(Category);
            return Json(new MultiSelectList(listItems, "Value", "Text"));


        }
        


    }
}