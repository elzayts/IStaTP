using System;
using Xunit;
using Microsoft.AspNetCore.Mvc;
using istatp_lab2.Models;
using istatp_lab2.Controllers;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Linq;

namespace XUnitTestlab2
{
    public class RoomControllerTest : RoomsControllerTest, IDisposable
    {
        private readonly DbConnection _connection;

        public RoomControllerTest()
            : base(
                new DbContextOptionsBuilder<Lab2Context>()
                    .UseSqlite(CreateInMemoryDatabase())
                    .Options)
        {
            _connection = RelationalOptionsExtension.Extract(ContextOptions).Connection;
        }

        private static DbConnection CreateInMemoryDatabase()
        {
            var connection = new SqliteConnection("Filename=:memory:");

            connection.Open();

            return connection;
        }

        public void Dispose() => _connection.Dispose();

        [Fact]
        public void Can_get_items()
        {
            using (var context = new Lab2Context(ContextOptions))
            {
                var controller = new RoomsController(context);

                var items = controller.GetRooms().ToList();

                Assert.Equal(3, items.Count);
                Assert.Equal(2, items[1].RoomID);
                Assert.Equal(500, items[2].Price);
              
            }
        }

        [Fact]
        public void Can_get_item()
        {
            using (var context = new Lab2Context(ContextOptions))
            {
                var controller = new RoomsController(context);

                var item = controller.GetRooms(1);

                Assert.Equal("oll", item.Description);
            }
        }
    }
}
