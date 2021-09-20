using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using q5id.guardian.Models;
using q5id.guardian.Selectors;
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
                //lovedOnesContent.SecondaryImageDatas = newList;
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

        public LovedOnesContentView(HomePage homePage) : base(homePage)
        {
            InitializeComponent();
            this.SetBinding(PrimaryImageDataProperty, "PrimaryImage");
            this.SetBinding(SecondaryImageDatasProperty, "SecondaryImages");
            this.SetBinding(IsUpdateSuccessProperty, "IsUpdateSuccess");
            this.SetBinding(ResetCommandProperty, "ResetCommand");
            MainPage.UpdateRightControlVisibility(false);
            MainPage.UpdateRightControlImage(FontAwesomeIcons.Times);
            SetupView();
            
        }

        private void SetupView()
        {
            ClearImages();
            PushView(new LovedOnesListView(this));
        }

        

        public void ShowIntroView()
        {
            PushView(new AddLovedIntroView(this));
            MainPage.UpdateRightControlVisibility(true);
            MainPage.UpdateRightControlImage(FontAwesomeIcons.Times);
        }

        public void ShowProfileInfoView()
        {
            PushView(new AddLovedProfileInfoView(this));
        }

        public void ShowPhysicalInfoView()
        {
            PushView(new AddLovedPhysicalInfoView(this));
        }

        public void ShowImageInfoView()
        {
            PushView(new AddLovedImageView(this));
        }

        public void ShowDetailInfoView()
        {
            PushView(new AddLovedDetailView(this));
        }

        public void ShowReviewView()
        {
            PushView(new AddLovedReviewView(this));
        }

        public void ShowEditView(bool isUpdate)
        {
            MainPage.UpdateRightControlVisibility(true);
            var addLovedEditView = new AddLovedEditView(this);
            PushView(addLovedEditView);
            addLovedEditView.UpdateImage(isUpdate);
        }

        public void OnUpdateSuccess()
        {
            MainPage.UpdateRightControlVisibility(false);
            MainPage.UpdateRightControlImage(FontAwesomeIcons.Times);
            ClearImages();
            ResetCommand?.Execute(null);
            BackToTop();
        }

        public void ClearImages()
        {
            PrimaryImageData = null;
            SecondaryImageDatas = new ObservableCollection<ImageData>()
            {
                null,
                null,
                null,
                null,
            };
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if(Parent != null)
            {
                MainPage.RightControlClicked += OnHomeRightControlClick;
            }
            else
            {
                MainPage.RightControlClicked -= OnHomeRightControlClick;
            }
        }

        public void OnHomeRightControlClick(object sender, EventArgs e)
        {
            BackToTop();
            ClearImages();
            ResetCommand?.Execute(null);
            MainPage.UpdateRightControlVisibility(false);
        }

        public override Grid GetContentView()
        {
            return this.GridContentView;
        }
    }
}
