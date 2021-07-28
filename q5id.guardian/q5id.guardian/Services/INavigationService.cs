using q5id.guardian.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace q5id.guardian.Services
{
    public interface INavigationService
    {
        void Register<T>();

        void SetRoot(Type viewModelType, object parameter = null, NavigationType navigationType = NavigationType.ContentPage);

        Task NavigateToAsync(Type viewModelType, object parameter = null);

        Task PopAsync();

        Task MasterDetailNavigateToAsync(Type viewModelType, object parameter = null);

        Task RemoveLastFromBackStackAsync();

        Task RemoveBackStackAsync();
    }
}
