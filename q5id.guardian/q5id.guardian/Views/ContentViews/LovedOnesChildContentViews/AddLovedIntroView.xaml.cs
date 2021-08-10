using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public partial class AddLovedIntroView : BaseLovedContentChildView
    {
        public AddLovedIntroView(LovedOnesContentView mainCtv) : base(mainCtv)
        {
            InitializeComponent();
        }

        void OnStartClicked(System.Object sender, System.EventArgs e)
        {
            MainContentView.PushView(new AddLovedProfileInfoView(MainContentView));
        }
    }
}
