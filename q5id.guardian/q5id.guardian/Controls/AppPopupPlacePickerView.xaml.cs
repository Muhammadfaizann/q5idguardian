using System;
using System.Collections;
using System.Collections.Generic;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms.Platform;

namespace q5id.guardian.Controls
{
    public partial class AppPopupPlacePickerView : ContentView
    {
        private AppPlacePickerListPage mListPopupPage;

        public static BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(AppPopupPlacePickerView), 0);

        public static BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(AppPopupPlacePickerView), 0);

        public static BindableProperty ContentPaddingProperty =
            BindableProperty.Create(nameof(ContentPadding), typeof(Thickness), typeof(AppPopupPlacePickerView), new Thickness(5));

        public static BindableProperty NormalBorderColorProperty =
            BindableProperty.Create(nameof(NoramlBorderColor), typeof(Color), typeof(AppPopupPlacePickerView), Color.Transparent);

        public static BindableProperty FocusBorderColorProperty =
            BindableProperty.Create(nameof(FocusBorderColor), typeof(Color), typeof(AppPopupPlacePickerView), Color.Transparent);


        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set
            {
                SetValue(CornerRadiusProperty, value);
                mAppPicker.CornerRadius = value;
            }
        }

        public int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set
            {
                SetValue(BorderThicknessProperty, value);
                mAppPicker.BorderThickness = value;
            }
        }
        public Color NoramlBorderColor
        {
            get => (Color)GetValue(NormalBorderColorProperty);
            set
            {
                SetValue(NormalBorderColorProperty, value);
                mAppPicker.NoramlBorderColor = value;
            }
        }
        public Color FocusBorderColor
        {
            get => (Color)GetValue(FocusBorderColorProperty);
            set
            {
                SetValue(FocusBorderColorProperty, value);
                mAppPicker.FocusBorderColor = value;
            }
        }
        /// <summary>
        /// This property cannot be changed at runtime in iOS.
        /// </summary>
        public Thickness ContentPadding
        {
            get => (Thickness)GetValue(ContentPaddingProperty);
            set
            {
                SetValue(ContentPaddingProperty, value);
                mAppPicker.Padding = value;
            }
        }

        public static BindableProperty FontSizeProperty =
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(AppPopupPlacePickerView), defaultValue: 12D);

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set
            {
                SetValue(FontSizeProperty, value);
                mAppPicker.FontSize = value;
            }
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(AppPopupPlacePickerView), Color.Default);
        public Color TextColor
        {
            set
            {
                SetValue(TextColorProperty, value);
                mAppPicker.TextColor = value;
            }
            get => (Color)GetValue(TextColorProperty);
        }

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(AppPopupPlacePickerView), Color.Default);
        public Color PlaceholderColor
        {
            set
            {
                SetValue(PlaceholderColorProperty, value);
                mAppPicker.PlaceholderColor = value;
            }
            get => (Color)GetValue(PlaceholderColorProperty);
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(AppPopupPickerView), "");
        public string Placeholder
        {
            set
            {
                SetValue(PlaceholderProperty, value);
                mAppPicker.Placeholder = value;
            }
            get => (string)GetValue(PlaceholderProperty);
        }

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(AppPopupPlacePickerView), "", BindingMode.TwoWay, propertyChanged: OnItemSelectedChanged);

        private static void OnItemSelectedChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is AppPopupPlacePickerView appPopupPicker)
            {
                appPopupPicker.GetDisplayMember();
            }
        }

        public event EventHandler SelectedItemChanged;

        public object SelectedItem
        {
            set
            {
                SetValue(SelectedItemProperty, value);
                GetDisplayMember();
                SelectedItemChanged?.Invoke(this, null);
            }
            get => GetValue(SelectedItemProperty);
        }

        private static readonly BindableProperty ItemDisplayBindingPathProperty = BindableProperty.Create(nameof(ItemDisplayBindingPath), typeof(string), typeof(AppPopupPlacePickerView), "");

        public string ItemDisplayBindingPath
        {
            set
            {
                SetValue(ItemDisplayBindingPathProperty, value);
                GetDisplayMember();
            }
            get => (string)GetValue(ItemDisplayBindingPathProperty);
        }


        void GetDisplayMember()
        {
            if (ItemDisplayBindingPath == null || ItemDisplayBindingPath == "")
            {
                mAppPicker.RemoveBinding(AppEntry.TextProperty);
                mAppPicker.Text = "";
                return;
            }
            Binding displayBinding = new Binding(ItemDisplayBindingPath, BindingMode.OneWay, null, null, null, SelectedItem);
            mAppPicker.BindingContext = SelectedItem;
            mAppPicker.SetBinding(AppEntry.TextProperty, displayBinding);
        }


        protected override void OnParentSet()
        {
            base.OnParentSet();
            UpdateView();
        }

        private void UpdateView()
        {
            this.mAppPicker.NoramlBorderColor = this.NoramlBorderColor;
            this.mAppPicker.FocusBorderColor = this.FocusBorderColor;
            this.mAppPicker.CornerRadius = this.CornerRadius;
            this.mAppPicker.BorderThickness = this.BorderThickness;
            this.mAppPicker.Padding = this.ContentPadding;
            this.mAppPicker.FontSize = this.FontSize;
            this.mAppPicker.TextColor = this.TextColor;
            this.mAppPicker.Placeholder = this.Placeholder;
            this.mAppPicker.PlaceholderColor = this.PlaceholderColor;
            GetDisplayMember();
        }

        public void SetSelectedObject(object obj)
        {
            SelectedItem = obj;
        }

        async void OnContentTapped(System.Object sender, System.EventArgs e)
        {
            if (mListPopupPage == null)
            {
                mListPopupPage = new AppPlacePickerListPage(this);
            }
            mListPopupPage.ResetView();
            await Navigation.PushPopupAsync(mListPopupPage);
        }

        public AppPopupPlacePickerView()
        {
            InitializeComponent();
        }
    }
}
