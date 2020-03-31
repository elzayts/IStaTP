using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using istp_laba1;

namespace istp_laba1.Controllers
{
    public class AbonementsController : Controller
    {
        private readonly MydbContext _context;

        public AbonementsController(MydbContext context)
        {
            _context = context;
        }

        // GET: Abonements
        public async Task<IActionResult> Index()
        {
            var mydbContext = _context.Abonements.Include(a => a.Type);
            return View(await mydbContext.ToListAsync());
        }

        // GET: Abonements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonements = await _context.Abonements
                .Include(a => a.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abonements == null)
            {
                return NotFound();
            }

            return View(abonements);
        }

        // GET: Abonements/Create
        public IActionResult Create()
        {
            ViewData["TypeId"] = new SelectList(_context.AbonementTypes, "Id", "Type");
            return View();
        }

        // POST: Abonements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TypeId")] Abonements abonements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(abonements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["TypeId"] = new SelectList(_context.AbonementTypes, "Id", "Type", abonements.TypeId);
            return View(abonements);
        }

        // GET: Abonements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonements = await _context.Abonements.FindAsync(id);
            if (abonements == null)
            {
                return NotFound();
            }
            ViewData["TypeId"] = new SelectList(_context.AbonementTypes, "Id", "Type", abonements.TypeId);
            return View(abonements);
        }

        // POST: Abonements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TypeId")] Abonements abonements)
        {
            if (id != abonements.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abonements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbonementsExists(abonements.Id))
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
            ViewData["TypeId"] = new SelectList(_context.AbonementTypes, "Id", "Type", abonements.TypeId);
            return View(abonements);
        }

        // GET: Abonements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonements = await _context.Abonements
                .Include(a => a.Type)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abonements == null)
            {
                return NotFound();
            }

            return View(abonements);
        }

        // POST: Abonements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abonements = await _context.Abonements.FindAsync(id);
            var ab_st = _context.StudentAbonements.Where(a => a.AbonId == id).ToArray();
            foreach (var a in ab_st)
            {
                _context.StudentAbonements.Remove(a);
            }
            await _context.SaveChangesAsync();
            _context.Abonements.Remove(abonements);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbonementsExists(int id)
        {
            return _context.Abonements.Any(e => e.Id == id);
        }
    }
}
