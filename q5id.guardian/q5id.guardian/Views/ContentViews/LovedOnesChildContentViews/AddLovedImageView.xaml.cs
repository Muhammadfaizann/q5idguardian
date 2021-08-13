using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FFImageLoading.Forms;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public partial class AddLovedImageView : BaseLovedContentChildView
    {
        public AddLovedImageView(LovedOnesContentView mainCtv) : base(mainCtv)
        {
            InitializeComponent();
            ViewTitle = "Add Loved One";
            UpdateView(false);
            mainCtv.ClearImages();
            ClearImages();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if(Parent == null)
            {
                UpdateView(false);
                ClearImages();
            }
        }

        public void UpdateView(bool isHasPrimaryImage)
        {
            if (isHasPrimaryImage)
            {
                StackNoImage.IsVisible = false;
                StackHasImage.IsVisible = true;
                FrmPrimaryPhoto.IsVisible = false;
                BtnNext.IsVisible = true;
            }
            else
            {
                StackNoImage.IsVisible = true;
                StackHasImage.IsVisible = false;
                FrmPrimaryPhoto.IsVisible = true;
                BtnNext.IsVisible = false;
            }
        }

        public void ClearImages()
        {
            ImgPrimary.Source = null;
            ImgSnd1.Source = null;
            ImgSnd2.Source = null;
            ImgSnd3.Source = null;
            ImgSnd4.Source = null;

            FrmSnd1.IsVisible = true;
            FrmSnd2.IsVisible = false;
            FrmSnd3.IsVisible = false;
            FrmSnd4.IsVisible = false;
        }

        private async Task<System.IO.Stream> PickPhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.PickPhotoAsync();
                var photoStream = await photo.OpenReadAsync();
                return photoStream;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"PickPhotoAsync THREW: {ex.Message}");
                return null;
            }
        }

        public void OnNextClicked(System.Object sender, System.EventArgs e)
        {

            MainContentView.PushView(new AddLovedDetailView(MainContentView));
        }

        public async void OnAddPhotoClicked(System.Object sender, System.EventArgs e)
        {
            System.IO.Stream sourceStream = await PickPhotoAsync();
            if(sourceStream != null)
            {
                if (sender == FrmPrimaryPhotoBtn)
                {
                    MainContentView.PrimaryImageSourceByteArray = Utils.Utils.ConvertStreamToByteArray(sourceStream);
                    ImgPrimary.Source = ImageSource.FromStream(() => sourceStream);
                    FrmSnd1.IsVisible = true;
                    
                    UpdateView(true);
                }
                else if(sender == FrmSnd1)
                {
                    MainContentView.SecondaryImageSourceByteArrays.Add(Utils.Utils.ConvertStreamToByteArray(sourceStream));
                    ImgSnd1.Source = ImageSource.FromStream(() => sourceStream);
                    FrmSnd1.IsVisible = false;
                    FrmSnd2.IsVisible = true;
                    
                }
                else if (sender == FrmSnd2)
                {
                    MainContentView.SecondaryImageSourceByteArrays.Add(Utils.Utils.ConvertStreamToByteArray(sourceStream));
                    ImgSnd2.Source = ImageSource.FromStream(() => sourceStream);
                    FrmSnd2.IsVisible = false;
                    FrmSnd3.IsVisible = true;
                    
                }
                else if (sender == FrmSnd3)
                {
                    MainContentView.SecondaryImageSourceByteArrays.Add(Utils.Utils.ConvertStreamToByteArray(sourceStream));
                    ImgSnd3.Source = ImageSource.FromStream(() => sourceStream);
                    FrmSnd3.IsVisible = false;
                    FrmSnd4.IsVisible = true;
                    
                }
                else if (sender == FrmSnd4)
                {
                    MainContentView.SecondaryImageSourceByteArrays.Add(Utils.Utils.ConvertStreamToByteArray(sourceStream));
                    ImgSnd4.Source = ImageSource.FromStream(() => sourceStream);
                    FrmSnd4.IsVisible = false;
                   
                }
            }
        }
    }
}
