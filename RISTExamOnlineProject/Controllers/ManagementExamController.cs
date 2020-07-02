using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RISTExamOnlineProject.Models.db;

namespace RISTExamOnlineProject.Controllers
{
    public class ManagementExamController : Controller
    {
        private readonly SPTODbContext _sptoDbContext;

        public ManagementExamController(SPTODbContext context)
        {
            _sptoDbContext = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _sptoDbContext.ItemCategory.ToListAsync());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Create")]
        public async Task<IActionResult> Create_Post(ItemCategory itemCategory)
        {
            if (!ModelState.IsValid) return View();
            await _sptoDbContext.ItemCategory.AddAsync(itemCategory);
            await _sptoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");

        }

        public async Task<IActionResult> Update(string id)
        {
            return View(await _sptoDbContext.ItemCategory.FirstOrDefaultAsync(a => a.ItemCateg == id));
        }

        [HttpPost]
        [ActionName("Update")]
        public async Task<IActionResult> Update_Post(ItemCategory itemCategory)
        {
            _sptoDbContext.ItemCategory.Update(itemCategory);
            await _sptoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //[HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var queryItemCategory = await _sptoDbContext.ItemCategory.FirstOrDefaultAsync(x => x.ItemCateg == id);
            _sptoDbContext.ItemCategory.Remove(queryItemCategory);
            await _sptoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}