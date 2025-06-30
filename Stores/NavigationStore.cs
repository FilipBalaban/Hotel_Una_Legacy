using Hotel_Una_Legacy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Una_Legacy.Stores
{
    public class NavigationStore
    {
        public event Action CurrentViewModelChanged;

        private BaseViewModel _currentViewModel;
        public BaseViewModel CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }
        protected virtual void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
        
    }
}
