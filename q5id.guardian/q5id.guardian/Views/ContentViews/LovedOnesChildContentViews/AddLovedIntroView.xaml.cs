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
            ViewTitle = "Loved Ones";
        }

        void OnStartClicked(System.Object sender, System.EventArgs e)
        {
            if (MainContentView is LovedOnesContentView lovedOnesContentView)
            {
                lovedOnesContentView.ShowProfileInfoView();
            }
        }
    }
}
