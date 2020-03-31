using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace istp_laba1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChartsController : ControllerBase
    {
        private readonly MydbContext _context;

        public ChartsController(MydbContext context)
        {
            _context = context;
        }

        [HttpGet("JsonData")]
        public JsonResult JsonData()
        {
            var abonTypes = _context.AbonementTypes.Include(a => a.Abonements).ToList();
            List<object> abt = new List<object>();
            abt.Add(new[] { "Abonement Type", "Amount of abonements" });
            foreach(var a in abonTypes)
            {
                abt.Add(new object[] { a.Type, a.Abonements.Count() });
                Console.WriteLine(a);
            }

            return new JsonResult(abt);
        }

        [HttpGet("JsonData1")]
        public JsonResult JsonData1()
        {
            var teacst = _context.Classrooms.Include(a => a.Lessons).ToList();
            List<object> abt = new List<object>();
            abt.Add(new[] { "Classroom", "Amount of lessons" });
            foreach (var a in teacst)
            {
                abt.Add(new object[] { a.Name, a.Lessons.Count() });
                Console.WriteLine(a);
            }

            return new JsonResult(abt);
        }
    }
}