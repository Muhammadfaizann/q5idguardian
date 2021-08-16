using System;
using System.Collections.Generic;
using q5id.guardian.Views.Base;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public partial class AddLovedIntroView : BaseChildContentView
    {
        public AddLovedIntroView(BaseContainerView mainCtv) : base(mainCtv)
        {
            InitializeComponent();
        }

        void OnStartClicked(System.Object sender, System.EventArgs e)
        {
            MainContentView.PushView(new AddLovedProfileInfoView(MainContentView));
        }
    }
}
