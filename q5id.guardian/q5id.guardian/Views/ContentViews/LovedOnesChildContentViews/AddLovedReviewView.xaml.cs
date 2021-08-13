using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public partial class AddLovedReviewView : BaseLovedContentChildView
    {
        public AddLovedReviewView(LovedOnesContentView mainCtv) : base(mainCtv)
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
            System.IO.MemoryStream streamPrimary = new System.IO.MemoryStream(MainContentView.PrimaryImageSourceByteArray);
            ImgPrimary.Source = ImageSource.FromStream(() => streamPrimary);
            List<byte[]> imageSecSourceStreams = MainContentView.SecondaryImageSourceByteArrays;

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
                    System.IO.MemoryStream streamSec = new System.IO.MemoryStream(imageSecSourceStreams[indexImage]);
                    imageSecs[indexImage].Source = ImageSource.FromStream(() => streamSec);
                    indexImage++;
                }
            }
        }

        void OnEditClicked(System.Object sender, System.EventArgs e)
        {
            MainContentView.PushView(new AddLovedEditView(MainContentView));
        }
    }
}
