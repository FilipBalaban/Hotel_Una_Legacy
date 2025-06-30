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
    public class NavigationSideBarViewModel: BaseViewModel
    {
        private readonly Hotel _hotel;
        private readonly NavigationStore _navigationStore;

        private bool _reservationOverviewBtnChecked;
        private bool _makeReservationBtnChecked;
        private bool _removeReservationBtnChecked;
        private bool _updateReservationBtnChecked;
        private bool _calendarBtnChecked;

        public bool ReservationOverviewBtnChecked
        {
            get => _reservationOverviewBtnChecked;
            set
            {
                _reservationOverviewBtnChecked = value;
                OnPropertyChanged(nameof(ReservationOverviewBtnChecked));
            }
        }
        public bool MakeReservationBtnChecked
        {
            get => _makeReservationBtnChecked;
            set
            {
                _makeReservationBtnChecked = value;
                OnPropertyChanged(nameof(MakeReservationBtnChecked));
            }
        }
        public bool RemoveReservationBtnChecked
        {
            get => _removeReservationBtnChecked;
            set
            {
                _removeReservationBtnChecked = value;
                OnPropertyChanged(nameof(RemoveReservationBtnChecked));
            }
        }
        public bool UpdateReservationBtnChecked
        {
            get => _updateReservationBtnChecked;
            set
            {
                _updateReservationBtnChecked = value;
                OnPropertyChanged(nameof(UpdateReservationBtnChecked));
            }
        }
        public bool CalendarBtnChecked
        {
            get => _calendarBtnChecked;
            set
            {
                _calendarBtnChecked = value;
                OnPropertyChanged(nameof(CalendarBtnChecked));
            }
        }

        public ICommand ReservationOverviewCommand { get; }
        public ICommand MakeReservationCommand { get; }
        public ICommand RemoveReservationCommand { get; }
        public ICommand UpdateReservationCommand { get; }
        public ICommand CalendarCommand { get; }

        public NavigationSideBarViewModel(NavigationStore navigationStore, Hotel hotel)
        {
            _hotel = hotel;
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += UpdateCheckedBtn;

            ReservationOverviewCommand = new NavigateCommand(new NavigationService(_navigationStore, CreateReservationOverviewVM));
            MakeReservationCommand = new NavigateCommand(new NavigationService(_navigationStore, CreateMakeReservationVM));
            RemoveReservationCommand = new NavigateCommand(new NavigationService(_navigationStore, CreateRemoveReservationVM));
            UpdateReservationCommand = new NavigateCommand(new NavigationService(_navigationStore, CreateUpdateReservationVM));
            CalendarCommand = new NavigateCommand(new NavigationService(_navigationStore, CreateCalendarVM));

            ReservationOverviewBtnChecked = true;
        }
        private ReservationOverviewViewModel CreateReservationOverviewVM()
        {
            return ReservationOverviewViewModel.LoadViewModel(_hotel);
        }
        private MakeReservationViewModel CreateMakeReservationVM()
        {
            return new MakeReservationViewModel(_hotel, new NavigationService(_navigationStore, CreateReservationOverviewVM));
        }
        private RemoveReservationViewModel CreateRemoveReservationVM()
        {
            return new RemoveReservationViewModel(_hotel, new NavigationService(_navigationStore, CreateReservationOverviewVM));
        }
        private UpdateReservationViewModel CreateUpdateReservationVM()
        {
            return new UpdateReservationViewModel(_hotel, new NavigationService(_navigationStore, CreateReservationOverviewVM));
        }
        private CalendarViewModel CreateCalendarVM()
        {
            return CalendarViewModel.LoadViewModel(_hotel);
        }
        public void UpdateCheckedBtn()
        {
            if (_navigationStore.CurrentViewModel.GetType() == typeof(ReservationOverviewViewModel))
            {
                ReservationOverviewBtnChecked = true;
            }
            else if (_navigationStore.CurrentViewModel.GetType() == typeof(MakeReservationViewModel))
            {
                MakeReservationBtnChecked = true;
            }
            else if (_navigationStore.CurrentViewModel.GetType() == typeof(ReservationOverviewViewModel))
            {
                ReservationOverviewBtnChecked = true;
            }
            else if (_navigationStore.CurrentViewModel.GetType() == typeof(MakeReservationViewModel))
            {
                UpdateReservationBtnChecked = true;
            }
            else if (_navigationStore.CurrentViewModel.GetType() == typeof(CalendarViewModel))
            {
                CalendarBtnChecked = true;
            }
        }
    }
}
