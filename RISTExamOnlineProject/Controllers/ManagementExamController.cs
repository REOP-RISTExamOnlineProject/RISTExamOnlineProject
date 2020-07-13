using System;
using System.Linq;
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
        public async Task<IActionResult> Create(ItemCategoryModel itemCategory)
        {
            if (!ModelState.IsValid) return View();
            itemCategory.UserName = User.Identity.Name;
            itemCategory.AddDate = DateTime.Now;
            itemCategory.UpdDate = DateTime.Now;
            itemCategory.ComputerName = Request.HttpContext.Connection.RemoteIpAddress.ToString();
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
        public async Task<IActionResult> Update_Post(ItemCategoryModel itemCategory)
        {
            if (!ModelState.IsValid) return View();
            itemCategory.UpdDate = DateTime.Now;
            _sptoDbContext.ItemCategory.Update(itemCategory);
            await _sptoDbContext.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        //[HttpPost]
        //[ActionName("Update")]
        
        //public async Task<IActionResult> Update_Post([Bind("ItemCateg,ItemCategName,AddDate,UserName,ComputerName,UpdDate")] ItemCategory itemCategoryUpd)
        //{
        //    //if (!ModelState.IsValid) return View();
        //    //{
        //    //var itemCategoryUpd = new ItemCategory
        //    //{

        //    //    ItemCategName = model.ItemCategName,
        //    //    AddDate = model.AddDate,
        //    //    UserName = User.Identity.Name,
        //    //    ComputerName = Request.HttpContext.Connection.RemoteIpAddress.ToString(),
        //    //    UpdDate = DateTime.Now
        //    //};
        //    //_sptoDbContext.ItemCategory.Update(itemCategoryUpd);
        //    //    await _sptoDbContext.SaveChangesAsync();
        //    //    return RedirectToAction("Index");
        //    //}


        //    //try
        //    //{
        //    //    if (ModelState.IsValid)
        //    //    {
        //    //        bool isNew = !id.HasValue;
        //    //        ItemCategory customer = isNew ? new ItemCategory
        //    //        {
        //    //            AddDate = DateTime.UtcNow
        //    //        } : _sptoDbContext.Set<ItemCategory>().SingleOrDefault(s => s.ItemCateg == id.Value);
        //    //        customer.FirstName = model.FirstName;
        //    //        customer.LastName = model.LastName;
        //    //        customer.MobileNo = model.MobileNo;
        //    //        customer.Email = model.Email;
        //    //        customer.IPAddress = Request.HttpContext.Connection.RemoteIpAddress.ToString();
        //    //        customer.ModifiedDate = DateTime.UtcNow;
        //    //        if (isNew)
        //    //        {
        //    //            context.Add(customer);
        //    //        }
        //    //        context.SaveChanges();
        //    //    }
        //    //}
        //    //catch (Exception ex)
        //    //{
        //    //    throw ex;
        //    //}
        //    //return RedirectToAction("Index");
        //    return View();
        //}
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> AddOrEdit([Bind("EmployeeId,FullName,EmpCode,Position,OfficeLocation")] ItemCategory itemCategory)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        if (itemCategory.ItemCateg == 0)
        //            _context.Add(employee);
        //        else
        //            _context.Update(employee);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(employee);
        //}
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