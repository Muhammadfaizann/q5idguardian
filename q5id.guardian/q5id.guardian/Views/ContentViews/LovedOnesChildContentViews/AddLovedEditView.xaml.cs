using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using FFImageLoading.Forms;
using q5id.guardian.Models;
using q5id.guardian.Views.Base;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public partial class AddLovedEditView : BaseChildContentView
    {
        private bool mIsUpdate = false;

        public AddLovedEditView(BaseContainerView mainCtv, bool isUpdate = false) : base(mainCtv)
        {
            InitializeComponent();
            ViewTitle = "Add Loved One";
            mIsUpdate = isUpdate;
            if (isUpdate)
            {
                ImgPrimary.SetBinding(CachedImage.SourceProperty, "Image");
            }
            BtnDelete.IsVisible = isUpdate;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if(Parent != null)
            {
                UpdateImage();
            }
            else
            {
                ClearImage();
            }
        }

        private void ClearImage()
        {
            //ImgPrimary.Source = null;
            //ImgSec1.Source = null;
            //ImgSec2.Source = null;
            //ImgSec3.Source = null;
            //ImgSec4.Source = null;
        }

        private void UpdateImage()
        {
            if(mIsUpdate == false)
            {
                if (MainContentView is LovedOnesContentView lovedOnesContentView)
                {
                    ImgPrimary.Source = ImageSource.FromStream(() => new System.IO.MemoryStream(lovedOnesContentView.PrimaryImageData.ImageByteArray));
                    ObservableCollection<ImageData> imageSecSourceStreams = lovedOnesContentView.SecondaryImageDatas;

                    if (imageSecSourceStreams != null && imageSecSourceStreams.Count > 0)
                    {
                        List<Image> imageSecs = new List<Image>()
                        {
                            ImgSec1, ImgSec2, ImgSec3, ImgSec4
                        };
                        int imageCount = imageSecSourceStreams.Count;
                        int indexImage = 0;
                        while (indexImage < imageCount && indexImage < imageSecs.Count && imageSecSourceStreams[indexImage] != null)
                        {
                            var imageByteArray = lovedOnesContentView.SecondaryImageDatas[indexImage].ImageByteArray;
                            imageSecs[indexImage].Source = ImageSource.FromStream(() => new System.IO.MemoryStream(imageByteArray));
                            indexImage++;
                        }
                    }
                }
            }
            else
            {
                //ImgPrimary.SetBinding(Image.SourceProperty, "Image");
                //ImgSec1.SetBinding(Image.SourceProperty, "Image2");
                //ImgSec2.SetBinding(Image.SourceProperty, "Image3");
                //ImgSec3.SetBinding(Image.SourceProperty, "Image4");
                //ImgSec4.SetBinding(Image.SourceProperty, "Image5");
            }
        }
    }
}
