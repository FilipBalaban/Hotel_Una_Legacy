using Hotel_Una_Legacy.DatabaseContext;
using Hotel_Una_Legacy.DTOs;
using Hotel_Una_Legacy.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.Services.ReservationManagers
{
    public class DatabaseReservationManager
    {
        private readonly HotelDbContextFactory _hotelDbContextFactory;
        public DatabaseReservationManager(HotelDbContextFactory hotelDbContextFactory)
        {
            _hotelDbContextFactory = hotelDbContextFactory;
        }
        public async Task AddReservation(Reservation reservation)
        {
            using (HotelDbContext dbContext = _hotelDbContextFactory.CreateDbContext())
            {
                dbContext.Add(ToReservationDTO(reservation));
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            using (HotelDbContext dbContext = _hotelDbContextFactory.CreateDbContext())
            {

                IEnumerable<ReservationDTO> reservationDTOs = await dbContext.Reservations.ToListAsync();
                return reservationDTOs.Select(r => ToReservation(r));
            }
        }
        public async Task RemoveReservation(Reservation reservation)
        {
            using (HotelDbContext dbContext = _hotelDbContextFactory.CreateDbContext())
            {
                dbContext.Remove(ToReservationDTO(reservation, reservation.ID));
                await dbContext.SaveChangesAsync();
            }
        }
        public async Task UpdateReservation(Reservation newReservation)
        {
            using (HotelDbContext dbContext = _hotelDbContextFactory.CreateDbContext())
            {
                dbContext.Update(ToReservationDTO(newReservation, newReservation.ID));
                await dbContext.SaveChangesAsync();
            }
        }
        private Reservation ToReservation(ReservationDTO dto)
        {
            return new Reservation(dto.ID, dto.RoomNum, dto.FirstName, dto.LastName, dto.StartDate, dto.EndDate, dto.NumberOfGuests);
        }
        private ReservationDTO ToReservationDTO(Reservation reservation, int id = 0)
        {
            if (id > 0)
            {
                return new ReservationDTO
                {
                    ID = id,
                    RoomNum = reservation.RoomNum,
                    FirstName = reservation.FirstName,
                    LastName = reservation.LastName,
                    StartDate = reservation.StartDate,
                    EndDate = reservation.EndDate,
                    NumberOfGuests = reservation.NumberOfGuests,
                };
            }
            return new ReservationDTO
            {
                RoomNum = reservation.RoomNum,
                FirstName = reservation.FirstName,
                LastName = reservation.LastName,
                StartDate = reservation.StartDate,
                EndDate = reservation.EndDate,
                NumberOfGuests = reservation.NumberOfGuests,
            };
        }
    }
}
