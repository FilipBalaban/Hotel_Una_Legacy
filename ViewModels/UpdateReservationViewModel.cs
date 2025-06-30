using Hotel_Una_Legacy.Commands;
using Hotel_Una_Legacy.Models;
using Hotel_Una_Legacy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Hotel_Una_Legacy.ViewModels
{
    public class UpdateReservationViewModel: BaseViewModel
    {
        private int _reservationID;
        private UIElement _reservationInputContentControl;
        private int _roomNum;
        private string _firstName;
        private string _lastName;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _numberOfGuests;

        public int RoomNum
        {
            get => _roomNum;
            set
            {
                _roomNum = value;
                OnPropertyChanged(nameof(RoomNum));
            }
        }
        public string FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }
        public string LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }
        public DateTime StartDate
        {
            get => _startDate;
            set
            {
                _startDate = value;
                OnPropertyChanged(nameof(StartDate));
            }
        }
        public DateTime EndDate
        {
            get => _endDate;
            set
            {
                _endDate = value;
                OnPropertyChanged(nameof(EndDate));
            }
        }
        public int NumberOfGuests
        {
            get => _numberOfGuests;
            set
            {
                _numberOfGuests = value;
                OnPropertyChanged(nameof(NumberOfGuests));
            }
        }

        public int ReservationID
        {
            get => _reservationID;
            set
            {
                _reservationID = value;
                OnPropertyChanged(nameof(ReservationID));
            }
        }
        public UIElement ReservationInputContentControl
        {
            get => _reservationInputContentControl;
            set
            {
                _reservationInputContentControl = value;
                OnPropertyChanged(nameof(ReservationInputContentControl));
            }
        }
        public ICommand SearchCommand { get; }
        public ICommand UpdateReservationCommand { get; }
        public ICommand CancelCommand { get; }
        public UpdateReservationViewModel(Hotel hotel, NavigationService navigationService)
        {
            UpdateReservationCommand = new UpdateReservationCommand(hotel, this, navigationService);
            SearchCommand = new SearchCommand(hotel, this);
            CancelCommand = new NavigateCommand(navigationService);
        }
    }
}
