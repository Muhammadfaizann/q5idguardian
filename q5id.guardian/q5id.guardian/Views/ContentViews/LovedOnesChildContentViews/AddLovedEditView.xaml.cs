using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public partial class AddLovedEditView : BaseLovedContentChildView
    {
        public AddLovedEditView(LovedOnesContentView mainCtv) : base(mainCtv)
        {
            InitializeComponent();
            ViewTitle = "Add Loved One";
            UpdateImage();
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
                if (indexImage < imageCount && indexImage < imageSecs.Count)
                {
                    int currentIndex = indexImage;
                    System.IO.MemoryStream streamSec = new System.IO.MemoryStream(imageSecSourceStreams[currentIndex]);
                    imageSecs[indexImage].Source = ImageSource.FromStream(() => streamSec);
                    indexImage++;
                }
            }
        }
    }
}
