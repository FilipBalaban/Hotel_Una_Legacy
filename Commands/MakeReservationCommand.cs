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
    public class MakeReservationCommand : AsyncBaseCommand
    {
        private readonly Hotel _hotel;
        private readonly MakeReservationViewModel _makeReservationViewModel;
        private readonly NavigationService _navigationService;
        public MakeReservationCommand(Hotel hotel, MakeReservationViewModel makeReservationViewModel, NavigationService navigationService)
        {
            _hotel = hotel;
            _makeReservationViewModel = makeReservationViewModel;
            _navigationService = navigationService;
            _makeReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override bool CanExecute(object parameter)
        {
            if(_makeReservationViewModel.FirstName == null || _makeReservationViewModel.LastName == null)
            {
                return false;
            }
            return _makeReservationViewModel.RoomNum > 0 && !(string.IsNullOrEmpty(_makeReservationViewModel.FirstName)) && !(string.IsNullOrEmpty(_makeReservationViewModel.LastName)) && _makeReservationViewModel.FirstName.Length <= 50 && _makeReservationViewModel.LastName.Length <= 50 && _makeReservationViewModel.NumberOfGuests > 0 && _makeReservationViewModel.StartDate < _makeReservationViewModel.EndDate;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Reservation reservation = new Reservation(_makeReservationViewModel.RoomNum, _makeReservationViewModel.FirstName, _makeReservationViewModel.LastName, _makeReservationViewModel.StartDate, _makeReservationViewModel.EndDate, _makeReservationViewModel.NumberOfGuests);

            try
            {
                await _hotel.AddReservation(reservation);
                MessageBox.Show("Soba je uspješno rezervisana", "Uspjeh", MessageBoxButton.OK, MessageBoxImage.Information);
                _navigationService.Navigate();
            } catch (ReservationConflictsException ex)
            {
                MessageBox.Show("Soba je zauzeta tokom ovog datuma", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            } catch(NonExistentRoomException ex)
            {
                MessageBox.Show("Soba ne postoji", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            } catch(InsufficientRoomCapacityException ex)
            {
                MessageBox.Show("Kapacitet sobe ne podržava ovaj broj gostiju", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(_makeReservationViewModel.RoomNum) || 
                e.PropertyName == nameof(_makeReservationViewModel.FirstName) ||
                e.PropertyName == nameof(_makeReservationViewModel.LastName) ||
                e.PropertyName == nameof(_makeReservationViewModel.StartDate) ||
                e.PropertyName == nameof(_makeReservationViewModel.EndDate) ||
                e.PropertyName == nameof(_makeReservationViewModel.NumberOfGuests))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
