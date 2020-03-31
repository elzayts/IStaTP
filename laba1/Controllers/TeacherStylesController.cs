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
    public class TeacherStylesController : Controller
    {
        private readonly MydbContext _context;

        public TeacherStylesController(MydbContext context)
        {
            _context = context;
        }

        // GET: TeacherStyles
        public async Task<IActionResult> Index(int? id)
        {
            if (id == null)
            {
                var mydbContext = _context.TeacherStyles.Include(t => t.Style).Include(t => t.Teacher);
                return View(await mydbContext.ToListAsync());
            }
          
            var mydbContext1 = _context.TeacherStyles.Where(t => t.TeacherId == id).Include(t => t.Style).Include(t => t.Teacher);
            return View(await mydbContext1.ToListAsync());

        }
        public async Task<IActionResult> Index1(int? id)
        {
            if (id == null)
            {
                var mydbContext = _context.TeacherStyles.Include(t => t.Style).Include(t => t.Teacher);
                return View(await mydbContext.ToListAsync());
            }
           var mydbContext2 = _context.TeacherStyles.Where(t => t.StyleId == id).Include(t => t.Style).Include(t => t.Teacher);
            return View(await mydbContext2.ToListAsync());

        }

        // GET: TeacherStyles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherStyles = await _context.TeacherStyles
                .Include(t => t.Style)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherStyles == null)
            {
                return NotFound();
            }

            return View(teacherStyles);
        }

        // GET: TeacherStyles/Create
        public IActionResult Create()
        {
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Name");
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name");
            return View();
        }

        // POST: TeacherStyles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TeacherId,StyleId")] TeacherStyles teacherStyles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherStyles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Name", teacherStyles.StyleId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name", teacherStyles.TeacherId);
            return View(teacherStyles);
        }

        // GET: TeacherStyles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherStyles = await _context.TeacherStyles.FindAsync(id);
            if (teacherStyles == null)
            {
                return NotFound();
            }
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Name", teacherStyles.StyleId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name", teacherStyles.TeacherId);
            return View(teacherStyles);
        }

        // POST: TeacherStyles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TeacherId,StyleId")] TeacherStyles teacherStyles)
        {
            if (id != teacherStyles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherStyles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherStylesExists(teacherStyles.Id))
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
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Name", teacherStyles.StyleId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name", teacherStyles.TeacherId);
            return View(teacherStyles);
        }

        // GET: TeacherStyles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherStyles = await _context.TeacherStyles
                .Include(t => t.Style)
                .Include(t => t.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherStyles == null)
            {
                return NotFound();
            }

            return View(teacherStyles);
        }

        // POST: TeacherStyles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherStyles = await _context.TeacherStyles.FindAsync(id);
            _context.TeacherStyles.Remove(teacherStyles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherStylesExists(int id)
        {
            return _context.TeacherStyles.Any(e => e.Id == id);
        }
    }
}
