using System;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace q5id.guardian.ViewModels
{
    public class LovedOnesViewModel : BaseViewModel
    {
        public LovedOnesViewModel()
        {
            Title = "Loved Ones";
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://q5id.com/product"));
        }

        public ICommand OpenWebCommand { get; }
    }
}