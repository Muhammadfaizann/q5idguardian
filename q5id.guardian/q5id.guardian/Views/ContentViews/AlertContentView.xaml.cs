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
        public const int NAVIGATE_FROM_HOME = 0;
        public const int NAVIGATE_FROM_LOVED_ONES = 1;

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
            BackToTop();
            MainPage.UpdateRightControlVisibility(false);
            IsFromHomeView = false;
            IsFromLovedOnesView = false;
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

        public bool IsFromHomeView { get; private set; }
        public bool IsFromLovedOnesView { get; private set; }

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
            PushView(new AlertDetailView(this));
        }

        public void ShowCreateAlertChooseLove()
        {
           ResetCommand?.Execute(null);
           PushView(new CreateAlertChooseLoveView(this));
        }

        public void ShowCreateAlertDetail()
        {
            PushView(new CreateAlertDetailView(this));
        }

        private void SetupView()
        {
            PushView(new AlertListVew(this));
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

        public void ShowCreateAlert(int fromView)
        {
            ShowCreateAlertChooseLove();
            IsFromHomeView = fromView == NAVIGATE_FROM_HOME ? true : false;
            IsFromLovedOnesView = fromView == NAVIGATE_FROM_LOVED_ONES ? true : false;
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
            if (IsFromHomeView)
            {
                MainPage.ShowHomeView();
                IsFromHomeView = false;
            }
            else if (IsFromLovedOnesView)
            {
                MainPage.ShowLovedOnesView();
                IsFromLovedOnesView = false;
            }
            else
            {
                MainPage.UpdateRightControlVisibility(false);
                ResetCommand?.Execute(null);
                BackToTop();
            }
        }

        public override Grid GetContentView()
        {
            return this.GridContentView;
        }
    }
}
