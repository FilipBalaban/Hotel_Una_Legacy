using Hotel_Una_Legacy.Commands;
using Hotel_Una_Legacy.Models;
using Hotel_Una_Legacy.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel_Una_Legacy.ViewModels
{
    public class CalendarViewModel: BaseViewModel
    {
        private List<Reservation> _reservations;
        private readonly ObservableCollection<Week> _weeks;
        private ObservableCollection<int> _roomNumbers;
        private readonly Hotel _hotel;
        private DateTime _selectedDate;
        private int _selectedRoomNum;
        public DateTime SelectedDate
        {
            get => _selectedDate;
            set
            {
                _selectedDate = value;
                OnPropertyChanged(nameof(CurrentMonth));
                DisplayCalendar();
            }
        }
        public IEnumerable<int> RoomNumbers => _roomNumbers;
        public int SelectedRoomNum
        {
            get => _selectedRoomNum;
            set
            {
                _selectedRoomNum = value;
                OnPropertyChanged(nameof(SelectedRoomNum));
                DisplayCalendar();
            }
        }
        public string CurrentMonth => _selectedDate.ToString("MMMM", new CultureInfo("sr-Latn-RS")) + ' ' + _selectedDate.Year;
        public IEnumerable<Week> Weeks => _weeks;
        public ICommand LastMonthCommand { get; }
        public ICommand NextMonthCommand { get; }
        public ICommand LoadReservationsCommand { get; }
        public CalendarViewModel(Hotel hotel)
        {
            _hotel = hotel;
            _weeks = new ObservableCollection<Week>();
            _roomNumbers = new ObservableCollection<int>();
            _reservations = new List<Reservation>();
            SelectedDate = DateTime.Now;
            LastMonthCommand = new LastMonthCommand(this);
            NextMonthCommand = new NextMonthCommand(this);
            LoadReservationsCommand = new LoadReservationsCommand(this, hotel);

            foreach (Room room in _hotel.GetRooms())
            {
                _roomNumbers.Add(room.RoomNum);
            }
        }
        public static CalendarViewModel LoadViewModel(Hotel hotel)
        {
            CalendarViewModel viewModel = new CalendarViewModel(hotel);
            viewModel.LoadReservationsCommand.Execute(null);
            return viewModel;
        }
        private void DisplayCalendar()
        {
            _weeks.Clear();
            int daysInMonth = DateTime.DaysInMonth(_selectedDate.Year, _selectedDate.Month);
            int daysInWeekCounter = 1;
            List<Reservation> currentMonthReservations = _reservations.Where(r => r.RoomNum == SelectedRoomNum).Where(r => r.StartDate.Month == _selectedDate.Month || r.EndDate.Month == _selectedDate.Month).ToList();

            Week week = new Week();
            for(int i = 1; i <= daysInMonth; i++)
            {
                string dayInMonth = new DateTime(_selectedDate.Year, _selectedDate.Month, i).ToString("dddd");
                if(i == 1 && dayInMonth != "Sunday")
                {
                    int lastMonth = new DateTime(_selectedDate.Year, _selectedDate.Month, 1).AddMonths(-1).Month;
                    int year = _selectedDate.Year;
                    if(lastMonth == 12)
                    {
                        year--;
                    }

                    int actualDayInWeek = (int)new DateTime(_selectedDate.Year, _selectedDate.Month, i).DayOfWeek;
                    int daysInLastMonth = DateTime.DaysInMonth(year, lastMonth);
                    for (int j = daysInLastMonth - actualDayInWeek + 1; j <= daysInLastMonth; j++)
                    {
                        string dayInLastMonth = new DateTime(year, lastMonth, j).ToString("dddd");
                        week.AssignDateToDay(j, dayInLastMonth, "LightGray");
                        daysInWeekCounter++;
                    }
                }
                AssignDate(new DateTime(_selectedDate.Year, _selectedDate.Month, i), dayInMonth, currentMonthReservations, week);             
                if (daysInWeekCounter >= 7)
                {
                    _weeks.Add(week);
                    week = new Week();
                    daysInWeekCounter = 0;
                }
                daysInWeekCounter++;
                if (i == daysInMonth && dayInMonth != "Saturday")
                {
                    int nextMonth = new DateTime(_selectedDate.Year, _selectedDate.Month, 1).AddMonths(1).Month;
                    int actualDayInWeek = (int)new DateTime(_selectedDate.Year, _selectedDate.Month, i).DayOfWeek;
                    int year = _selectedDate.Year;
                    if(nextMonth == 1)
                    {
                        year++;
                    }
                    int daysInNextMonth = DateTime.DaysInMonth(year, nextMonth);
                    for (int j = 1; j < 7 - actualDayInWeek; j++)
                    {
                        string dayInNextMonth = new DateTime(year, nextMonth, j).ToString("dddd");
                        week.AssignDateToDay(j, dayInNextMonth, "LightGray");
                        daysInWeekCounter++;
                    }
                    _weeks.Add(week);
                }
            }
        }
        private void AssignDate(DateTime date, string dayInMonth, List<Reservation> currentMonthReservations, Week week)
        {
            if (currentMonthReservations.Count > 0)
            {
                foreach (Reservation reservation in currentMonthReservations)
                {
                    DateTime reservationStartDate = reservation.StartDate.Date;
                    DateTime reservationEndDate = reservation.EndDate.Date;
                    DateTime dateOnly = date.Date;
                    if (reservationStartDate <= dateOnly
                        && reservationEndDate >= dateOnly)
                    {
                        week.AssignDateToDay(date.Day, dayInMonth, "Red", "Bold");
                        return;
                    }
                }
            }
            week.AssignDateToDay(date.Day, dayInMonth);
        }
        public void LoadReservations(IEnumerable<Reservation> reservations)
        {
            _reservations = reservations.ToList();
        }
    }
}
