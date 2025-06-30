using Hotel_Una_Legacy.Models;
using Hotel_Una_Legacy.ViewModels;
using Hotel_Una_Legacy.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hotel_Una_Legacy.Commands
{
    public class SearchCommand : AsyncBaseCommand
    {
        private RemoveReservationViewModel _removeReservationViewModel;
        private UpdateReservationViewModel _updateReservationViewModel;
        private readonly Hotel _hotel;
        public RemoveReservationViewModel RemoveReservationView
        {
            get => _removeReservationViewModel;
            set
            {
                _removeReservationViewModel = value;
            }
        }
        public UpdateReservationViewModel UpdateReservationViewModel
        {
            get => _updateReservationViewModel;
            set
            {
                _updateReservationViewModel = value;
            }
        }
        public SearchCommand(Hotel hotel, RemoveReservationViewModel removeReservationViewModel)
        {
            _hotel = hotel;
            _removeReservationViewModel = removeReservationViewModel;
            _removeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
            
        }
        public SearchCommand(Hotel hotel, UpdateReservationViewModel updateReservationViewModel)
        {
            _hotel = hotel;
            _updateReservationViewModel = updateReservationViewModel;
            _updateReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override bool CanExecute(object parameter)
        {
            if(_removeReservationViewModel != null)
            {
                return _removeReservationViewModel.ReservationID > 0;
            }
            else if(_updateReservationViewModel != null)
            {
                return _updateReservationViewModel.ReservationID > 0;
            }
            return false;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            int reservationID = 0;
            IEnumerable<Reservation> reservations = await _hotel.GetReservations();
            Reservation reservation;
            if (_removeReservationViewModel != null)
            {
                reservationID = _removeReservationViewModel.ReservationID;
                reservation = reservations.FirstOrDefault(r => r.ID == _removeReservationViewModel.ReservationID);
            }
            else
            {
                reservationID = _updateReservationViewModel.ReservationID;
                reservation = reservations.FirstOrDefault(r => r.ID == _updateReservationViewModel.ReservationID);
            }

            if (reservation != null)
            {
                if (_removeReservationViewModel != null)
                {
                    _removeReservationViewModel.ReservationContentControl = new ReservationViewModel(reservation).GetReservationDataContentControl();
                }
                else
                {
                    _updateReservationViewModel.RoomNum = reservation.RoomNum;
                    _updateReservationViewModel.FirstName = reservation.FirstName;
                    _updateReservationViewModel.LastName = reservation.LastName;
                    _updateReservationViewModel.StartDate = reservation.StartDate;
                    _updateReservationViewModel.EndDate = reservation.EndDate;
                    _updateReservationViewModel.NumberOfGuests = reservation.NumberOfGuests;

                    _updateReservationViewModel.ReservationInputContentControl = new ReservationViewModel(reservation).GetReservationInputContentControl();
                }
            }
            else
            {
                MessageBox.Show("Rezervacija nije pronađena", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_updateReservationViewModel.ReservationID))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
