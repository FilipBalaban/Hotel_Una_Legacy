using Hotel_Una_Legacy.Commands;
using Hotel_Una_Legacy.Models;
using Hotel_Una_Legacy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Input;

namespace Hotel_Una_Legacy.ViewModels
{
    public class RemoveReservationViewModel: BaseViewModel
    {
        private int _reservationID;
        private UIElement _reservationContentControl;
        private Hotel _hotel;

        public int ReservationID
        {
            get => _reservationID;
            set
            {
                _reservationID = value;
                OnPropertyChanged(nameof(ReservationID));
            }
        }
        public UIElement ReservationContentControl
        {
            get => _reservationContentControl;
            set
            {
                _reservationContentControl = value;
                OnPropertyChanged(nameof(ReservationContentControl));
            }
        }
        public ICommand SearchCommand { get; }
        public ICommand RemoveReservationCommand { get; }
        public ICommand CancelCommand { get; }
        public RemoveReservationViewModel(Hotel hotel, NavigationService navigationService)
        {
            _hotel = hotel;
            RemoveReservationCommand = new RemoveReservationCommand(hotel, this, navigationService);
            SearchCommand = new SearchCommand(_hotel, this);
            CancelCommand = new NavigateCommand(navigationService);
        }
    }
}
