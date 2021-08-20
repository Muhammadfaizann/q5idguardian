using System;
using System.Diagnostics;
using System.Windows.Input;
using q5id.guardian.Models;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels.ItemViewModels
{
    public class AlertItemViewModel : BaseItemViewModel<Alert>
    {

        public Action<AlertItemViewModel> OnUpdateExpanded;
        
        private string mUpdatedTimeDescription = "";
        public string UpdatedTimeDescription
        {
            get => mUpdatedTimeDescription;
            set
            {
                mUpdatedTimeDescription = value;
                RaisePropertyChanged(nameof(UpdatedTimeDescription));
            }
        }

        private Boolean mIsExpanded = false;
        public Boolean IsExpanded
        {
            get => mIsExpanded;
            set
            {
                mIsExpanded = value;
                RaisePropertyChanged(nameof(IsExpanded));
            }
        }

        public ICommand ToggleExpandedCommand
        {
            get
            {
                return new Command(() => {
                    IsExpanded = !IsExpanded;
                    if(OnUpdateExpanded != null)
                    {
                        OnUpdateExpanded(this);
                    }
                });
            }
        }

        public string FirstImage
        {
            get => "https://images.statusfacebook.com/profile_pictures/beautiful-children-photos/beautiful-children-dp-profile-pictures-for-whatsapp-facebook-05.jpg";
        }

        public string SecondImage
        {
            get => "https://images.statusfacebook.com/profile_pictures/beautiful-children-photos/beautiful-children-dp-profile-pictures-for-whatsapp-facebook-06.jpg";
        }

        public string ThirdImage
        {
            get => "https://images.statusfacebook.com/profile_pictures/beautiful-children-photos/beautiful-children-dp-profile-pictures-for-whatsapp-facebook-02.jpg";
        }

        public Action OnUpdateItemAction { get; set; }

        public AlertItemViewModel(Alert alert) : base(alert)
        {
           
        }
    }
}
