using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Threading.Tasks;
using FFImageLoading.Forms;
using q5id.guardian.Models;
using q5id.guardian.Views.Base;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public partial class AddLovedImageView : BaseChildContentView
    {
        public AddLovedImageView(BaseContainerView mainCtv) : base(mainCtv)
        {
            InitializeComponent();
            ViewTitle = "Add Loved One";
            UpdateView(false);
            if(mainCtv is LovedOnesContentView lovedOnesContentView)
            {
                lovedOnesContentView.ClearImages();
            }
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

        public void OnNextClicked(System.Object sender, System.EventArgs e)
        {
            if (MainContentView is LovedOnesContentView lovedOnesContentView)
            {
                lovedOnesContentView.ShowDetailInfoView();
            }
        }

        public async void OnAddPhotoClicked(System.Object sender, System.EventArgs e)
        {
            if (MainContentView is LovedOnesContentView lovedOnesContentView)
            {
                ImageData imageData = await PickPhotoAsync();
               
                if (imageData != null)
                {
                    var listSecondImageDatas = lovedOnesContentView.SecondaryImageDatas;
                    if (sender == FrmPrimaryPhotoBtn)
                    {
                        lovedOnesContentView.PrimaryImageData = imageData;
                        ImgPrimary.Source = ImageSource.FromStream(() =>
                        {
                            return new MemoryStream(imageData.ImageByteArray);
                        });
                        FrmSnd1.IsVisible = true;
                        UpdateView(true);
                    }
                    else if (sender == FrmSnd1)
                    {
                        listSecondImageDatas[0] = imageData;
                        ImgSnd1.Source = ImageSource.FromStream(() => new MemoryStream(imageData.ImageByteArray));
                        FrmSnd1.IsVisible = false;
                        FrmSnd2.IsVisible = true;

                    }
                    else if (sender == FrmSnd2)
                    {
                        listSecondImageDatas[1] = imageData;
                        ImgSnd2.Source = ImageSource.FromStream(() => new MemoryStream(imageData.ImageByteArray));
                        FrmSnd2.IsVisible = false;
                        FrmSnd3.IsVisible = true;

                    }
                    else if (sender == FrmSnd3)
                    {
                        listSecondImageDatas[2] = imageData;
                        ImgSnd3.Source = ImageSource.FromStream(() => new MemoryStream(imageData.ImageByteArray));
                        FrmSnd3.IsVisible = false;
                        FrmSnd4.IsVisible = true;

                    }
                    else if (sender == FrmSnd4)
                    {
                        listSecondImageDatas[3] = imageData;
                        ImgSnd4.Source = ImageSource.FromStream(() => new MemoryStream(imageData.ImageByteArray));
                        FrmSnd4.IsVisible = false;
                    }
                    lovedOnesContentView.SecondaryImageDatas = CloneImages(listSecondImageDatas);
                }
            }
        }

        private ObservableCollection<ImageData> CloneImages(ObservableCollection<ImageData> listImage)
        {
            var result = new ObservableCollection<ImageData>();
            foreach (ImageData imageData in listImage)
            {
                result.Add(imageData);
            }
            return result;
        }
    }
}
