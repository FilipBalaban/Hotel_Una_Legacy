using Hotel_Una_Legacy.Models;
using Hotel_Una_Legacy.Services.ReservationManagers;
using Hotel_Una_Legacy.Stores;
using Hotel_Una_Legacy.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Hotel_Una_Legacy
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Hotel _hotel;
        private readonly NavigationStore _navigationStore;
        private readonly NavigationSideBarViewModel _navigationSideBarViewModel;
        private readonly string _connectionString = ConfigurationManager.ConnectionStrings["Hotel_Una_Legacy.Properties.Settings.HotelUnaTestDBConnectionString"].ConnectionString;
        private readonly DatabaseReservationManager _databaseReservationManager;
        public App()
        {
            _databaseReservationManager = new DatabaseReservationManager(_connectionString);
            _hotel = new Hotel("Hotel Una", _databaseReservationManager);
            _navigationStore = new NavigationStore();
            _navigationSideBarViewModel = new NavigationSideBarViewModel(_navigationStore, _hotel);
            _navigationStore.CurrentViewModel = ReservationOverviewViewModel.LoadViewModel(_hotel);
        }
        protected override void OnStartup(StartupEventArgs e)
        {
            var mainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_hotel, _navigationStore)
            };

            mainWindow.Show();

            base.OnStartup(e);
        }
    }
}
