using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using hw_core.Data;
using hw_core.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace hw_core.Controllers
{
    [Authorize]
    public class PortfolioCategoriesAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PortfolioCategoriesAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PortfolioCategoriesAdmin
        public async Task<IActionResult> Index()
        {
            return View(await _context.PortfolioCategories.ToListAsync());
        }

        // GET: PortfolioCategoriesAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioCategory = await _context.PortfolioCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolioCategory == null)
            {
                return NotFound();
            }

            return View(portfolioCategory);
        }

        // GET: PortfolioCategoriesAdmin/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: PortfolioCategoriesAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] PortfolioCategory portfolioCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(portfolioCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(portfolioCategory);
        }

        // GET: PortfolioCategoriesAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioCategory = await _context.PortfolioCategories.FindAsync(id);
            if (portfolioCategory == null)
            {
                return NotFound();
            }
            return View(portfolioCategory);
        }

        // POST: PortfolioCategoriesAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] PortfolioCategory portfolioCategory)
        {
            if (id != portfolioCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolioCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioCategoryExists(portfolioCategory.Id))
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
            return View(portfolioCategory);
        }

        // GET: PortfolioCategoriesAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolioCategory = await _context.PortfolioCategories
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolioCategory == null)
            {
                return NotFound();
            }

            return View(portfolioCategory);
        }

        // POST: PortfolioCategoriesAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portfolioCategory = await _context.PortfolioCategories.FindAsync(id);
            _context.PortfolioCategories.Remove(portfolioCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioCategoryExists(int id)
        {
            return _context.PortfolioCategories.Any(e => e.Id == id);
        }
    }
}
