using System;
using q5id.guardian.Utils;
using Xamarin.Forms;

namespace q5id.guardian.Controls
{
    public class AppDatePicker : DatePicker
    {
        private string _format = null;
        
        public static readonly BindableProperty NullableDateProperty = BindableProperty.Create(nameof(NullableDate), typeof(DateTime?), typeof(AppDatePicker), null, BindingMode.TwoWay, null, propertyChanged: OnNullableDateChanged);

        public static void OnNullableDateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is AppDatePicker datePicker)
            {
                datePicker.UpdateDate();
            }
        }

        public DateTime? NullableDate
        {
            get { return (DateTime?)GetValue(NullableDateProperty); }
            set { SetValue(NullableDateProperty, value); UpdateDate(); }
        }

        public void UpdateDate()
        {
            if (NullableDate.HasValue)
            {
                Date = NullableDate.Value;
                this.FontFamily = ThemeConstanst.FontPoppinsRegular;
            }
            else
            {
                this.FontFamily = ThemeConstanst.FontPoppinsItalic;
            }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            UpdateDate();
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);
            if (propertyName == DatePicker.DateProperty.PropertyName) NullableDate = Date;
        }

        public static readonly BindableProperty PlaceHolderProperty = BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(AppDatePicker), defaultValue: "");

        public static void OnPlaceholderChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is AppDatePicker datePicker)
            {
                datePicker.UpdateDate();
            }
        }

        public string Placeholder
        {
            get { return (string)GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); UpdateDate(); }
        }

        public static BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(AppDatePicker), 0);

        public static BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(AppDatePicker), 0);

        public static BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(AppDatePicker), new Thickness(5));

        public static BindableProperty NormalBorderColorProperty =
            BindableProperty.Create(nameof(NoramlBorderColor), typeof(Color), typeof(AppDatePicker), Color.Transparent);

        public static BindableProperty FocusBorderColorProperty =
            BindableProperty.Create(nameof(FocusBorderColor), typeof(Color), typeof(AppDatePicker), Color.Transparent);


        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        public int BorderThickness
        {
            get => (int)GetValue(BorderThicknessProperty);
            set => SetValue(BorderThicknessProperty, value);
        }
        public Color NoramlBorderColor
        {
            get => (Color)GetValue(NormalBorderColorProperty);
            set => SetValue(NormalBorderColorProperty, value);
        }
        public Color FocusBorderColor
        {
            get => (Color)GetValue(FocusBorderColorProperty);
            set => SetValue(FocusBorderColorProperty, value);
        }
        /// <summary>
        /// This property cannot be changed at runtime in iOS.
        /// </summary>
        public Thickness Padding
        {
            get => (Thickness)GetValue(PaddingProperty);
            set => SetValue(PaddingProperty, value);
        }
    }
}
