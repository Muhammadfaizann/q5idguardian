
using q5id.guardian.Services;
using Splat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class BaseViewModel : BindableObject
    {
        private INavigationService _navigationService;
        protected INavigationService NavigationService => _navigationService = _navigationService ?? Locator.Current.GetService<INavigationService>();

        bool isBusy = false;
        public bool IsBusy
        {
            get 
            {
                return isBusy; 
            }
            set
            {
                isBusy = value;
                OnPropertyChanged(nameof(IsBusy));
            }
        }

        string title = string.Empty;
        public string Title
        {
            get 
            {
                return title; 
            }
            set 
            {
                title = value;
                OnPropertyChanged(nameof(Title));
            }
        }

        public virtual void Initialize(object parameter)
        {

        }

        //protected bool SetProperty<T>(ref T backingStore, T value,
        //    [CallerMemberName] string propertyName = "",
        //    Action onChanged = null)
        //{
        //    if (EqualityComparer<T>.Default.Equals(backingStore, value))
        //        return false;

        //    backingStore = value;
        //    onChanged?.Invoke();
        //    OnPropertyChanged(propertyName);
        //    return true;
        //}

        //#region INotifyPropertyChanged
        //public event PropertyChangedEventHandler PropertyChanged;
        //protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
        //{
        //    var changed = PropertyChanged;
        //    if (changed == null)
        //        return;

        //    changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
        //}
        //#endregion
    }
}
