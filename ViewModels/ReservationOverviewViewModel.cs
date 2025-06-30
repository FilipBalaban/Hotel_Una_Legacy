using Hotel_Una_Legacy.Commands;
using Hotel_Una_Legacy.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Hotel_Una_Legacy.ViewModels
{
    public class ReservationOverviewViewModel: BaseViewModel
    {
        private ObservableCollection<ReservationViewModel> _reservations;
        private readonly Hotel _hotel;
        private string _selectedSortingProperty;
        private string _selectedSortingOrder;
        
        public IEnumerable<ReservationViewModel> Reservations => _reservations;

        public ObservableCollection<SortingItem> SortingPropertyItems { get; set; } = new ObservableCollection<SortingItem>()
        {
            new SortingItem("ID", "ID"), new SortingItem("Datum rezervacije", "ReservationDate"), new SortingItem("Check in", "StartDate"), new SortingItem("Check out", "EndDate"),
        };
        public ObservableCollection<SortingItem> SortingOrderItems { get; set; } = new ObservableCollection<SortingItem>()
        {
            new SortingItem("Opadajuće", "Descending"), new SortingItem( "Rastuće", "Ascending"),
        };
        public string SelectedSortingProperty
        {
            get => _selectedSortingProperty;
            set
            {
                _selectedSortingProperty = value;
                if(!string.IsNullOrEmpty(_selectedSortingProperty) && !string.IsNullOrEmpty(_selectedSortingOrder))
                {
                    SortReservations(_selectedSortingProperty, _selectedSortingOrder);
                }
            }
        }
        public string SelectedSortingOrder
        {
            get => _selectedSortingOrder;
            set
            {
                _selectedSortingOrder = value;
                if (!string.IsNullOrEmpty(_selectedSortingOrder) && !string.IsNullOrEmpty(_selectedSortingProperty))
                {
                    SortReservations(_selectedSortingProperty, _selectedSortingOrder);
                }
            }
        }
        public ICommand LoadReservationsCommand { get; }
        public ReservationOverviewViewModel(Hotel hotel)
        {
            _reservations = new ObservableCollection<ReservationViewModel>();
            _hotel = hotel;
            LoadReservationsCommand = new LoadReservationsCommand(this, _hotel);
        }
        public static ReservationOverviewViewModel LoadViewModel(Hotel hotel)
        {
            ReservationOverviewViewModel viewModel = new ReservationOverviewViewModel(hotel);
            viewModel.LoadReservationsCommand.Execute(null);
            return viewModel;
        }
        public void LoadReservations(IEnumerable<Reservation> reservations)
        {
            _reservations.Clear();
            foreach (Reservation reservation in reservations)
            {
                _reservations.Add(new ReservationViewModel(reservation));
            }
        }
        private void SortReservations(string sortPropertyName, string sortOrder)
        {
            List<ReservationViewModel> reservationsList = _reservations.ToList();
            _reservations.Clear();

            switch (sortPropertyName)
            {
                case "ID":
                    if(sortOrder == "Ascending")
                    {
                        reservationsList.Sort((r1, r2) => r1.ID.CompareTo(r2.ID));
                    }
                    else
                    {
                        reservationsList.Sort((r1, r2) => r2.ID.CompareTo(r1.ID));
                    }
                    break;
                case "ReservationDate":
                    if (sortOrder == "Ascending")
                    {
                        reservationsList.Sort((r1, r2) => r1.ReservationDate.CompareTo(r2.ReservationDate));
                    }
                    else
                    {
                        reservationsList.Sort((r1, r2) => r2.ReservationDate.CompareTo(r1.ReservationDate));
                    }
                    break;
                case "StartDate":
                    if (sortOrder == "Ascending")
                    {
                        reservationsList.Sort((r1, r2) => r1.StartDate.CompareTo(r2.StartDate));
                    }
                    else
                    {
                        reservationsList.Sort((r1, r2) => r2.StartDate.CompareTo(r1.StartDate));
                    }
                    break;
                case "EndDate":
                    if (sortOrder == "Ascending")
                    {
                        reservationsList.Sort((r1, r2) => r1.EndDate.CompareTo(r2.EndDate));
                    }
                    else
                    {
                        reservationsList.Sort((r1, r2) => r2.EndDate.CompareTo(r1.EndDate));
                    }
                    break;
            }
            foreach(ReservationViewModel r in reservationsList)
            {
                _reservations.Add(r);
            }
        }
    }
}
