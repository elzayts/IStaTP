using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using istatp_lab2.Models;

namespace XUnitTestlab2
{   
    public class RoomsControllerTest :DbContext
    {
        protected RoomsControllerTest(DbContextOptions<Lab2Context> contextOptions)
        {
            ContextOptions = contextOptions;
            Seed();
        }


        protected DbContextOptions<Lab2Context> ContextOptions { get; }
        private void Seed()
        {
            using (var context = new Lab2Context(ContextOptions))
            {
                context.Database.EnsureDeleted();
                context.Database.EnsureCreated();

                var one = new Rooms { RoomID = 1, Description = "oll", Number = 100, Price = 5 };

                var two = new Rooms { RoomID = 2, Description = "fgdhl", Number = 200, Price = 50 };

                var three = new Rooms { RoomID = 3, Description = "room", Number = 300, Price = 500 };

                context.AddRange(one, two, three);

                context.SaveChanges();
            }
        }

    }
   
}
