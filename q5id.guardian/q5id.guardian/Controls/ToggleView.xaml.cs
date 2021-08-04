using System;
using System.Collections.Generic;
using q5id.guardian.Utils;
using Xamarin.Forms;

namespace q5id.guardian.Controls
{
    public partial class ToggleView : ContentView
    {
        public static BindableProperty ImageSizeProperty =
            BindableProperty.Create(nameof(ImageSize), typeof(int), typeof(ToggleView), 25);

        public int ImageSize
        {
            get => (int)GetValue(ImageSizeProperty);
            set
            {
                SetValue(ImageSizeProperty, value);
                UpdateImage();
            }
        }

        public static readonly BindableProperty ActiveBackgroundColorProperty = BindableProperty.Create(nameof(ActiveBackgroundColor), typeof(Color), typeof(ToggleView), Color.Transparent);
        public Color ActiveBackgroundColor
        {
            set => SetValue(ActiveBackgroundColorProperty, value);
            get => (Color)GetValue(ActiveBackgroundColorProperty);
        }

        public static readonly BindableProperty NormalBackgroundColorProperty = BindableProperty.Create(nameof(NormalBackgroundColor), typeof(Color), typeof(ToggleView), Color.Transparent);
        public Color NormalBackgroundColor
        {
            set => SetValue(NormalBackgroundColorProperty, value);
            get => (Color)GetValue(NormalBackgroundColorProperty);
        }

        public static readonly BindableProperty ActiveIconColorProperty = BindableProperty.Create(nameof(ActiveIconColor), typeof(Color), typeof(ToggleView), Color.Transparent);
        public Color ActiveIconColor
        {
            set => SetValue(ActiveIconColorProperty, value);
            get => (Color)GetValue(ActiveIconColorProperty);
        }

        public static readonly BindableProperty NormalIconColorProperty = BindableProperty.Create(nameof(NormalIconColor), typeof(Color), typeof(ToggleView), Color.Transparent);
        public Color NormalIconColor
        {
            set => SetValue(NormalIconColorProperty, value);
            get => (Color)GetValue(NormalIconColorProperty);
        }

        public static BindableProperty ImageGlyphProperty =
            BindableProperty.Create(nameof(ImageGlyph), typeof(string), typeof(ToggleView), "");

        public string ImageGlyph
        {
            get => (string)GetValue(ImageGlyphProperty);
            set => SetValue(ImageGlyphProperty, value);
        }

        public static BindableProperty IsActiveProperty =
            BindableProperty.Create(nameof(IsActive), typeof(bool), typeof(ToggleView), false);

        public bool IsActive
        {
            get => (bool)GetValue(IsActiveProperty);
            set => SetValue(IsActiveProperty, value);
        }

        public static BindableProperty CornerRadiusProperty =
           BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(ToggleView), 0);
        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set => SetValue(CornerRadiusProperty, value);
        }

        private FontImageSource iconImageSource;

        public event EventHandler Changed;

        public ToggleView()
        {
            InitializeComponent();
            
        }

        private void UpdateContentView()
        {
            UpdateImage();
            this.BackgroundColor = Color.Transparent;
            frameContent.BackgroundColor = this.NormalBackgroundColor;
            frameContent.CornerRadius = this.CornerRadius;
            TapGestureRecognizer contentTapGes = new TapGestureRecognizer();
            contentTapGes.Tapped += ContentTapGes_Tapped;
            frameContent.GestureRecognizers.Add(contentTapGes);
        }

        public override ControlTemplate ResolveControlTemplate()
        {
            return base.ResolveControlTemplate();
        }

        protected override void InvalidateLayout()
        {
            base.InvalidateLayout();
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            UpdateContentView();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
        }

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            frameContent.BackgroundColor = this.NormalBackgroundColor;
        }

        private void ContentTapGes_Tapped(object sender, EventArgs e)
        {
            IsActive = !IsActive;
            Changed?.Invoke(this, e);
            UpdateView();
        }

        private void UpdateImage()
        {
            iconImageSource = new FontImageSource();
            iconImageSource.FontFamily = ThemeConstanst.FontAwesomeSolid;
            iconImageSource.Color = NormalIconColor;
            iconImageSource.Glyph = ImageGlyph;
            iconImageSource.Size = ImageSize;
            imageIcon.Source = iconImageSource;
        }

        private void UpdateView()
        {
            this.BackgroundColor = Color.Transparent;
            var backgroundColor = IsActive ? ActiveBackgroundColor : NormalBackgroundColor;
            var iconColor = IsActive ? ActiveIconColor : NormalIconColor;
            frameContent.Background = Brush.Transparent;
            frameContent.BackgroundColor = backgroundColor;
            frameContent.CornerRadius = this.CornerRadius;
            iconImageSource.Color = iconColor;
        }
    }
}
