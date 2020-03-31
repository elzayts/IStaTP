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
    public class StylesController : Controller
    {
        private readonly MydbContext _context;

        public StylesController(MydbContext context)
        {
            _context = context;
        }

        // GET: Styles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Styles.ToListAsync());
        }

        // GET: Styles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var styles = await _context.Styles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (styles == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index1", "TeacherStyles", new { id = styles.Id});
        }

        // GET: Styles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Styles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Styles styles)
        {
            if (ModelState.IsValid)
            {
                _context.Add(styles);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(styles);
        }

        // GET: Styles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var styles = await _context.Styles.FindAsync(id);
            if (styles == null)
            {
                return NotFound();
            }
            return View(styles);
        }

        // POST: Styles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Styles styles)
        {
            if (id != styles.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(styles);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StylesExists(styles.Id))
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
            return View(styles);
        }

        // GET: Styles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var styles = await _context.Styles
                .FirstOrDefaultAsync(m => m.Id == id);
            if (styles == null)
            {
                return NotFound();
            }

            return View(styles);
        }

        // POST: Styles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var styles = await _context.Styles.FindAsync(id);
            var ts = _context.TeacherStyles.Where(st => st.StyleId == id).ToList();
            foreach(var a in ts)
            {
                _context.Remove(a);
            }
            await _context.SaveChangesAsync();
            _context.Styles.Remove(styles);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StylesExists(int id)
        {
            return _context.Styles.Any(e => e.Id == id);
        }
    }
}
