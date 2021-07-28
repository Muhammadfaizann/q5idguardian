using System;
using Xamarin.Forms;

namespace q5id.guardian.Effects
{
    public class SafeAreaEffect : RoutingEffect
	{
		public Boolean IsPaddingTop { get; set; } = false;
		public Boolean IsPaddingBottom { get; set; } = false;

		public SafeAreaEffect() : base($"Xamarin.{nameof(SafeAreaEffect)}")
		{
		}
	}
}
