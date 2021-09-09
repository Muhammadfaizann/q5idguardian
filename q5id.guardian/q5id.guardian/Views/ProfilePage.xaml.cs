﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using q5id.guardian.Models;
using q5id.guardian.ViewModels;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace q5id.guardian.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : BasePage<ProfileViewModel>
    {
        public ProfilePage()
        {
            InitializeComponent();
            this.SetBinding(ProfileImageDataProperty, "ProfileImage");
        }

        public static readonly BindableProperty ProfileImageDataProperty =
            BindableProperty.Create(nameof(ProfileImageData), typeof(ImageData), typeof(ProfilePage), null, defaultBindingMode: BindingMode.TwoWay);

        public ImageData ProfileImageData
        {
            get
            {
                return (ImageData)GetValue(ProfileImageDataProperty);
            }
            set
            {
                SetValue(ProfileImageDataProperty, value);
            }
        }

        async void OnPickImageTapped(System.Object sender, System.EventArgs e)
        {
            ImageData imageData = await PickPhotoAsync();
            if (imageData != null)
            {
                ProfileImageData = imageData;
                ImgProfile.Source = ImageSource.FromStream(() =>
                {
                    return new MemoryStream(imageData.ImageByteArray);
                });
                FrmImageProfile.IsVisible = false;
            }
        }

        private async Task<ImageData> PickPhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                var photoStream = await photo.OpenReadAsync();

                return new ImageData()
                {
                    ImageByteArray = Utils.Utils.ConvertStreamToByteArray(photoStream),
                    Extension = Path.GetExtension(photo.FileName)
                };
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PickPhotoAsync THREW: {ex.Message}");
                return null;
            }
        }

        void AppButton_Clicked(System.Object sender, System.EventArgs e)
        {
        }
    }
}
