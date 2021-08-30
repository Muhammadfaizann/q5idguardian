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
        public const int VIEW_LIST_INDEX = 0;
        public const int VIEW_INTRO_INDEX = 1;
        public const int VIEW_PROFILE_INFO_INDEX = 2;
        public const int VIEW_PHYSIC_INFO_INDEX = 3;
        public const int VIEW_IMAGE_INFO_INDEX = 4;
        public const int VIEW_DETAIL_INFO_INDEX = 5;
        public const int VIEW_REVIEW_INDEX = 6;
        public const int VIEW_EDIT_INDEX = 7;

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

        public AddLovedEditView AddLovedEditView { get; private set; }
        public Boolean IsUpdateFlow { get; private set; } = false;

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
            CarouselViewContent.ItemTemplate = new LovedOnesPageSelector()
            {
                ListTemplate = new DataTemplate(() =>
                {
                    return GetContentView(new LovedOnesListView(this));
                }),
                IntroTemplate = new DataTemplate(() =>
                {
                    return GetContentView(new AddLovedIntroView(this));
                }),
                ProfileInfoTemplate = new DataTemplate(() =>
                {
                    return GetContentView(new AddLovedProfileInfoView(this));
                }),
                PhysicalInfoTemplate = new DataTemplate(() =>
                {
                    return GetContentView(new AddLovedPhysicalInfoView(this));
                }),
                ImageInfoTemplate = new DataTemplate(() =>
                {
                    return GetContentView(new AddLovedImageView(this));
                }),
                DetailInfoTemplate = new DataTemplate(() =>
                {
                    return GetContentView(new AddLovedDetailView(this));
                }),
                ReviewTemplate = new DataTemplate(() =>
                {
                    return GetContentView(new AddLovedReviewView(this));
                }),
                EditTemplate = new DataTemplate(() =>
                {
                    AddLovedEditView = new AddLovedEditView(this);
                    AddLovedEditView.UpdateImage(IsUpdateFlow);
                    return GetContentView(AddLovedEditView);
                }),
            };

            CarouselViewContent.ItemsSource = new List<int>
            {
                VIEW_LIST_INDEX,
                VIEW_INTRO_INDEX,
                VIEW_PROFILE_INFO_INDEX,
                VIEW_PHYSIC_INFO_INDEX,
                VIEW_IMAGE_INFO_INDEX,
                VIEW_DETAIL_INFO_INDEX,
                VIEW_REVIEW_INDEX,
                VIEW_EDIT_INDEX
            };
        }

        public void ShowIntroView()
        {
            CarouselViewContent.Position = VIEW_INTRO_INDEX;
            MainPage.UpdateRightControlVisibility(true);
            MainPage.UpdateRightControlImage(FontAwesomeIcons.Times);
        }

        public void ShowProfileInfoView()
        {
            CarouselViewContent.Position = VIEW_PROFILE_INFO_INDEX;
        }

        public void ShowPhysicalInfoView()
        {
            CarouselViewContent.Position = VIEW_PHYSIC_INFO_INDEX;
        }

        public void ShowImageInfoView()
        {
            CarouselViewContent.Position = VIEW_IMAGE_INFO_INDEX;
        }

        public void ShowDetailInfoView()
        {
            CarouselViewContent.Position = VIEW_DETAIL_INFO_INDEX;
        }

        public void ShowReviewView()
        {
            CarouselViewContent.Position = VIEW_REVIEW_INDEX;
        }

        public void ShowEditView(bool isUpdate)
        {
            CarouselViewContent.Position = VIEW_EDIT_INDEX;
            IsUpdateFlow = isUpdate;
            MainPage.UpdateRightControlVisibility(true);
            if(AddLovedEditView != null)
            {
                AddLovedEditView.UpdateImage(IsUpdateFlow);
            }
        }

        public View GetContentView(ContentView view)
        {
            Binding contextBinding = new Binding(".", BindingMode.Default, null, null, null, this.BindingContext);
            view.SetBinding(BindingContextProperty, contextBinding);
            var containerView = new Grid();
            containerView.Children.Add(view);
            return containerView;
        }

        public void OnUpdateSuccess()
        {
            MainPage.UpdateRightControlVisibility(false);
            MainPage.UpdateRightControlImage(FontAwesomeIcons.Times);
            ClearImages();
            ResetCommand?.Execute(null);
            CarouselViewContent.Position = VIEW_LIST_INDEX;
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
            if(CarouselViewContent.Position != 0)
            {
                CarouselViewContent.Position = 0;
                ClearImages();
                ResetCommand?.Execute(null);
                MainPage.UpdateRightControlVisibility(false);
            }
        }
    }
}
