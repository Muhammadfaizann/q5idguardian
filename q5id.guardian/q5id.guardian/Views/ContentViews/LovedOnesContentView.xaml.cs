using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Input;
using q5id.guardian.Utils;
using q5id.guardian.Views.Base;
using q5id.guardian.Views.ContentViews.LovedOnesChildContentViews;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews
{
    public partial class LovedOnesContentView : BaseContainerView
    {
        public byte[] PrimaryImageSourceByteArray;
        public List<byte[]> SecondaryImageSourceByteArrays = new List<byte[]>();

        public static readonly BindableProperty ResetCommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(LovedOnesContentView), null, defaultBindingMode: BindingMode.TwoWay);

        public ICommand ResetCommand
        {
            get { return (ICommand)GetValue(ResetCommandProperty); }
            set
            {
                SetValue(ResetCommandProperty, value);
            }
        }

        public LovedOnesContentView(HomePage homePage) : base(homePage)
        {
            InitializeComponent();
            this.SetBinding(ResetCommandProperty, "ResetCommand");
            MainPage.UpdateRightControlVisibility(false);
            MainPage.UpdateRightControlImage(FontAwesomeIcons.Times);
            ResetView();
            ResetCommand?.Execute(null);
            this.PushView(new LovedOnesListView(this));
        }

        

        public void ClearImages()
        {
            PrimaryImageSourceByteArray = null;
            SecondaryImageSourceByteArrays.Clear();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if(MainPage != null)
            {
                if(Parent != null)
                {
                    MainPage.UpdateRightControlVisibility(previousContentViews.Count > 0);
                    MainPage.RightControlClicked += OnHomeRightControlClick;
                }
                else
                {
                    MainPage.UpdateRightControlVisibility(false);
                    MainPage.RightControlClicked -= OnHomeRightControlClick;
                }
            }
        }

        public override void PushView(BaseChildContentView view)
        {
            base.PushView(view);
            MainPage.UpdateRightControlVisibility(this.previousContentViews.Count > 0);
        }

        public override void BackView()
        {
            base.BackView();
            MainPage.UpdateRightControlVisibility(this.previousContentViews.Count > 0);
        }

        public void OnHomeRightControlClick(object sender, EventArgs e)
        {
            if(previousContentViews.Count > 0)
            {
                ResetView();
                ResetCommand?.Execute(null);
                this.PushView(new LovedOnesListView(this));
            }
        }

        protected override Layout<View> GetContentView()
        {
            return gridContent;
        }
    }
}
