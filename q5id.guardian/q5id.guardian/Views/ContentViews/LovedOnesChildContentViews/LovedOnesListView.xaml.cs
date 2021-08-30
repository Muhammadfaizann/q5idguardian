using System;
using System.Collections.Generic;
using q5id.guardian.Views.Base;
using Xamarin.Forms;

namespace q5id.guardian.Views.ContentViews.LovedOnesChildContentViews
{
    public partial class LovedOnesListView : BaseChildContentView
    {
        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(LovedOnesListView), null, defaultBindingMode: BindingMode.TwoWay, propertyChanged: OnSelectedItemChanged);

        private static void OnSelectedItemChanged(BindableObject bindable, object oldValue, object newValue)
        {
            
        }

        public object SelectedItem
        {
            get
            {
                return (object)GetValue(SelectedItemProperty);
            }
            set
            {
                SetValue(SelectedItemProperty, value);
            }
        }

        public LovedOnesListView(BaseContainerView mainCtv) : base(mainCtv)
        {
            InitializeComponent();
            this.SetBinding(SelectedItemProperty, "SelectedLovedOnes");
            ViewTitle = "Loved Ones";
        }

        void OnAddClicked(System.Object sender, System.EventArgs e)
        {
            SelectedItem = null;
            MainContentView.MainPage.UpdateRightControlImage(Utils.FontAwesomeIcons.Times);
            if(MainContentView is LovedOnesContentView lovedOnesContentView)
            {
                lovedOnesContentView.ShowIntroView();
            }

        }

        void OnItemFrameTapped(System.Object sender, System.EventArgs e)
        {
            if(e is TappedEventArgs tappedEventArgs)
            {
                SelectedItem = tappedEventArgs.Parameter;
                if (MainContentView is LovedOnesContentView lovedOnesContentView)
                {
                    lovedOnesContentView.ShowEditView(true);
                }
            }
        }
    }
}
