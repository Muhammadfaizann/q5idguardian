using System;
using System.Diagnostics;
using q5id.guardian.Models;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels.ItemViewModels
{
    public class AlertItemViewModel : BaseItemViewModel<Alert>
    {
        
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
