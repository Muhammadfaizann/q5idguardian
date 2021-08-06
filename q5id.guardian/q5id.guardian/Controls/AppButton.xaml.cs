using System;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace q5id.guardian.Controls
{
    public partial class AppButton : ContentView
    {
        public static readonly BindableProperty NormalBackgroundStartColorProperty = BindableProperty.Create(nameof(NormalBackgroundStartColor), typeof(Color), typeof(AppButton), Color.Black);
        public Color NormalBackgroundStartColor
        {
            set
            {
                SetValue(NormalBackgroundStartColorProperty, value);
                this.frameContent.NormalBackgroundColorStart = value;
            }
            get => (Color)GetValue(NormalBackgroundStartColorProperty);
        }

        public static readonly BindableProperty NormalBackgroundEndColorProperty = BindableProperty.Create(nameof(NormalBackgroundEndColor), typeof(Color), typeof(AppButton), Color.Black);
        public Color NormalBackgroundEndColor
        {
            set
            {
                SetValue(NormalBackgroundEndColorProperty, value);
                this.frameContent.NormalBackgroundColorEnd = value;
            }
            get => (Color)GetValue(NormalBackgroundEndColorProperty);
        }

        public static readonly BindableProperty PressedBackgroundColorProperty = BindableProperty.Create(nameof(PressedBackgroundColor), typeof(Color), typeof(AppButton), Color.Black);
        public Color PressedBackgroundColor
        {
            set
            {
                SetValue(PressedBackgroundColorProperty, value);
                this.frameContent.PressedBackgroundColor = value;
            }
            get => (Color)GetValue(PressedBackgroundColorProperty);
        }

        public static readonly BindableProperty DeactiveBackgroundColorProperty = BindableProperty.Create(nameof(DeactiveBackgroundColor), typeof(Color), typeof(AppButton), Color.Black);
        public Color DeactiveBackgroundColor
        {
            set
            {
                SetValue(DeactiveBackgroundColorProperty, value);
                this.frameContent.DeactiveBackgroundColor = value;
            }
            get => (Color)GetValue(DeactiveBackgroundColorProperty);
        }

        public static BindableProperty CornerRadiusProperty =
            BindableProperty.Create(nameof(CornerRadius), typeof(int), typeof(AppButton), 0);

        public int CornerRadius
        {
            get => (int)GetValue(CornerRadiusProperty);
            set
            {
                SetValue(CornerRadiusProperty, value);
                this.frameContent.CornerRadius = value;
            }
        }

        public static readonly BindableProperty HasShadowProperty =
            BindableProperty.Create(nameof(HasShadow), typeof(Boolean), typeof(AppButton), false);

        public Boolean HasShadow
        {
            get { return (Boolean)GetValue(HasShadowProperty); }
            set
            {
                this.frameContent.HasShadow = value;
                SetValue(HasShadowProperty, value);
            }
        }

        public static readonly BindableProperty BorderColorProperty = BindableProperty.Create(nameof(BorderColor), typeof(Color), typeof(AppButton), Color.Black);
        public Color BorderColor
        {
            set
            {
                SetValue(BorderColorProperty, value);
                this.frameContent.BorderColor = value;
            }
            get => (Color)GetValue(BorderColorProperty);
        }

        // BindableProperty implementation
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(AppButton), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set
            {
                SetValue(CommandProperty, value);
            }
        }

        // BindableProperty implementation
        public static readonly BindableProperty IsDisableProperty =
            BindableProperty.Create(nameof(IsDisable), typeof(Boolean), typeof(AppButton), false, BindingMode.TwoWay, null, OnIsDisablePropertyChanged);

        private static void OnIsDisablePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if(bindable is AppButton appButton && newValue is Boolean isDisable)
            {
                appButton.frameContent.IsDisable = isDisable;
                appButton.labelContent.TextColor = isDisable ? appButton.DeactiveLabelColor : appButton.NormalLabelColor;
            }
        }

        public Boolean IsDisable
        {
            get { return (Boolean)GetValue(IsDisableProperty); }
            set
            {
                frameContent.IsDisable = value;
                labelContent.TextColor = value ? DeactiveLabelColor : NormalLabelColor;
                SetValue(IsDisableProperty, value);
            }
        }

        public static readonly BindableProperty NormalLabelColorProperty = BindableProperty.Create(nameof(NormalLabelColor), typeof(Color), typeof(AppButton), Color.Black);
        public Color NormalLabelColor
        {
            set
            {
                SetValue(NormalLabelColorProperty, value);
                this.labelContent.TextColor = IsDisable ? DeactiveLabelColor : value;
            }
            get => (Color)GetValue(NormalLabelColorProperty);
        }

        public static readonly BindableProperty PressedLabelColorProperty = BindableProperty.Create(nameof(PressedLabelColor), typeof(Color), typeof(AppButton), Color.Black);
        public Color PressedLabelColor
        {
            set => SetValue(PressedLabelColorProperty, value);
            get => (Color)GetValue(PressedLabelColorProperty);
        }

        public static readonly BindableProperty DeactiveLabelColorProperty = BindableProperty.Create(nameof(DeactiveLabelColor), typeof(Color), typeof(AppButton), Color.Black);
        public Color DeactiveLabelColor
        {
            set
            {
                SetValue(DeactiveLabelColorProperty, value);
                this.labelContent.TextColor = IsDisable ? value : NormalLabelColor;
            }
            get => (Color)GetValue(DeactiveLabelColorProperty);
        }

        public static BindableProperty ButtonTitleProperty =
            BindableProperty.Create(nameof(ButtonTitle), typeof(string), typeof(AppButton), "");

        public string ButtonTitle
        {
            get => (string)GetValue(ButtonTitleProperty);
            set
            {
                SetValue(ButtonTitleProperty, value);
                this.labelContent.Text = value;
            }
        }

        public static BindableProperty ButtonTitleSizeProperty =
            BindableProperty.Create(nameof(ButtonTitleSize), typeof(double), typeof(AppButton), Label.FontSizeProperty.DefaultValue);

        public double ButtonTitleSize
        {
            get => (double)GetValue(ButtonTitleSizeProperty);
            set
            {
                SetValue(ButtonTitleSizeProperty, value);
                this.labelContent.FontSize = value;
            }
        }

        public event EventHandler Clicked;

        public AppButton()
        {
            InitializeComponent();
            frameContent.TouchBegin += FrameContent_TouchBegin;
            frameContent.TouchEnd += FrameContent_TouchEnd;
            frameContent.Clicked += FrameContent_Clicked;
        }

        protected override void OnParentSet()
        {
            base.OnParentSet();
            UpdateView();
        }

        private void UpdateView()
        {
            this.frameContent.NormalBackgroundColorStart = this.NormalBackgroundStartColor;
            this.frameContent.NormalBackgroundColorEnd = this.NormalBackgroundEndColor;
            this.frameContent.PressedBackgroundColor = this.PressedBackgroundColor;
            this.frameContent.DeactiveBackgroundColor = this.DeactiveBackgroundColor;
            this.frameContent.BorderColor = this.BorderColor;
            this.frameContent.CornerRadius = this.CornerRadius;
            this.frameContent.IsDisable = this.IsDisable;
            this.frameContent.HasShadow = this.HasShadow;

            this.labelContent.TextColor = this.IsDisable ? this.DeactiveLabelColor : this.NormalLabelColor;
            this.labelContent.Text = this.ButtonTitle;
            this.labelContent.FontSize = this.ButtonTitleSize;
        }

        private void FrameContent_Clicked(object sender, EventArgs e)
        {
            Clicked?.Invoke(sender, e);
            Command?.Execute(sender);
        }

        private void FrameContent_TouchEnd(object sender, EventArgs e)
        {
            if(this.IsDisable == false)
            {
                this.labelContent.TextColor = this.NormalLabelColor;
            }
        }

        private void FrameContent_TouchBegin(object sender, EventArgs e)
        {
            if (this.IsDisable == false)
            {
                this.labelContent.TextColor = this.PressedLabelColor;
            }
        }
    }
}
