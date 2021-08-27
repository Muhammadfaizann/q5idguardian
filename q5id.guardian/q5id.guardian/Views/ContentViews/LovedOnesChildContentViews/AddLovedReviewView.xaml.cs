using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using q5id.guardian.Models;
using q5id.guardian.Views.Base;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public partial class AddLovedReviewView : BaseChildContentView
    {
        public AddLovedReviewView(BaseContainerView mainCtv) : base(mainCtv)
        {
            InitializeComponent();
            ViewTitle = "Add Loved One";
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if (Parent != null)
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
            ImgPrimary.Source = null;
            ImgSec1.Source = null;
            ImgSec2.Source = null;
            ImgSec3.Source = null;
            ImgSec4.Source = null;
        }


        private void UpdateImage()
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
                    while (indexImage < imageCount && indexImage < imageSecs.Count)
                    {
                        var imageByteArray = lovedOnesContentView.SecondaryImageDatas[indexImage].ImageByteArray;
                        imageSecs[indexImage].Source = ImageSource.FromStream(() => new System.IO.MemoryStream(imageByteArray));
                        indexImage++;
                    }
                }
            }
        }

        void OnEditClicked(System.Object sender, System.EventArgs e)
        {
            MainContentView.PushView(new AddLovedEditView(MainContentView));
        }
    }
}
