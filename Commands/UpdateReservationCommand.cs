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
    public class UpdateReservationCommand : AsyncBaseCommand
    {
        private readonly Hotel _hotel;
        private readonly UpdateReservationViewModel _updateReservationViewModel;
        private readonly NavigationService _navigationService;

        public UpdateReservationCommand(Hotel hotel, UpdateReservationViewModel removeReservationViewModel, NavigationService navigationService)
        {
            _hotel = hotel;
            _updateReservationViewModel = removeReservationViewModel;
            _updateReservationViewModel.PropertyChanged += OnViewModelPropertyChanged;
            _navigationService = navigationService;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_updateReservationViewModel.RoomNum) || 
                e.PropertyName == nameof(_updateReservationViewModel.FirstName) ||
                e.PropertyName == nameof(_updateReservationViewModel.LastName) ||
                e.PropertyName == nameof(_updateReservationViewModel.StartDate) ||
                e.PropertyName == nameof(_updateReservationViewModel.EndDate) ||
                e.PropertyName == nameof(_updateReservationViewModel.NumberOfGuests))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            if (_updateReservationViewModel.FirstName == null || _updateReservationViewModel.LastName == null)
            {
                return false;
            }
            return _updateReservationViewModel.RoomNum > 0 && !(string.IsNullOrEmpty(_updateReservationViewModel.FirstName)) && !(string.IsNullOrEmpty(_updateReservationViewModel.LastName)) && _updateReservationViewModel.NumberOfGuests > 0 && _updateReservationViewModel.StartDate < _updateReservationViewModel.EndDate;
        }
        public override async Task ExecuteAsync(object parameter)
        {
            Reservation reservation = new Reservation(_updateReservationViewModel.ReservationID, _updateReservationViewModel.RoomNum, _updateReservationViewModel.FirstName, _updateReservationViewModel.LastName, _updateReservationViewModel.StartDate, _updateReservationViewModel.EndDate, _updateReservationViewModel.NumberOfGuests, _updateReservationViewModel.Comment);
            try
            {
                await _hotel.UpdateReservation(reservation);
                MessageBox.Show("Rezervacija je uspješno ažurirana", "Uspjeh", MessageBoxButton.OK, MessageBoxImage.Information);
                _navigationService.Navigate();
            }
            catch (NonExistentReservationException ex)
            {
                MessageBox.Show("Rezervacija nije pronađena", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (ReservationConflictsException ex)
            {
                MessageBox.Show("Soba je zauzeta tokom ovog perioda", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (NonExistentRoomException ex)
            {
                MessageBox.Show("Soba ne postoji", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            catch (InsufficientRoomCapacityException ex)
            {
                MessageBox.Show("Kapacitet sobe ne podržava ovaj broj gostiju", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
