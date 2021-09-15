using System;
using System.Collections.Generic;
using System.Windows.Input;
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

        public static readonly BindableProperty ResetCommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(AlertContentView), null, defaultBindingMode: BindingMode.TwoWay);

        public ICommand ResetCommand
        {
            get { return (ICommand)GetValue(ResetCommandProperty); }
            set
            {
                SetValue(ResetCommandProperty, value);
            }
        }

        public static readonly BindableProperty IsUpdateSuccessProperty =
            BindableProperty.Create(nameof(IsUpdateSuccess), typeof(Boolean), typeof(AlertContentView), false, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnIsUpdateSuccessChanged);

        private static void OnIsUpdateSuccessChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is AlertContentView view && newValue is Boolean boolVal)
            {
                if (boolVal == true)
                {
                    view.OnUpdateSuccess();
                    view.IsUpdateSuccess = false;
                }
            }
        }

        private void OnUpdateSuccess()
        {
            CarouselViewContent.Position = VIEW_LIST_INDEX;
            MainPage.UpdateRightControlVisibility(false);
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

        public AlertDetailView AlertDetailView { get; private set; }

        public CreateAlertDetailView CreateAlertDetailView { get; private set; }

        public AlertContentView(HomePage homePage) : base(homePage)
        {
            InitializeComponent();
            MainPage.UpdateRightControlVisibility(false);
            MainPage.UpdateRightControlImage(FontAwesomeIcons.ChevronLeft);
            this.SetBinding(IsUpdateSuccessProperty, "IsUpdateSuccess");
            this.SetBinding(ResetCommandProperty, "ResetCommand");
            SetupView();
        }

        public void ShowDetail()
        {
            CarouselViewContent.Position = VIEW_DETAIL_INDEX;
            if(AlertDetailView != null)
            {
                AlertDetailView.InitView();
            }
        }

        public void ShowCreateAlertChooseLove()
        {
            ResetCommand?.Execute(null);
            CarouselViewContent.Position = VIEW_CREATE_ALERT_CHOOSE_LOVE_INDEX;
        }

        public void ShowCreateAlertDetail()
        {
            CarouselViewContent.Position = VIEW_CREATE_ALERT_DETAIL_INDEX;
            if(CreateAlertDetailView != null)
            {
                CreateAlertDetailView.InitView();
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
                    AlertDetailView = new AlertDetailView(this);
                    return GetContentView(AlertDetailView);
                }),
                CreateAlertChooseLoveTemplate = new DataTemplate(() =>
                {
                    return GetContentView(new CreateAlertChooseLoveView(this));
                }),
                CreateAlertDetailTemplate = new DataTemplate(() =>
                {
                    CreateAlertDetailView = new CreateAlertDetailView(this);
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
            var containerView = new Frame();
            containerView.BackgroundColor = Color.Transparent;
            containerView.HasShadow = false;
            containerView.BorderColor = Color.Transparent;
            containerView.HorizontalOptions = LayoutOptions.Fill;
            containerView.VerticalOptions = LayoutOptions.Fill;
            containerView.Padding = new Thickness(0);
            containerView.Margin = new Thickness(0);
            containerView.Content = view;
            view.HorizontalOptions = LayoutOptions.Fill;
            view.VerticalOptions = LayoutOptions.Fill;
            view.Margin = new Thickness(0);
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
                ResetCommand?.Execute(null);
            }
        }
    }
}
