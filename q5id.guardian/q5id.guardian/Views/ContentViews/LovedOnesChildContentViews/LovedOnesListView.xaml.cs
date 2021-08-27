using System;
using System.Collections.Generic;
using q5id.guardian.Views.Base;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public partial class LovedOnesListView : BaseChildContentView
    {
        public static readonly BindableProperty SelectedItemIndexProperty =
            BindableProperty.Create(nameof(SelectedItemIndex), typeof(int), typeof(LovedOnesListView), -1, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSelectedItemIndexChanged);

        private static void OnSelectedItemIndexChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }

        public int SelectedItemIndex
        {
            get
            {
                return (int)GetValue(SelectedItemIndexProperty);
            }
            set
            {
                SetValue(SelectedItemIndexProperty, value);
            }
        }

        public LovedOnesListView(BaseContainerView mainCtv) : base(mainCtv)
        {
            InitializeComponent();
            this.SetBinding(SelectedItemIndexProperty, "SelectedLovedOnesIndex");
            ViewTitle = "Loved Ones";
        }

        void OnAddClicked(System.Object sender, System.EventArgs e)
        {
            SelectedItemIndex = -1;
            MainContentView.MainPage.UpdateRightControlImage(Utils.FontAwesomeIcons.Times);
            MainContentView.PushView(new AddLovedIntroView(MainContentView));

        }

        void OnLovedOnesItemTapped(System.Object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            SelectedItemIndex = e.ItemIndex;
            MainContentView.MainPage.UpdateRightControlImage(Utils.FontAwesomeIcons.ChevronLeft);
            MainContentView.PushView(new AddLovedEditView(MainContentView, true));
        }
    }
}
