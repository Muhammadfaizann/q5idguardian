using System;
using System.Collections.Generic;
using q5id.guardian.Views.Base;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public partial class AddLovedPhysicalInfoView : BaseChildContentView
    {
        public AddLovedPhysicalInfoView(BaseContainerView mainCtv) : base(mainCtv)
        {
            InitializeComponent();
            ViewTitle = "Add Loved One";
        }

        void OnNextClicked(System.Object sender, System.EventArgs e)
        {
            if (IsValidInput() == false)
            {
                return;
            }
            if (MainContentView is LovedOnesContentView lovedOnesContentView)
            {
                lovedOnesContentView.ShowImageInfoView();
            }
        }

        private bool IsValidInput()
        {
            if (HairColorPicker.SelectedItem == null)
            {
                ShowErrorMessage("Please input hair color");
                return false;
            }

            if (EyeColorPicker.SelectedItem == null)
            {
                ShowErrorMessage("Please input eye color");
                return false;
            }

            if (HeightFeetPicker.SelectedItem == null || HeightInchesPicker.SelectedItem == null)
            {
                ShowErrorMessage("Please input height");
                return false;
            }

            if (EntryWeight.Text == "" || EntryWeight.Text == null)
            {
                ShowErrorMessage("Please input weight");
                return false;
            }

            return true;
        }

        private async void ShowErrorMessage(string message)
        {
            await App.Current.MainPage.DisplayAlert("Error", message, "OK");
        }
    }
}
