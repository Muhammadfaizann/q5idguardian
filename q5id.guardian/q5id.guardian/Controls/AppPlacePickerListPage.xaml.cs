using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
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
                await Navigation.PopPopupAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Get Place Detail Error: " + ex.Message);
            }
            
        }

        private void EntrySearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            DebouncedSearch();
        }

        private CancellationTokenSource _throttleCts = new CancellationTokenSource();

        private async void DebouncedSearch()
        {
            try
            {
                Interlocked.Exchange(ref _throttleCts, new CancellationTokenSource()).Cancel();

                await Task.Delay(TimeSpan.FromMilliseconds(500), _throttleCts.Token)

                    .ContinueWith(async task => await Search(),
                        CancellationToken.None,
                        TaskContinuationOptions.OnlyOnRanToCompletion,
                        TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch (Exception ex)
            {
                //Ignore any Threading errors
            }
        }

        private async Task Search()
        {
            int minimumLengthForSearching = 2;
            if (EntrySearch.Text != "" && EntrySearch.Text != null && EntrySearch.Text.Length > minimumLengthForSearching)
            {
                Debug.WriteLine("Seaching with " + EntrySearch.Text);
                var result = await AppApiManager.Instances.GetPlaces(EntrySearch.Text);
                mItemSource = result.AutoCompletePlaces;
                ListPlaces.ItemsSource = mItemSource;
            }
        }

        async void OnCloseClicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}
