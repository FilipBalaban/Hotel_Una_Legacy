using Hotel_Una_Legacy.Commands;
using Hotel_Una_Legacy.Models;
using Hotel_Una_Legacy.Services;
using Hotel_Una_Legacy.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel_Una_Legacy.ViewModels
{
    public class MakeReservationViewModel: BaseViewModel
    {
        private int _roomNum;
        private string _firstName;
        private string _lastName;
        private DateTime _startDate;
        private DateTime _endDate;
        private int _numberOfGuests;
        private string _comment;

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
        public string Comment
        {
            get => _comment;
            set
            {
                _comment = value;
                OnPropertyChanged(nameof(Comment));
            }
        }
        public ICommand ReserveCommand { get; }
        public ICommand CancelCommand { get; }

        public MakeReservationViewModel(Hotel hotel, NavigationService navigationService)
        {
            StartDate = DateTime.Now;
            EndDate = DateTime.Now.AddDays(7);
            ReserveCommand = new MakeReservationCommand(hotel, this, navigationService);
            CancelCommand = new NavigateCommand(navigationService);
        }
    }
}
