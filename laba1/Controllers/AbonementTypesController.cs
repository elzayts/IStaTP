using System;
using System.IO;
using ClosedXML.Excel;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using istp_laba1;
using Microsoft.AspNetCore.Http;

namespace istp_laba1.Controllers
{
    public class AbonementTypesController : Controller
    {
        private readonly MydbContext _context;

        public AbonementTypesController(MydbContext context)
        {
            _context = context;
        }

        // GET: AbonementTypes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AbonementTypes.ToListAsync());
        }

        // GET: AbonementTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonementTypes = await _context.AbonementTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abonementTypes == null)
            {
                return NotFound();
            }

            return View(abonementTypes);
        }

        // GET: AbonementTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AbonementTypes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] AbonementTypes abonementTypes)
        {
            if (ModelState.IsValid)
            {
                _context.Add(abonementTypes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(abonementTypes);
        }

        // GET: AbonementTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonementTypes = await _context.AbonementTypes.FindAsync(id);
            if (abonementTypes == null)
            {
                return NotFound();
            }
            return View(abonementTypes);
        }

        // POST: AbonementTypes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] AbonementTypes abonementTypes)
        {
            if (id != abonementTypes.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(abonementTypes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AbonementTypesExists(abonementTypes.Id))
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
            return View(abonementTypes);
        }

        // GET: AbonementTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var abonementTypes = await _context.AbonementTypes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (abonementTypes == null)
            {
                return NotFound();
            }

            return View(abonementTypes);
        }

        // POST: AbonementTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var abonementTypes = await _context.AbonementTypes.FindAsync(id);
            var ab_st = _context.StudentAbonements.Where(a => a.Abon.TypeId == id).ToArray();
            foreach (var a in ab_st)
            {
                _context.StudentAbonements.Remove(a);
            }
            await _context.SaveChangesAsync();

            var abon = _context.Abonements.Where(a => a.TypeId == id).ToArray();
            foreach (var a in abon)
            {
                _context.Abonements.Remove(a);
            }
            await _context.SaveChangesAsync();
            _context.AbonementTypes.Remove(abonementTypes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AbonementTypesExists(int id)
        {
            return _context.AbonementTypes.Any(e => e.Id == id);
        }

      /*  [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Import(IFormFile fileExcel)
        {
            if (ModelState.IsValid)
            {
                if (fileExcel != null)
                {
                    using (var stream = new FileStream(fileExcel.FileName, FileMode.Create))
                    {
                        await fileExcel.CopyToAsync(stream);
                        using XLWorkbook workBook = new XLWorkbook(stream,
            XLEventTracking.Disabled);
                        {
                            //перегляд усіх листів (в даному випадку категорій)
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {

                                //перегляд усіх рядків                    
                                foreach (IXLRow row in worksheet.RowsUsed())
                                {
                                    try
                                    {
                                       
                                        //у разі наявності автора знайти його, у разі відсутності - додати

                                       
                                            AbonementTypes abtype;
                                            var a = (from abtyp in _context.AbonementTypes
                                                     where abtyp.Type.Contains(row.Cell(4).Value.ToString())
                                                     select abtyp).ToList();
                                            if (a.Count > 0)
                                            {
                                                abtype = a[0];
                                            }

                                            else
                                            {
                                                abtype = new AbonementTypes();
                                                abtype.Type = row.Cell(4).Value.ToString();
                                                //додати в контекст
                                                _context.AbonementTypes.Add(abtype);
                                            }
                                    }

                                   catch (Exception e)
                                    {
                                   

                                    }
                                }
                            }

                        }
                    }
                }
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }*/
    }
}
