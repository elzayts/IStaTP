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
    public class LessonTeachersController : Controller
    {
        private readonly MydbContext _context;

        public LessonTeachersController(MydbContext context)
        {
            _context = context;
        }

        // GET: LessonTeachers
        public async Task<IActionResult> Index(int ? id)
        {
            if(id==null)
            {
                var mydbContext = _context.LessonTeacher.Include(l => l.Lesson).Include(l => l.Teacher);
                return View(await mydbContext.ToListAsync());
            }
            var mydbContext1 = _context.LessonTeacher.Where(c=>c.LessonId==id).Include(l => l.Lesson).Include(l => l.Teacher);
            return View(await mydbContext1.ToListAsync());
        }

        // GET: LessonTeachers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessonTeacher = await _context.LessonTeacher
                .Include(l => l.Lesson)
                .Include(l => l.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessonTeacher == null)
            {
                return NotFound();
            }

            return View(lessonTeacher);
        }

        // GET: LessonTeachers/Create
        public IActionResult Create()
        {
         
                ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "Id");
                ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name");
                return View();
        }

        // POST: LessonTeachers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,LessonId,TeacherId")] LessonTeacher lessonTeacher)
        {
            if (ModelState.IsValid)
            {
                _context.Add(lessonTeacher);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "Id", lessonTeacher.LessonId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name", lessonTeacher.TeacherId);
            return View(lessonTeacher);
        }

        // GET: LessonTeachers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessonTeacher = await _context.LessonTeacher.FindAsync(id);
            if (lessonTeacher == null)
            {
                return NotFound();
            }
            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "Id", lessonTeacher.LessonId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name", lessonTeacher.TeacherId);
            return View(lessonTeacher);
        }

        // POST: LessonTeachers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,LessonId,TeacherId")] LessonTeacher lessonTeacher)
        {
            if (id != lessonTeacher.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(lessonTeacher);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LessonTeacherExists(lessonTeacher.Id))
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
            ViewData["LessonId"] = new SelectList(_context.Lessons, "Id", "Id", lessonTeacher.LessonId);
            ViewData["TeacherId"] = new SelectList(_context.Teachers, "Id", "Name", lessonTeacher.TeacherId);
            return View(lessonTeacher);
        }

        // GET: LessonTeachers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessonTeacher = await _context.LessonTeacher
                .Include(l => l.Lesson)
                .Include(l => l.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (lessonTeacher == null)
            {
                return NotFound();
            }

            return View(lessonTeacher);
        }

        // POST: LessonTeachers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lessonTeacher = await _context.LessonTeacher.FindAsync(id);
            _context.LessonTeacher.Remove(lessonTeacher);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LessonTeacherExists(int id)
        {
            return _context.LessonTeacher.Any(e => e.Id == id);
        }
    }
}
