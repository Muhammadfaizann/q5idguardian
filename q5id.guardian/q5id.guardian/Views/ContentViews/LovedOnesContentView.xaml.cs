using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using q5id.guardian.Models;
using q5id.guardian.Utils;
using q5id.guardian.Views.Base;
using q5id.guardian.Views.ContentViews.LovedOnesChildContentViews;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews
{
    public partial class LovedOnesContentView : BaseContainerView
    {
        public static readonly BindableProperty IsUpdateSuccessProperty =
            BindableProperty.Create(nameof(IsUpdateSuccess), typeof(Boolean), typeof(LovedOnesContentView), false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnIsUpdateSuccessChanged);

        private static void OnIsUpdateSuccessChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is LovedOnesContentView view && newValue is Boolean boolVal)
            {
                if(boolVal == true)
                {
                    view.OnUpdateSuccess();
                    view.IsUpdateSuccess = false;
                }
            }
        }

        public Boolean IsUpdateSuccess
        {
            get
            {
                return (Boolean)GetValue(IsUpdateSuccessProperty);
            }
            set
            {
                SetValue(IsUpdateSuccessProperty, value);
            }
        }

        public static readonly BindableProperty PrimaryImageDataProperty =
            BindableProperty.Create(nameof(PrimaryImageData), typeof(ImageData), typeof(LovedOnesContentView), null, defaultBindingMode: BindingMode.TwoWay);

        public ImageData PrimaryImageData
        {
            get
            {
                return (ImageData)GetValue(PrimaryImageDataProperty);
            }
            set
            {
                SetValue(PrimaryImageDataProperty, value);
            }
        }

        public static readonly BindableProperty SecondaryImageDatasProperty =
            BindableProperty.Create(nameof(SecondaryImageDatas), typeof(ObservableCollection<ImageData>), typeof(LovedOnesContentView), null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSecondImagesChanged);

        private static void OnSecondImagesChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is LovedOnesContentView lovedOnesContent && newValue is ObservableCollection<ImageData> newList)
            {
                lovedOnesContent.SecondaryImageDatas = newList;
            }
        }

        public ObservableCollection<ImageData> SecondaryImageDatas
        {
            get
            {
                return (ObservableCollection<ImageData>)GetValue(SecondaryImageDatasProperty);
            }
            set
            {
                SetValue(SecondaryImageDatasProperty, value);
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
            this.SetBinding(PrimaryImageDataProperty, "PrimaryImage");
            this.SetBinding(SecondaryImageDatasProperty, "SecondaryImages");
            this.SetBinding(IsUpdateSuccessProperty, "IsUpdateSuccess");
            MainPage.UpdateRightControlVisibility(false);
            MainPage.UpdateRightControlImage(FontAwesomeIcons.Times);
            ResetView();
            ResetCommand?.Execute(null);
            this.PushView(new LovedOnesListView(this));
            
        }

        public void OnUpdateSuccess()
        {
            MainPage.UpdateRightControlVisibility(false);
            MainPage.UpdateRightControlImage(FontAwesomeIcons.Times);
            ClearImages();
            ResetView();
            this.PushView(new LovedOnesListView(this));
        }

        public void ClearImages()
        {
            PrimaryImageData = null;
            SecondaryImageDatas = new ObservableCollection<ImageData>();
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
