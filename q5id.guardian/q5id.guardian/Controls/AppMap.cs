using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using q5id.guardian.Models;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using static Xamarin.Forms.BindableProperty;

namespace q5id.guardian.Controls
{
    public class AppMap : Map
    {
        private const string V = "redNue";
        public static readonly BindableProperty AlertItemsSourceProperty = BindableProperty.Create(nameof(AlertItemsSource), typeof(IEnumerable), typeof(AppMap), (object)null, (BindingMode)2, (ValidateValueDelegate)null, (BindingPropertyChangedDelegate)(object)(BindingPropertyChangedDelegate)delegate (BindableObject b, object o, object n) {
            ((AppMap)(object)b).OnAlertItemsSourceChanged((IEnumerable)o, (IEnumerable)n);
        }, (BindingPropertyChangingDelegate)null, (CoerceValueDelegate)null, (CreateDefaultValueDelegate)null);


        public IEnumerable AlertItemsSource
        {
            get => (IEnumerable)GetValue(AlertItemsSourceProperty);
            set => SetValue(AlertItemsSourceProperty, value);
        }

        private void OnAlertItemsSourceChanged(IEnumerable oldItemsSource, IEnumerable newItemsSource)
        {
            var appMap = this;
            appMap.MapElements.Clear();
            Color pinColor = Color.Red;
            object pinColorObj;
            Application.Current.Resources.TryGetValue("redNue", out pinColorObj);
            if(pinColorObj is Color color)
            {
                pinColor = color;
            }
            if (newItemsSource != null && newItemsSource is Collection<Alert> listAlert)
            {
                foreach (var alert in listAlert)
                {
                    var position = alert.Position;
                    Circle insideCircle = new Circle
                    {
                        Center = new Position(position.Latitude, position.Longitude),
                        Radius = Distance.FromKilometers(1),
                        StrokeColor = pinColor,
                        StrokeWidth = 0,
                        FillColor = pinColor
                    };
                    Circle outsideCircle = new Circle
                    {
                        Center = new Position(position.Latitude, position.Longitude),
                        Radius = Distance.FromKilometers(5),
                        StrokeColor = pinColor,
                        StrokeWidth = 4,
                        FillColor = Color.Transparent
                    };
                    appMap.MapElements.Add(insideCircle);
                    appMap.MapElements.Add(outsideCircle);
                }
            }
        }

        public AppMap()
        {
        }
    }
}
