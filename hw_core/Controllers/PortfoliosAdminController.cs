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
    public class PortfoliosAdminController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PortfoliosAdminController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: PortfoliosAdmin
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Portfolios.Include(p => p.Category);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: PortfoliosAdmin/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _context.Portfolios
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }

        // GET: PortfoliosAdmin/Create
        public IActionResult Create()
        {
            ViewData["PortfolioCategory_id"] = new SelectList(_context.PortfolioCategories, "Id", "Id");
            return View();
        }

        // POST: PortfoliosAdmin/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Body,Status,PortfolioCategory_id")] Portfolio portfolio)
        {
            if (ModelState.IsValid)
            {
                _context.Add(portfolio);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PortfolioCategory_id"] = new SelectList(_context.PortfolioCategories, "Id", "Id", portfolio.PortfolioCategory_id);
            return View(portfolio);
        }

        // GET: PortfoliosAdmin/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio == null)
            {
                return NotFound();
            }
            ViewData["PortfolioCategory_id"] = new SelectList(_context.PortfolioCategories, "Id", "Id", portfolio.PortfolioCategory_id);
            return View(portfolio);
        }

        // POST: PortfoliosAdmin/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Body,Status,PortfolioCategory_id")] Portfolio portfolio)
        {
            if (id != portfolio.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(portfolio);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PortfolioExists(portfolio.Id))
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
            ViewData["PortfolioCategory_id"] = new SelectList(_context.PortfolioCategories, "Id", "Id", portfolio.PortfolioCategory_id);
            return View(portfolio);
        }

        // GET: PortfoliosAdmin/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var portfolio = await _context.Portfolios
                .Include(p => p.Category)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (portfolio == null)
            {
                return NotFound();
            }

            return View(portfolio);
        }

        // POST: PortfoliosAdmin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            _context.Portfolios.Remove(portfolio);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PortfolioExists(int id)
        {
            return _context.Portfolios.Any(e => e.Id == id);
        }
    }
}
