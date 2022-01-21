using System;
using System.Collections.Generic;
using q5id.guardian.Views.Base;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public partial class AddLovedProfileInfoView : BaseChildContentView
    {
        public AddLovedProfileInfoView(BaseContainerView mainCtv) : base(mainCtv)
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
                lovedOnesContentView.ShowPhysicalInfoView();
            }
        }

        private bool IsValidInput()
        {
            if(EntryFirstName.Text == "" || EntryFirstName.Text == null)
            {
                ShowErrorMessage("Please input first name");
                return false;
            }
            if (EntryLastName.Text == "" || EntryLastName.Text == null)
            {
                ShowErrorMessage("Please input first name");
                return false;
            }
            if (EntryBirthDay.NullableDate == null)
            {
                ShowErrorMessage("Please input birthday");
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
