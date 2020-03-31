using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ClosedXML.Excel;
using istp_laba1;

namespace istp_laba1.Controllers
{
    public class StudentsController : Controller
    {
        private readonly MydbContext _context;

        public StudentsController(MydbContext context)
        {
            _context = context;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }
        public async Task<IActionResult> Abonements(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var st = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (st == null)
            {
                return NotFound();
            }

            return RedirectToAction("Index", "StudentAbonements", new { id = st.Id, name=st.Name });
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ProfileDescription,Name,Photo")] Students students)
        {
            if (ModelState.IsValid)
            {
                _context.Add(students);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(students);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students.FindAsync(id);
            if (students == null)
            {
                return NotFound();
            }
            return View(students);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ProfileDescription,Name,Photo")] Students students)
        {
            if (id != students.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(students);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentsExists(students.Id))
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
            return View(students);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var students = await _context.Students
                .FirstOrDefaultAsync(m => m.Id == id);
            if (students == null)
            {
                return NotFound();
            }

            return View(students);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var students = await _context.Students.FindAsync(id);
            var ab_st = _context.StudentAbonements.Where(a => a.StudId== id).ToArray();
            
            foreach (var a in ab_st)
            {
                int ab_id = a.AbonId;
                var abon = _context.Abonements.Find(ab_id);
                _context.StudentAbonements.Remove(a);
                await _context.SaveChangesAsync();
                _context.Abonements.Remove(abon);
                await _context.SaveChangesAsync();
            }
            _context.Students.Remove(students);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentsExists(int id)
        {
            return _context.Students.Any(e => e.Id == id);
        }

        [HttpPost]
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
                        using (XLWorkbook workBook = new XLWorkbook(stream,
            XLEventTracking.Disabled))
                        {
       
                            foreach (IXLWorksheet worksheet in workBook.Worksheets)
                            {
                  
                                foreach (IXLRow row in worksheet.RowsUsed().Skip(1))
                                {
                                    try
                                    {
                                        Students stud = new Students();
                                        stud.Name = row.Cell(1).Value.ToString();
                                        stud.Photo = row.Cell(2).Value.ToString();
                                        stud.ProfileDescription = row.Cell(3).Value.ToString();
                                        _context.Students.Add(stud);

                                            if (row.Cell(4).Value.ToString().Length >0)
                                            {
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
                                                    _context.AbonementTypes.Add(abtype);
                                                }

                                                Abonements ab = new Abonements();
                                                ab.Type = abtype;
                                                ab.TypeId = abtype.Id;
                                                _context.Abonements.Add(ab);

                                                string dateString = row.Cell(5).Value.ToString();
                                                DateTime dateFromString =
                                                DateTime.Parse(dateString, System.Globalization.CultureInfo.InvariantCulture);
                                             
                                                StudentAbonements st = new StudentAbonements();
                                                st.Stud = stud;
                                                st.Abon = ab;
                                                st.AbonId = ab.Id;
                                                st.StudId = stud.Id;
                                                st.ActivationDate = dateFromString;
                                                _context.StudentAbonements.Add(st);
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
        }

        public ActionResult Export()
        {
            using (XLWorkbook workbook = new XLWorkbook(XLEventTracking.Disabled))
            {
              
                    var worksheet = workbook.Worksheets.Add("Students");
                    worksheet.Cell("A1").Value = "Name";
                    worksheet.Cell("B1").Value ="Surname" ;
                    worksheet.Cell("C1").Value = "Profile description";
                worksheet.Cell("D1").Value = "Abonement type";
                worksheet.Cell("F1").Value = "Abonement type";
                worksheet.Cell("H1").Value = "Abonement type";
                worksheet.Cell("E1").Value = "Activation date";
                worksheet.Cell("G1").Value = "Activation date";
                worksheet.Cell("I1").Value = "Activation date";
                worksheet.Column("E").Width = 15;
                worksheet.Column("G").Width = 15;
                worksheet.Column("I").Width = 15;
                worksheet.Row(1).Style.Font.Bold = true;
                var stud = _context.Students.ToList();
                for (int i = 0; i < stud.Count; i++)
                    {
                        worksheet.Cell(i + 2, 1).Value = stud[i].Name;
                        worksheet.Cell(i + 2, 2).Value = stud[i].Photo;
                        worksheet.Cell(i + 2, 3).Value = stud[i].ProfileDescription;
                    var stab = _context.StudentAbonements.Where(a => a.StudId ==
             stud[i].Id).Include(b=>b.Abon.Type).ToList();
                        int j = 1;

                        foreach (var a in stab)
                        {
                            if (j <4)
                            {
                            worksheet.Cell(i + 2, j + 3).Value = a.Abon.Type.Type;
                            worksheet.Cell(i + 2, j + 4).Value = a.ActivationDate;
                            j =j+2;
                            }
                        }
                    }
                
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    stream.Flush();
                    return new FileContentResult(stream.ToArray(),
                      "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet")
                        {
                        FileDownloadName = $"studentsList_{ DateTime.UtcNow.ToShortDateString()}.xlsx"
                    };
                }
            }
        }
    }
}
