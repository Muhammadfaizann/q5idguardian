using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public partial class LovedOnesListView : BaseLovedContentChildView
    {
        public LovedOnesListView(LovedOnesContentView mainCtv) : base(mainCtv)
        {
            InitializeComponent();
        }

        void OnAddClicked(System.Object sender, System.EventArgs e)
        {
            MainContentView.PushView(new AddLovedIntroView(MainContentView));
        }
    }
}
