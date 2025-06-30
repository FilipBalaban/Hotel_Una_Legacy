using Hotel_Una_Legacy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.Services.ReservationManagers
{
    public interface IReservationManager
    {
        Task AddReservation(Reservation reservation);
        Task RemoveReservation(Reservation reservation);
        Task UpdateReservation(Reservation newReservation);
        Task<IEnumerable<Reservation>> GetReservations();
    }
}
