using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RISTExamOnlineProject.Models.db;

namespace RISTExamOnlineProject.Controllers
{
    public class ItemCategoryMaster : Controller
    {
        private readonly SPTODbContext _context;

        public ItemCategoryMaster(SPTODbContext context)
        {
            _context = context;
        }

        // GET: ItemCategoryMaster
        public async Task<IActionResult> Index()
        {
            return View(await _context.ItemCategory.ToListAsync());
        }

        // GET: ItemCategoryMaster/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCategoryModel = await _context.ItemCategory
                .FirstOrDefaultAsync(m => m.ItemCateg == id);
            if (itemCategoryModel == null)
            {
                return NotFound();
            }

            return View(itemCategoryModel);
        }

        // GET: ItemCategoryMaster/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}
        private bool TransactionModelExists(string id)
        {
            return _context.ItemCategory.Any(e => e.ItemCateg == id);
        }

        //[Helper.NoDirectAccessAttribute]
        public async Task<IActionResult> AddOrEdit(string id = null)
        {
            if (id == null)
                return View(new ItemCategoryModel());
            var itemCategoryModel = await _context.ItemCategory.FindAsync(id);
            if (itemCategoryModel == null)
            {
                return NotFound();
            }
            return View(itemCategoryModel);
        }

        // POST: ItemCategoryMaster/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddOrEdit(string id, [Bind("ItemCateg,ItemCategName,AddDate,UpdDate,UserName,ComputerName")] ItemCategoryModel itemCategoryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemCategoryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemCategoryModel);
        }

        // GET: ItemCategoryMaster/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCategoryModel = await _context.ItemCategory.FindAsync(id);
            if (itemCategoryModel == null)
            {
                return NotFound();
            }
            return View(itemCategoryModel);
        }

        // POST: ItemCategoryMaster/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ItemCateg,ItemCategName,AddDate,UpdDate,UserName,ComputerName")] ItemCategoryModel itemCategoryModel)
        {
            if (id != itemCategoryModel.ItemCateg)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemCategoryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemCategoryModelExists(itemCategoryModel.ItemCateg))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(itemCategoryModel);
        }

        // GET: ItemCategoryMaster/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemCategoryModel = await _context.ItemCategory
                .FirstOrDefaultAsync(m => m.ItemCateg == id);
            if (itemCategoryModel == null)
            {
                return NotFound();
            }

            return View(itemCategoryModel);
        }

        // POST: ItemCategoryMaster/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var itemCategoryModel = await _context.ItemCategory.FindAsync(id);
            _context.ItemCategory.Remove(itemCategoryModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemCategoryModelExists(string id)
        {
            return _context.ItemCategory.Any(e => e.ItemCateg == id);
        }
    }
}
