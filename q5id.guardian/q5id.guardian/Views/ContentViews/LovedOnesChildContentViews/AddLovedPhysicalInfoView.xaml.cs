using System;
using System.Collections.Generic;
using q5id.guardian.Views.Base;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public partial class AddLovedPhysicalInfoView : BaseChildContentView
    {
        public AddLovedPhysicalInfoView(BaseContainerView mainCtv) : base(mainCtv)
        {
            InitializeComponent();
            ViewTitle = "Add Loved One";

        }

        void OnNextClicked(System.Object sender, System.EventArgs e)
        {
            MainContentView.PushView(new AddLovedImageView(MainContentView));
        }
    }
}
