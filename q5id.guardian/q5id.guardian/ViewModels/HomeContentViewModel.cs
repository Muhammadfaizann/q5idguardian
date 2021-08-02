using System;
using q5id.guardian.Models;

namespace q5id.guardian.ViewModels
{
    public class HomeContentViewModel : BaseViewModel
    {
        private User mUser = null;
        public User User
        {
            get => mUser;
            set
            {
                mUser = value;
                OnPropertyChanged(nameof(User));
            }
        }

        public HomeContentViewModel()
        {
        }
    }
}
