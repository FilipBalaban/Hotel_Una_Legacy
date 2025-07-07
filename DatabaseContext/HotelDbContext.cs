using Hotel_Una_Legacy.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.DatabaseContext
{
    public class HotelDbContext: DbContext
    {
        public HotelDbContext(DbContextOptions options): base(options)
        {
            
        }
        public DbSet<ReservationDTO> Reservations { get; set; }
    }
}
