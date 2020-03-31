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
    public class LessonsController : Controller
    {
        private readonly MydbContext _context;

        public LessonsController(MydbContext context)
        {
            _context = context;
        }

        // GET: Lessons
        public async Task<IActionResult> Index()
        {
            var mydbContext = _context.Lessons.Include(l => l.Style).Include(l => l.StyleNavigation);
            return View(await mydbContext.ToListAsync());
        }

        // GET: Lessons/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
           {
                return NotFound();
            }

            var lessons = await _context.Lessons
                .Include(l => l.Style)
                .Include(l => l.StyleNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessons == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "LessonTeachers", new { id = lessons.Id });
        }

        // GET: Lessons/Create
        public IActionResult Create()
        {
            ViewData["StyleId"] = new SelectList(_context.Classrooms, "Id", "Name");
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Name");
            return View();
        }

        // POST: Lessons/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassroomId,Date,Time,StyleId")] Lessons lessons)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lessons);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StyleId"] = new SelectList(_context.Classrooms, "Id", "Name", lessons.StyleId);
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Name", lessons.StyleId);
            return View(lessons);
        }

        // GET: Lessons/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessons = await _context.Lessons.FindAsync(id);
            if (lessons == null)
            {
                return NotFound();
            }
            ViewData["StyleId"] = new SelectList(_context.Classrooms, "Id", "Name", lessons.StyleId);
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Name", lessons.StyleId);
            return View(lessons);
        }

        // POST: Lessons/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassroomId,Date,Time,StyleId")] Lessons lessons)
        {
            if (id != lessons.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lessons);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonsExists(lessons.Id))
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
            ViewData["StyleId"] = new SelectList(_context.Classrooms, "Id", "Name", lessons.StyleId);
            ViewData["StyleId"] = new SelectList(_context.Styles, "Id", "Name", lessons.StyleId);
            return View(lessons);
        }

        // GET: Lessons/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessons = await _context.Lessons
                .Include(l => l.Style)
                .Include(l => l.StyleNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessons == null)
            {
                return NotFound();
            }

            return View(lessons);
        }

        // POST: Lessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lessons = await _context.Lessons.FindAsync(id);
            _context.Lessons.Remove(lessons);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonsExists(int id)
        {
            return _context.Lessons.Any(e => e.Id == id);
        }
    }
}
