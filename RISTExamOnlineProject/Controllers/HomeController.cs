using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using RISTExamOnlineProject.Models.db;
using System.Linq;

namespace RISTExamOnlineProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly SPTODbContext _sptoDbContext;
        private readonly IHttpContextAccessor httpContextAccessor;
        public HomeController(SPTODbContext context, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        { 
            _sptoDbContext = context;
            _configuration = configuration;
            this.httpContextAccessor = httpContextAccessor;
        }
        [Authorize]
        public IActionResult Index()
        {
            var UserName = User.Identity.Name;
            vewOperatorAlls dataOperator = new vewOperatorAlls();
            dataOperator = _sptoDbContext.vewOperatorAll.FirstOrDefault(x => x.OperatorID == UserName);
            ViewBag.NameEng = dataOperator.NameEng;
            ViewBag.JobTitle = dataOperator.JobTitle;
            ViewBag.imgProfile = "";
            return View();
        }

       
    }
}