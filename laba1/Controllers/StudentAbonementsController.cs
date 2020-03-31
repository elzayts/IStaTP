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
    public class StudentAbonementsController : Controller
    {
        private readonly MydbContext _context;

        public StudentAbonementsController(MydbContext context)
        {
            _context = context;
        }

        // GET: StudentAbonements
        public async Task<IActionResult> Index(int ? id, string? name)
        { 
            if(id==null)
            {
                var mydbContext = _context.StudentAbonements.Include(s => s.Abon).Include(s => s.Stud);
                return View(await mydbContext.ToListAsync());
            }
            var mydbContext1 = _context.StudentAbonements.Where(c=>c.StudId==id).Include(s => s.Abon).Include(s => s.Stud);
            return View(await mydbContext1.ToListAsync());

        }

        // GET: StudentAbonements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentAbonements = await _context.StudentAbonements
                .Include(s => s.Abon.Type)
                .Include(s => s.Stud)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentAbonements == null)
            {
                return NotFound();
            }

            return View(studentAbonements);
        }

        // GET: StudentAbonements/Create
        public IActionResult Create()
        {
            ViewData["AbonId"] = new SelectList(_context.Abonements, "Id", "Id");
            ViewData["StudId"] = new SelectList(_context.Students, "Id", "Id");
            return View();
        }

        // POST: StudentAbonements/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StudId,AbonId,ActivationDate")] StudentAbonements studentAbonements)
        {
            if (ModelState.IsValid)
            {
                _context.Add(studentAbonements);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AbonId"] = new SelectList(_context.Abonements, "Id", "Id", studentAbonements.AbonId);
            ViewData["StudId"] = new SelectList(_context.Students, "Id", "Id", studentAbonements.StudId);
            return View(studentAbonements);
        }

        // GET: StudentAbonements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentAbonements = await _context.StudentAbonements.FindAsync(id);
            if (studentAbonements == null)
            {
                return NotFound();
            }
            ViewData["AbonId"] = new SelectList(_context.Abonements, "Id", "Id", studentAbonements.AbonId);
            ViewData["StudId"] = new SelectList(_context.Students, "Id", "Id", studentAbonements.StudId);
            return View(studentAbonements);
        }

        // POST: StudentAbonements/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,StudId,AbonId,ActivationDate")] StudentAbonements studentAbonements)
        {
            if (id != studentAbonements.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(studentAbonements);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentAbonementsExists(studentAbonements.Id))
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
            ViewData["AbonId"] = new SelectList(_context.Abonements, "Id", "Id", studentAbonements.AbonId);
            ViewData["StudId"] = new SelectList(_context.Students, "Id", "Id", studentAbonements.StudId);
            return View(studentAbonements);
        }

        // GET: StudentAbonements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var studentAbonements = await _context.StudentAbonements
                .Include(s => s.Abon)
                .Include(s => s.Stud)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (studentAbonements == null)
            {
                return NotFound();
            }

            return View(studentAbonements);
        }

        // POST: StudentAbonements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var studentAbonements = await _context.StudentAbonements.FindAsync(id);
            _context.StudentAbonements.Remove(studentAbonements);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentAbonementsExists(int id)
        {
            return _context.StudentAbonements.Any(e => e.Id == id);
        }
    }
}
