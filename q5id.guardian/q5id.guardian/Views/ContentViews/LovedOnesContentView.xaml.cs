using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        public static readonly BindableProperty PrimaryImageSourceByteArrayProperty =
            BindableProperty.Create(nameof(PrimaryImageSourceByteArray), typeof(byte[]), typeof(LovedOnesContentView), null, defaultBindingMode: BindingMode.TwoWay);

        public byte[] PrimaryImageSourceByteArray
        {
            get
            {
                return (byte[])GetValue(PrimaryImageSourceByteArrayProperty);
            }
            set
            {
                SetValue(PrimaryImageSourceByteArrayProperty, value);
            }
        }

        public static readonly BindableProperty SecondaryImageSourceByteArraysProperty =
            BindableProperty.Create(nameof(SecondaryImageSourceByteArrays), typeof(ObservableCollection<byte[]>), typeof(LovedOnesContentView), null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSecondImagesChanged);

        private static void OnSecondImagesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is LovedOnesContentView lovedOnesContent && newValue is ObservableCollection<byte[]> newList)
            {
                lovedOnesContent.SecondaryImageSourceByteArrays = newList;
            }
        }

        public ObservableCollection<byte[]> SecondaryImageSourceByteArrays
        {
            get
            {
                return (ObservableCollection<byte[]>)GetValue(SecondaryImageSourceByteArraysProperty);
            }
            set
            {
                SetValue(SecondaryImageSourceByteArraysProperty, value);
            }
        }

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
            this.SetBinding(PrimaryImageSourceByteArrayProperty, "PrimaryImage");
            this.SetBinding(SecondaryImageSourceByteArraysProperty, "SecondaryImages");
            MainPage.UpdateRightControlVisibility(false);
            MainPage.UpdateRightControlImage(FontAwesomeIcons.Times);
            ResetView();
            ResetCommand?.Execute(null);
            this.PushView(new LovedOnesListView(this));
            
        }

        

        public void ClearImages()
        {
            PrimaryImageSourceByteArray = null;
            SecondaryImageSourceByteArrays = new ObservableCollection<byte[]>();
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

        public override void PushView(BaseChildContentView view, bool isSaved = true)
        {
            base.PushView(view, isSaved);
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
