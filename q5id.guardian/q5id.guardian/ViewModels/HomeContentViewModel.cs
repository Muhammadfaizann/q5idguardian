using System;
using System.Collections.ObjectModel;
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

        private ObservableCollection<Intro> mPage;
        public ObservableCollection<Intro> Pages
        {
            get
            {
                return this.mPage;
            }

            set
            {
                this.mPage = value;
                OnPropertyChanged(nameof(Pages));
            }
        }

        public HomeContentViewModel()
        {
            Pages = new ObservableCollection<Intro>()
            {
                new Intro("Keeping kids and loved ones safe", "Guardian is a secure alert system where parents and caregivers get help from nearby Guardian volunteers to find lost loved ones."),
                new Intro("Protect your entire family", "With a monthly subscription, add multiple dependents to your account. If a loved one goes missing, nearby Guardian volunteers are ready to search."),
                new Intro("Get a free volunteer account", "If you don’t need to track loved ones, use Guardian for free. Should a caregiversend an alert and you’re nearby, you can step in and help."),
                new Intro("Privacy is our business", "We use our unique verified ID process to guarantee identities of caregivers and volunteers. You can trust your Guardian search team and know that your data is safe.")
            };
        }
    }
}
