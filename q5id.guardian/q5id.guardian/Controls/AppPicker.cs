using System;
using Xamarin.Forms;

namespace q5id.guardian.Controls
{
    public class AppPicker : Picker
    {
        public static BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(AppPicker), 0);

        public static BindableProperty BorderThicknessProperty =
            BindableProperty.Create(nameof(BorderThickness), typeof(int), typeof(AppPicker), 0);

        public static BindableProperty PaddingProperty =
            BindableProperty.Create(nameof(Padding), typeof(Thickness), typeof(AppPicker), new Thickness(5));

        public static BindableProperty NormalBorderColorProperty =
            BindableProperty.Create(nameof(NoramlBorderColor), typeof(Color), typeof(AppPicker), Color.Transparent);

        public static BindableProperty FocusBorderColorProperty =
            BindableProperty.Create(nameof(FocusBorderColor), typeof(Color), typeof(AppPicker), Color.Transparent);


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

        public AppPicker()
        {
            
        }
    }
}
