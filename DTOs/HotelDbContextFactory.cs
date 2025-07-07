using Hotel_Una_Legacy.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.DTOs
{
    public class HotelDbContextFactory
    {
        private readonly string _connectionString;
        public HotelDbContextFactory(string connectionString)
        {
            _connectionString = connectionString;
        }
        public HotelDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<HotelDbContext>();
            options.UseSqlite(_connectionString);
            return new HotelDbContext(options.Options);
        }
    }
}
