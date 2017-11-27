using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FoodReport.DAL.Models;

namespace FoodReport.Models
{
    public class FoodReportContext : DbContext
    {
        public FoodReportContext (DbContextOptions<FoodReportContext> options)
            : base(options)
        {
        }

        public DbSet<FoodReport.DAL.Models.Report> Report { get; set; }
    }
}
