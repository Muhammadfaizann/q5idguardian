﻿using System;
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
        private GoogleMapsApiService mapsService;
        private List<GooglePlaceAutoCompletePrediction> mItemSource;

        private AppPopupPlacePickerView mPickerView;

        public AppPlacePickerListPage(AppPopupPlacePickerView mPickerView)
        {
            InitializeComponent();
            this.mPickerView = mPickerView;
            mapsService = new GoogleMapsApiService();
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
                var place = await mapsService.GetPlaceDetails(itemData.PlaceId);
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
                await Task.Delay(1000);
                mIsTextChanging = false;
                if(EntrySearch.Text != "" && EntrySearch.Text != null)
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
                    var result = await mapsService.GetPlaces(EntrySearch.Text);
                    mIsCalling = false;
                    mItemSource = result.AutoCompletePlaces;
                    ListPlaces.ItemsSource = mItemSource;
                    if (mIsNeedContinueCall == true)
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