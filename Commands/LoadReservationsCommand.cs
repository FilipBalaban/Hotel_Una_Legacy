using Hotel_Una_Legacy.Models;
using Hotel_Una_Legacy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.Commands
{
    public class LoadReservationsCommand : AsyncBaseCommand
    {
        private readonly ReservationOverviewViewModel _reservationOverviewViewModel;
        private readonly CalendarViewModel _calendarViewModel;
        private readonly Hotel _hotel;
        public LoadReservationsCommand(ReservationOverviewViewModel reservationOverviewViewModel, Hotel hotel)
        {
            _reservationOverviewViewModel = reservationOverviewViewModel;
            _hotel = hotel;
        }
        public LoadReservationsCommand(CalendarViewModel calendarViewModel, Hotel hotel)
        {
            _calendarViewModel = calendarViewModel;
            _hotel = hotel;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            IEnumerable<Reservation> reservations = await _hotel.GetReservations();
            if(_reservationOverviewViewModel != null)
            {
                _reservationOverviewViewModel.LoadReservations(reservations);
                return;
            }
            _calendarViewModel?.LoadReservations(reservations);
        }
    }
}
