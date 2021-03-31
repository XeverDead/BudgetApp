using Common.Entities;
using DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<BudgetContext>()
                .UseSqlite("Filename=database.db;")
                .Options;

            var cr1 = new CategoryRecord
            {
                Id = 1,
                CategoryId = 1,
                Value = 2,
                Note = "dddd",
                RecordId = 1
            };

            var cr2 = (CategoryRecord)cr1.Clone();

            cr1.Note = "aaaaa";

            Console.WriteLine(cr2.Note);
        }
    }
}
