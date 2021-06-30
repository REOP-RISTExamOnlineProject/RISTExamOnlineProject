using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RISTExamOnlineProject.Models.db;
using X.PagedList;

namespace RISTExamOnlineProject.Controllers
{
    public class ItemCategoryMasterController : Controller
    {
        private readonly SPTODbContext _context;

        public ItemCategoryMasterController(SPTODbContext context)
        {
            _context = context;
        }

        // GET: ItemCategoryMaster
        public async Task<IActionResult> Index(int? page)
        {
            const int padgeSize = 8;

            ViewBag.pageCurrent = page;
            return View(await _context.ItemCategory.ToPagedListAsync(page ?? 1, padgeSize));
        }


        public async Task<IActionResult> AddOrEdit(int? page,int id = 0)
        {
            ViewBag.LicenseTypeGroup = new SelectList(_context.vewLicense_Group, "License_Grp_Nm", "License_Grp_Nm");
            if (id == 0)
                
                return View(new ItemCategoryModel());
            ViewBag.pageCurrent = page;

            //var listtype = (from listtypecroup in _context.vewLicense_Group
            //                    select new SelectListItem()
            //    {
            //        Text = listtypecroup.Group_No.ToString(),
            //        Value = listtypecroup.License_Grp_Nm.ToString(),
            //    }).ToList();

            //listtype.Insert(0, new SelectListItem
            //{
            //    Text = "----Select----",
            //    Value = string.Empty
            //});

            //ViewBag.LicenseTypeGroup = listtype;
            ////ViewBag.LicenseTypeGroup = new SelectList(_context.vewLicense_Group, "Group_No", "License_Grp_Nm");


            var itemCategoryModel = await _context.ItemCategory.FindAsync(id);
            if (itemCategoryModel == null)
            {
                return NotFound();
            }
            return View(itemCategoryModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(int id, [Bind("Nbr,ItemCateg,ItemCategName,ItemCategType,AddDate,UpdDate,UserName,ComputerName")] int? page, ItemCategoryModel itemCategoryModel)
        {
            if (!ModelState.IsValid)
                return Json(new
                    { isValid = false, html = Helper.RenderRazorViewToString(this, "AddOrEdit", itemCategoryModel) });
            //Insert
            if (id == 0)
            {
                itemCategoryModel.AddDate = DateTime.Now;
                //itemCategoryModel.UpdDate = null;
                itemCategoryModel.ComputerName = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                itemCategoryModel.UserName = User.Identity.Name;
                _context.Add(itemCategoryModel);
                await _context.SaveChangesAsync();

            }
            //Update
            else
            {
                try
                {
                    ViewBag.pageCurrent = page;

                    //update spec field EF 
                    _context.ItemCategory.Attach(itemCategoryModel);
                    
                    itemCategoryModel.UpdDate = DateTime.Now;
                    itemCategoryModel.ItemCategName = itemCategoryModel.ItemCategName;
                    itemCategoryModel.ItemCategType = itemCategoryModel.ItemCategType;
                    itemCategoryModel.UserName = User.Identity.Name;
                    itemCategoryModel.ComputerName = Request.HttpContext.Connection.RemoteIpAddress.ToString();
                    _context.Entry(itemCategoryModel).Property(x => x.UpdDate).IsModified = true;
                    _context.Entry(itemCategoryModel).Property(x => x.ItemCategName).IsModified = true;
                    _context.Entry(itemCategoryModel).Property(x => x.ItemCategType).IsModified = true;
                    _context.Entry(itemCategoryModel).Property(x => x.UserName).IsModified = true;
                    _context.Entry(itemCategoryModel).Property(x => x.ComputerName).IsModified = true;
                   


                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemCategoryModelExists(itemCategoryModel.Nbr))
                    { return NotFound(); }

                    throw;
                }
            }
            const int pageSize = 8;
            return Json(new { isValid = true, html = Helper.RenderRazorViewToString(this, "_ViewAll",  await _context.ItemCategory.ToPagedListAsync(page ?? 1, pageSize)) });
        }
        private bool ItemCategoryModelExists(int id)
        {
            return _context.ItemCategory.Any(e => e.Nbr == id);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id,int? page)
        {
            var itemCategoryModel = await _context.ItemCategory.FindAsync(id);
            _context.ItemCategory.Remove(itemCategoryModel);
            await _context.SaveChangesAsync();
            const int pageSize = 8;

         
            return Json(new { html = Helper.RenderRazorViewToString(this, "_ViewAll", await _context.ItemCategory.ToPagedListAsync( page ?? 1, pageSize)) });
        }



        //------------------- 



        public ActionResult ItemCatogoryMaster() {
            return View();
        } 




    }
}
