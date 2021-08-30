using System;
using System.Collections.Generic;
using q5id.guardian.Selectors;
using q5id.guardian.Utils;
using q5id.guardian.Views.Base;
using q5id.guardian.Views.ContentViews.AlertContentChildViews;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews
{
    public partial class AlertContentView : BaseContainerView
    {

        public const int VIEW_LIST_INDEX = 0;
        public const int VIEW_DETAIL_INDEX = 1;
        public const int VIEW_CREATE_ALERT_CHOOSE_LOVE_INDEX = 2;
        public const int VIEW_CREATE_ALERT_DETAIL_INDEX = 3;

        public AlertDetailView AlertDetailView { get; private set; }

        public CreateAlertDetailView CreateAlertDetailView { get; private set; }

        private object mAlertDetail;

        private object mLove;

        public AlertContentView(HomePage homePage) : base(homePage)
        {
            InitializeComponent();
            MainPage.UpdateRightControlVisibility(false);
            MainPage.UpdateRightControlImage(FontAwesomeIcons.ChevronLeft);
            SetupView();
        }

        public void ShowDetail(object parameter)
        {
            CarouselViewContent.Position = VIEW_DETAIL_INDEX;
            mAlertDetail = parameter;
            if(AlertDetailView != null)
            {
                AlertDetailView.AlertDetail = parameter;
            }
        }

        public void ShowCreateAlertChooseLove()
        {
            CarouselViewContent.Position = VIEW_CREATE_ALERT_CHOOSE_LOVE_INDEX;
        }

        public void ShowCreateAlertDetail(object parameter)
        {
            CarouselViewContent.Position = VIEW_CREATE_ALERT_DETAIL_INDEX;
            mLove = parameter;
            if(CreateAlertDetailView != null)
            {
                CreateAlertDetailView.Love = mLove;
            }
        }

        private void SetupView()
        { 
            CarouselViewContent.ItemTemplate = new AlertPageSelector()
            {
                ListTemplate = new DataTemplate(() =>
                {
                    return GetContentView(new AlertListVew(this));
                }),
                DetailTemplate = new DataTemplate(() =>
                {
                    AlertDetailView = new AlertDetailView(this, mAlertDetail);
                    return GetContentView(AlertDetailView);
                }),
                CreateAlertChooseLoveTemplate = new DataTemplate(() =>
                {
                    return GetContentView(new CreateAlertChooseLoveView(this));
                }),
                CreateAlertDetailTemplate = new DataTemplate(() =>
                {
                    CreateAlertDetailView = new CreateAlertDetailView(this, mLove);
                    return GetContentView(CreateAlertDetailView);
                }),
            };

            CarouselViewContent.ItemsSource = new List<int>
            {
                VIEW_LIST_INDEX,
                VIEW_DETAIL_INDEX,
                VIEW_CREATE_ALERT_CHOOSE_LOVE_INDEX,
                VIEW_CREATE_ALERT_DETAIL_INDEX
            };
        }

        public View GetContentView(ContentView view)
        {
            Binding contextBinding = new Binding(".", BindingMode.Default, null, null, null, this.BindingContext);
            view.SetBinding(BindingContextProperty, contextBinding);
            var containerView = new Grid();
            containerView.Children.Add(view);
            return containerView;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            if(Parent == null)
            {
                MainPage.RightControlClicked -= MainPage_RightControlClicked;
            }
            else
            {
                MainPage.RightControlClicked += MainPage_RightControlClicked;
            }
        }

        public void AddNewEventHandlerRightControlClicked(EventHandler eventHandler)
        {
            MainPage.RightControlClicked -= MainPage_RightControlClicked;
            MainPage.RightControlClicked += eventHandler;
        }

        public void RemoveEventHandlerRightControlClicked(EventHandler eventHandler)
        {
            MainPage.RightControlClicked -= eventHandler;
            MainPage.RightControlClicked += MainPage_RightControlClicked;
        }

        private void MainPage_RightControlClicked(object sender, EventArgs e)
        {
            if(CarouselViewContent.Position != VIEW_LIST_INDEX)
            {
                CarouselViewContent.Position = VIEW_LIST_INDEX;
                MainPage.UpdateRightControlVisibility(false);
            }
        }
    }
}
