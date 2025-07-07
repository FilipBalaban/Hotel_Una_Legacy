using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.DatabaseContext
{
    public class DesignTimeHotelDbContextFactory : IDesignTimeDbContextFactory<HotelDbContext>
    {
        public HotelDbContext CreateDbContext(string[] args)
        {
            var options = new DbContextOptionsBuilder<HotelDbContext>();
            options.UseSqlite("Data Source=HotelUna.db");
            return new HotelDbContext(options.Options);
        }
    }
}
