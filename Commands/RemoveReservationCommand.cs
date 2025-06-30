using Hotel_Una_Legacy.Exceptions;
using Hotel_Una_Legacy.Models;
using Hotel_Una_Legacy.Services;
using Hotel_Una_Legacy.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hotel_Una_Legacy.Commands
{
    public class RemoveReservationCommand : AsyncBaseCommand
    {
        private readonly Hotel _hotel;
        private readonly RemoveReservationViewModel _removeReservationViewModel;
        private readonly NavigationService _navigationService;

        public RemoveReservationCommand(Hotel hotel, RemoveReservationViewModel removeReservationViewModel, NavigationService navigationService)
        {
            _hotel = hotel;
            _removeReservationViewModel = removeReservationViewModel;
            _removeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
            _navigationService = navigationService;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_removeReservationViewModel.ReservationContentControl))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _removeReservationViewModel.ReservationContentControl != null;
        }

        public override async Task ExecuteAsync(object parameter)
        {
            try
            {
                IEnumerable<Reservation> reservations = await _hotel.GetReservations();
                Reservation reservation = reservations.FirstOrDefault(r => r.ID == _removeReservationViewModel.ReservationID);
                if (reservation != null)
                {
                    await _hotel.RemoveReservation(reservation);
                }
                else
                {
                    throw new NonExistentReservationException(reservation);
                }
                MessageBox.Show("Rezervacija je uspješno uklonjena", "Uspjeh", MessageBoxButton.OK, MessageBoxImage.Information);
                _navigationService.Navigate();
            }catch (NonExistentReservationException ex)
            {
                MessageBox.Show("Rezervacija nije pronađena", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
