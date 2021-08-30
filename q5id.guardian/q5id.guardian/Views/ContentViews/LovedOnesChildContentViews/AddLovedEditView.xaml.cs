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
    public partial class AddLovedEditView : BaseChildContentView
    {
        public AddLovedEditView(BaseContainerView mainCtv) : base(mainCtv)
        {
            InitializeComponent();
            ViewTitle = "Add Loved One";
            
            InitImageTapped();
        }

        private void InitImageTapped()
        {
            var ImgTapGes = new TapGestureRecognizer();
            ImgTapGes.Tapped += ImgTapGes_Tapped;
            ImgPrimary.GestureRecognizers.Add(ImgTapGes);
            ImgSec1.GestureRecognizers.Add(ImgTapGes);
            ImgSec2.GestureRecognizers.Add(ImgTapGes);
            ImgSec3.GestureRecognizers.Add(ImgTapGes);
            ImgSec4.GestureRecognizers.Add(ImgTapGes);
        }

        private void ImgTapGes_Tapped(object sender, EventArgs e)
        {
            if(sender == ImgSec4)
            {
                if (ImgSec3.Source.IsEmpty)
                {
                    return;
                }
            }
            if (sender == ImgSec3)
            {
                if (ImgSec2.Source.IsEmpty)
                {
                    return;
                }
            }
            if (sender == ImgSec2)
            {
                if (ImgSec1.Source.IsEmpty)
                {
                    return;
                }
            }
            if (sender == ImgSec1)
            {
                if (ImgPrimary.Source.IsEmpty)
                {
                    return;
                }
            }
            HandlePickPhoto(sender, e);
        }


        public void UpdateImage(bool isUpdate)
        {
            BtnDelete.IsVisible = isUpdate;
            if (isUpdate == false)
            {
                if (MainContentView is LovedOnesContentView lovedOnesContentView)
                {
                    ImgPrimary.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(lovedOnesContentView.PrimaryImageData.ImageByteArray));
                    ObservableCollection<ImageData> imageSecSourceStreams = lovedOnesContentView.SecondaryImageDatas;

                    if (imageSecSourceStreams != null && imageSecSourceStreams.Count > 0)
                    {
                        List<CachedImage> imageSecs = new List<CachedImage>()
                        {
                            ImgSec1, ImgSec2, ImgSec3, ImgSec4
                        };
                        int imageCount = imageSecSourceStreams.Count;
                        int indexImage = 0;
                        while (indexImage < imageSecs.Count && imageSecSourceStreams[indexImage] != null)
                        {
                            var imageByteArray = lovedOnesContentView.SecondaryImageDatas[indexImage].ImageByteArray;
                            imageSecs[indexImage].Source = ImageSource.FromStream(() => new System.IO.MemoryStream(imageByteArray));
                            indexImage++;
                        }
                    }
                }
                ImgPrimary.RemoveBinding(CachedImage.SourceProperty);
                ImgSec1.RemoveBinding(CachedImage.SourceProperty);
                ImgSec2.RemoveBinding(CachedImage.SourceProperty);
                ImgSec3.RemoveBinding(CachedImage.SourceProperty);
                ImgSec4.RemoveBinding(CachedImage.SourceProperty);
            }
            else
            {
                ImgPrimary.RemoveBinding(CachedImage.SourceProperty);
                ImgSec1.RemoveBinding(CachedImage.SourceProperty);
                ImgSec2.RemoveBinding(CachedImage.SourceProperty);
                ImgSec3.RemoveBinding(CachedImage.SourceProperty);
                ImgSec4.RemoveBinding(CachedImage.SourceProperty);

                ImgPrimary.Source = null;
                ImgSec1.Source = null;
                ImgSec2.Source = null;
                ImgSec3.Source = null;
                ImgSec4.Source = null;

                ImgPrimary.SetBinding(CachedImage.SourceProperty, "Image");
                ImgSec1.SetBinding(CachedImage.SourceProperty, "Image2");
                ImgSec2.SetBinding(CachedImage.SourceProperty, "Image3");
                ImgSec3.SetBinding(CachedImage.SourceProperty, "Image4");
                ImgSec4.SetBinding(CachedImage.SourceProperty, "Image5");
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

        public async void HandlePickPhoto(System.Object sender, System.EventArgs e)
        {
            if (MainContentView is LovedOnesContentView lovedOnesContentView)
            {
                ImageData imageData = await PickPhotoAsync();

                if (imageData != null)
                {
                    var listSecondImageDatas = lovedOnesContentView.SecondaryImageDatas;
                    if (sender == ImgPrimary)
                    {
                        lovedOnesContentView.PrimaryImageData = imageData;
                        ImgPrimary.Source = ImageSource.FromStream(() =>
                        {
                            return new MemoryStream(imageData.ImageByteArray);
                        });
                    }
                    else if (sender == ImgSec1)
                    {
                        listSecondImageDatas[0] = imageData;
                        ImgSec1.Source = ImageSource.FromStream(() => new MemoryStream(imageData.ImageByteArray));

                    }
                    else if (sender == ImgSec2)
                    {
                        listSecondImageDatas[1] = imageData;
                        ImgSec2.Source = ImageSource.FromStream(() => new MemoryStream(imageData.ImageByteArray));

                    }
                    else if (sender == ImgSec3)
                    {
                        listSecondImageDatas[2] = imageData;
                        ImgSec3.Source = ImageSource.FromStream(() => new MemoryStream(imageData.ImageByteArray));

                    }
                    else if (sender == ImgSec4)
                    {
                        listSecondImageDatas[3] = imageData;
                        ImgSec4.Source = ImageSource.FromStream(() => new MemoryStream(imageData.ImageByteArray));
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
