using Hotel_Una_Legacy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.Commands
{
    public class LastMonthCommand : BaseCommand
    {
        private readonly CalendarViewModel _calendarViewModel;
        public override void Execute(object parameter)
        {
            _calendarViewModel.SelectedDate = _calendarViewModel.SelectedDate.AddMonths(-1);
        }
        public LastMonthCommand(CalendarViewModel calendarViewModel)
        {
            _calendarViewModel = calendarViewModel;
        }
    }
}
