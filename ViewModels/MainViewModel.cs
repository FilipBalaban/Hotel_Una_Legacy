using Hotel_Una_Legacy.Models;
using Hotel_Una_Legacy.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.ViewModels
{
    public class MainViewModel: BaseViewModel
    {
        private readonly NavigationStore _navigationStore;
        public BaseViewModel CurrentViewModel => _navigationStore.CurrentViewModel;
        public NavigationSideBarViewModel NavigationSideBarViewModel { get; set; }
        public MainViewModel(Hotel hotel, NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            NavigationSideBarViewModel = new NavigationSideBarViewModel(_navigationStore, hotel);
        }
        protected virtual void OnCurrentViewModelChanged()
        {
            OnPropertyChanged(nameof(CurrentViewModel));
        }
    }
}
