using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using q5id.guardian.Models;
using q5id.guardian.Services;
using Rg.Plugins.Popup.Extensions;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace q5id.guardian.Controls
{
    public partial class AppPlacePickerListPage : PopupPage
    {
        private bool mIsCalling = false;
        private bool mIsNeedContinueCall = false;
        private bool mIsTextChanging = false;
        private string mTextSearch = "";
        private List<GooglePlaceAutoCompletePrediction> mItemSource;

        private AppPopupPlacePickerView mPickerView;

        public AppPlacePickerListPage(AppPopupPlacePickerView mPickerView)
        {
            InitializeComponent();
            this.mPickerView = mPickerView;
        }

        public void ResetView()
        {
            EntrySearch.Text = "";
            mItemSource = new List<GooglePlaceAutoCompletePrediction>();
            ListPlaces.ItemsSource = mItemSource;
        }

        private async void ListPlaces_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                GooglePlaceAutoCompletePrediction itemData = mItemSource[e.ItemIndex];
                var place = await AppApiManager.Instances.GetPlaceDetails(itemData.PlaceId);
                mPickerView.SelectedItem = place;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Get Place Detail Error: " + ex.Message);
            }
            await Navigation.PopPopupAsync();
        }

        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DelayGetPlaces();
        }

        private async void DelayGetPlaces()
        {
            if (mIsTextChanging == false)
            {
                mIsTextChanging = true;
                int delayTimeToGetData = 2000;
                await Task.Delay(delayTimeToGetData);
                mIsTextChanging = false;
                int minimumLengthForSearching = 2;
                if(EntrySearch.Text != "" && EntrySearch.Text != null && EntrySearch.Text.Length > minimumLengthForSearching)
                {
                    GetPlaces();
                }
            }
        }

        private async void GetPlaces()
        {
            try
            {
                if (mIsCalling == false)
                {
                    mIsNeedContinueCall = false;
                    mIsCalling = true;
                    mTextSearch = EntrySearch.Text;
                    var result = await AppApiManager.Instances.GetPlaces(mTextSearch);
                    mIsCalling = false;
                    mItemSource = result.AutoCompletePlaces;
                    ListPlaces.ItemsSource = mItemSource;
                    if (mIsNeedContinueCall == true && EntrySearch.Text != mTextSearch)
                    {
                        GetPlaces();
                    }
                }
                else
                {
                    mIsNeedContinueCall = true;
                }
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Get Places Error: " + ex.Message);
                mItemSource = new List<GooglePlaceAutoCompletePrediction>();
                ListPlaces.ItemsSource = mItemSource;
            }
        }

        async void OnCloseClicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}
