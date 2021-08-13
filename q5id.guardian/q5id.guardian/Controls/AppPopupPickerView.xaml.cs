using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Rg.Plugins.Popup.Extensions;
using System.Collections;

namespace q5id.guardian.Controls
{
    public partial class AppPopupPickerView : ContentView
    {
        private AppPickerListPage mListPopupPage;

        public static BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(AppPopupPickerView), 0);

        public static BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(AppPopupPickerView), 0);

        public static BindableProperty ContentPaddingProperty =
            BindableProperty.Create(nameof(ContentPadding), typeof(Thickness), typeof(AppPopupPickerView), new Thickness(5));

        public static BindableProperty NormalBorderColorProperty =
            BindableProperty.Create(nameof(NoramlBorderColor), typeof(Color), typeof(AppPopupPickerView), Color.Transparent);

        public static BindableProperty FocusBorderColorProperty =
            BindableProperty.Create(nameof(FocusBorderColor), typeof(Color), typeof(AppPopupPickerView), Color.Transparent);


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
            BindableProperty.Create(nameof(FontSize), typeof(double), typeof(AppPopupPickerView), defaultValue: 12D);

        public double FontSize
        {
            get => (double)GetValue(FontSizeProperty);
            set
            {
                SetValue(FontSizeProperty, value);
                mAppPicker.FontSize = value;
            }
        }

        public static readonly BindableProperty TextColorProperty = BindableProperty.Create(nameof(TextColor), typeof(Color), typeof(AppPopupPickerView), Color.Default);
        public Color TextColor
        {
            set
            {
                SetValue(TextColorProperty, value);
                mAppPicker.TextColor = value;
            }
            get => (Color)GetValue(TextColorProperty);
        }

        public static readonly BindableProperty PlaceholderColorProperty = BindableProperty.Create(nameof(PlaceholderColor), typeof(Color), typeof(AppPopupPickerView), Color.Default);
        public Color PlaceholderColor
        {
            set
            {
                SetValue(PlaceholderColorProperty, value);
                mAppPicker.TitleColor = value;
            }
            get => (Color)GetValue(PlaceholderColorProperty);
        }

        public static readonly BindableProperty PlaceholderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(AppPopupPickerView), "");
        public string Placeholder
        {
            set
            {
                SetValue(PlaceholderProperty, value);
                mAppPicker.Title = value;
            }
            get => (string)GetValue(PlaceholderProperty);
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(AppPopupPickerView), null,
                                    propertyChanged: OnItemsSourceChanged);

        static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var element = newValue as Element;
            if (element == null)
                return;
            element.Parent = (Element)bindable;
        }

        public IList ItemsSource
        {
            get => (IList)GetValue(ItemsSourceProperty);
            set
            {
                SetValue(ItemsSourceProperty, value);
                this.mAppPicker.ItemsSource = value;
            }
        }

        public static readonly BindableProperty ItemTemplateProperty =
            BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(AppPopupPickerView), null,
                                    validateValue: (b, v) => true);

        public DataTemplate ItemTemplate
        {
            get => (DataTemplate)GetValue(ItemTemplateProperty);
            set => SetValue(ItemTemplateProperty, value);
        }

        public static readonly BindableProperty SelectedItemProperty = BindableProperty.Create(nameof(SelectedItem), typeof(object), typeof(AppPopupPickerView), "", BindingMode.TwoWay, propertyChanged: OnItemSelectedChanged);

        private static void OnItemSelectedChanged(BindableObject bindable, object oldValue, object newValue)
        {
           if(bindable is AppPopupPickerView appPopupPickerView)
            {
                appPopupPickerView.SelectedItem = newValue;
            }
        }

        public object SelectedItem
        {
            set
            {
                SetValue(SelectedItemProperty, value);
                mAppPicker.SelectedItem = value;
            }
            get => GetValue(SelectedItemProperty);
        }

        private BindingBase _itemDisplayBinding;

        private static readonly BindableProperty ItemDisplayBindingProperty = BindableProperty.Create(nameof(ItemDisplayBinding), typeof(BindingBase), typeof(AppPopupPickerView), null);

        public BindingBase ItemDisplayBinding
        {
            set
            {
                SetValue(ItemDisplayBindingProperty, value);
                mAppPicker.ItemDisplayBinding = value;
            }
            get => (BindingBase)GetValue(ItemDisplayBindingProperty);
        }

        public AppPopupPickerView()
        {
            InitializeComponent();
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
            this.mAppPicker.Title = this.Placeholder;
            this.mAppPicker.TitleColor = this.PlaceholderColor;
            this.mAppPicker.ItemsSource = this.ItemsSource;
            this.mAppPicker.SelectedItem = this.SelectedItem;
            this.mAppPicker.ItemDisplayBinding = this.ItemDisplayBinding;
        }

        public void SetSelectedObject(object obj)
        {
            SelectedItem = obj;
        }

        async void OnContentTapped(System.Object sender, System.EventArgs e)
        {
            if(mListPopupPage == null)
            {
                mListPopupPage = new AppPickerListPage(this);
            }
            mListPopupPage.UpdateList(ItemsSource, ItemTemplate);
            await Navigation.PushPopupAsync(mListPopupPage);
        }
    }
}
