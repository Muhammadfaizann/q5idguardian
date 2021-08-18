using System;
using System.Collections.Generic;
using q5id.guardian.Views.Base;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public partial class LovedOnesListView : BaseChildContentView
    {
        public LovedOnesListView(BaseContainerView mainCtv) : base(mainCtv)
        {
            InitializeComponent();
            ViewTitle = "Loved Ones";
        }

        void OnAddClicked(System.Object sender, System.EventArgs e)
        {
            MainContentView.PushView(new AddLovedIntroView(MainContentView));
        }
    }
}
