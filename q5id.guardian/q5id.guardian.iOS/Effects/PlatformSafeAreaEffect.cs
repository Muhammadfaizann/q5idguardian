using System;
using System.Linq;
using CoreGraphics;
using q5id.guardian.Effects;
using q5id.guardian.iOS.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("Xamarin")]
[assembly: ExportEffect(typeof(PlatformSafeAreaEffect), "SafeAreaEffect")]
namespace q5id.guardian.iOS.Effects
{
    public class PlatformSafeAreaEffect : PlatformEffect
    {
        Thickness _padding;

        protected override void OnAttached()
        {
            try
            {
                var effect = (SafeAreaEffect)Element.Effects.FirstOrDefault(e => e is SafeAreaEffect);
                if (effect != null)
                {
                    if (Element is Layout element)
                    {
                        double paddingTop = 0;
                        double paddingBottom = 0;
                        if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                        {
                            _padding = element.Padding;
                            var insets = UIApplication.SharedApplication.Windows[0].SafeAreaInsets; // Can't use KeyWindow this early
                            if (insets.Top > 0) // We have a notch
                            {
                                paddingTop = effect.IsPaddingTop ? _padding.Top + insets.Top : _padding.Top;
                                paddingBottom = effect.IsPaddingBottom ? _padding.Bottom + insets.Bottom : _padding.Bottom;
                                element.Padding = new Thickness(_padding.Left + insets.Left, paddingTop, _padding.Right + insets.Right, paddingBottom);
                                return;
                            }
                        }
                        // Uses a default Padding of 20. Could use an property to modify if you wanted.
                        paddingTop = effect.IsPaddingTop ? _padding.Top + 20 : _padding.Top;
                        element.Padding = new Thickness(_padding.Left, paddingTop, _padding.Right, _padding.Bottom);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            if (Element is Layout element)
            {
                element.Padding = _padding;
            }
        }
    }
}