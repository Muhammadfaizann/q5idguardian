using System;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace q5id.guardian.Controls
{
    public class AppFrame : Frame
    {
        private bool isTouching = false;

        public static readonly BindableProperty NormalBackgroundColorStartProperty = BindableProperty.Create(nameof(NormalBackgroundColorStart), typeof(Color), typeof(AppFrame), Color.Black);
        public Color NormalBackgroundColorStart
        {
            set => SetValue(NormalBackgroundColorStartProperty, value);
            get => (Color)GetValue(NormalBackgroundColorStartProperty);
        }

        public static readonly BindableProperty NormalBackgroundColorEndProperty = BindableProperty.Create(nameof(NormalBackgroundColorEnd), typeof(Color), typeof(AppFrame), Color.Black);
        public Color NormalBackgroundColorEnd
        {
            set => SetValue(NormalBackgroundColorEndProperty, value);
            get => (Color)GetValue(NormalBackgroundColorEndProperty);
        }

        public static readonly BindableProperty PressedBackgroundColorProperty = BindableProperty.Create(nameof(PressedBackgroundColor), typeof(Color), typeof(AppFrame), Color.Black);
        public Color PressedBackgroundColor
        {
            set => SetValue(PressedBackgroundColorProperty, value);
            get => (Color)GetValue(PressedBackgroundColorProperty);
        }

        public static readonly BindableProperty DeactiveBackgroundColorProperty = BindableProperty.Create(nameof(DeactiveBackgroundColor), typeof(Color), typeof(AppFrame), Color.Black);
        public Color DeactiveBackgroundColor
        {
            set => SetValue(DeactiveBackgroundColorProperty, value);
            get => (Color)GetValue(DeactiveBackgroundColorProperty);
        }

        // BindableProperty implementation
        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(AppFrame), null);

        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // BindableProperty implementation
        public static readonly BindableProperty IsDisableProperty =
            BindableProperty.Create(nameof(IsDisable), typeof(Boolean), typeof(AppFrame), false);

        public Boolean IsDisable
        {
            get { return (Boolean)GetValue(IsDisableProperty); }
            set {
                SetValue(IsDisableProperty, value);
            }
        }

        public event EventHandler Clicked;
        public event EventHandler TouchBegin;
        public event EventHandler TouchEnd;

        public AppFrame()
        {
        }

        public void SendClickedCommand()
        {
            Command?.Execute(null);
            Clicked?.Invoke(this, null);
        }

        public void OnTouchBegin()
        {
            TouchBegin?.Invoke(this, null);
        }

        public void OnTouchEnd()
        {
            TouchEnd?.Invoke(this, null);
        }
    }
}
