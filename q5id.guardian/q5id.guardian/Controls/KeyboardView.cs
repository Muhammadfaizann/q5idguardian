using System;
using Xamarin.Forms;

namespace q5id.guardian.Controls
{
    public class KeyboardView : Grid
    {
        public double MarginBottom { get; set; } = 0;

        public event EventHandler<Boolean> OnKeyBoardUpdate;

        public KeyboardView()
        {
        }

        public void UpdateKeyBoardState(bool isShow)
        {
            OnKeyBoardUpdate?.Invoke(this, isShow);
        }
    }
}
