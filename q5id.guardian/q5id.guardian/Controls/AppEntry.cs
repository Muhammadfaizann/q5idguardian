using System;
using q5id.guardian.Utils;
using Xamarin.Forms;

namespace q5id.guardian.Controls
{
    public class AppEntry : Entry
    {
        public static BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(AppEntry), 0);

        public static BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(AppEntry), 0);

        public static BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(AppEntry), new Thickness(5));

        public static BindableProperty NormalBorderColorProperty =
            BindableProperty.Create(nameof(NoramlBorderColor), typeof(Color), typeof(AppEntry), Color.Transparent);

        public static BindableProperty FocusBorderColorProperty =
            BindableProperty.Create(nameof(FocusBorderColor), typeof(Color), typeof(AppEntry), Color.Transparent);


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

        protected override void OnTextChanged(string oldValue, string newValue)
        {
            base.OnTextChanged(oldValue, newValue);
            if(this.Text == "")
            {
                this.FontFamily = ThemeConstanst.FontPoppinsItalic;
            }
            else
            {
                this.FontFamily = ThemeConstanst.FontPoppinsRegular;
            }
        }
    }
}
