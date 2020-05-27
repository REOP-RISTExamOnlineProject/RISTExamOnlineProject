using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RISTExamOnlineProject.Models.db;

namespace RISTExamOnlineProject.Controllers
{
    public class HomeController : Controller

    {
        private readonly SPTODbContext _sptoDbContext;

        public HomeController(SPTODbContext context)
        {
            _sptoDbContext = context;
        }
        [Authorize(Roles = "Admin, User")]
        public IActionResult Index()
        {
            return View();
        }
     

      
    }
}