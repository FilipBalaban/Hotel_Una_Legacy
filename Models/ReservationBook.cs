using Hotel_Una_Legacy.Exceptions;
using Hotel_Una_Legacy.Services.ReservationManagers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.Models
{
    public class ReservationBook
    {
        private readonly DatabaseReservationManager _databaseReservationManager;

        public ReservationBook(DatabaseReservationManager databaseReservationManager)
        {
            _databaseReservationManager = databaseReservationManager;
        }
        public async Task AddReservation(Reservation reservation)
        {
            IEnumerable<Reservation> reservations = await _databaseReservationManager.GetReservations();
            foreach (Reservation existingReservation in reservations)
            {
                if (existingReservation.CausesConflicts(reservation))
                {
                    throw new ReservationConflictsException(existingReservation, reservation);
                }
            }
            await _databaseReservationManager.AddReservation(reservation);
        }
        public async Task RemoveReservation(Reservation reservation)
        {
            await _databaseReservationManager.RemoveReservation (reservation);
        }
        public async Task UpdateReservation(Reservation newReservation)
        {
            IEnumerable<Reservation> reservations = await _databaseReservationManager.GetReservations();

            if (!reservations.Any(r => r.ID == newReservation.ID))
            {
                throw new NonExistentReservationException(newReservation);
            }
            foreach (Reservation existingReservation in reservations)
            {
                if (newReservation.CausesConflicts(existingReservation))
                {
                    throw new ReservationConflictsException(existingReservation, newReservation);
                }
            }
            await _databaseReservationManager.UpdateReservation(newReservation);
        }
        public async Task<IEnumerable<Reservation>> GetReservations()
        {
            return await _databaseReservationManager.GetReservations();
        }
    }
}
