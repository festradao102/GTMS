using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GTMS.Data;
using GTMS.Models;

namespace GTMS.Controllers
{
    public class RefereeController : Controller
    {
        private readonly GtmsDbContext _context;

        public RefereeController(GtmsDbContext context)
        {
            _context = context;
        }

        // GET: Referee
        public async Task<IActionResult> Index()
        {
            return View(await _context.Referees.ToListAsync());
        }

        // GET: Referee/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referee = await _context.Referees
                .FirstOrDefaultAsync(m => m.RefereeID == id);
            if (referee == null)
            {
                return NotFound();
            }

            return View(referee);
        }

        // GET: Referee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Referee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("RefereeID,Identification,Name,LastName,Age,Height,Weight,Position")] Referee referee)
        {
            if (ModelState.IsValid)
            {
                _context.Add(referee);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(referee);
        }

        // GET: Referee/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referee = await _context.Referees.FindAsync(id);
            if (referee == null)
            {
                return NotFound();
            }
            return View(referee);
        }

        // POST: Referee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("RefereeID,Identification,Name,LastName,Age,Height,Weight,Position")] Referee referee)
        {
            if (id != referee.RefereeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(referee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RefereeExists(referee.RefereeID))
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
            return View(referee);
        }

        // GET: Referee/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var referee = await _context.Referees
                .FirstOrDefaultAsync(m => m.RefereeID == id);
            if (referee == null)
            {
                return NotFound();
            }

            return View(referee);
        }

        // POST: Referee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var referee = await _context.Referees.FindAsync(id);
            _context.Referees.Remove(referee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RefereeExists(int id)
        {
            return _context.Referees.Any(e => e.RefereeID == id);
        }
    }
}
