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
        public static readonly BindableProperty PositionItemsSourceProperty = BindableProperty.Create(nameof(PositionItemsSource), typeof(IEnumerable), typeof(AppMap), (object)null, (BindingMode)2, (ValidateValueDelegate)null, (BindingPropertyChangedDelegate)(object)(BindingPropertyChangedDelegate)delegate (BindableObject b, object o, object n) {
            ((AppMap)(object)b).OnPositionItemsSourceChanged((IEnumerable)o, (IEnumerable)n);
        }, (BindingPropertyChangingDelegate)null, (CoerceValueDelegate)null, (CreateDefaultValueDelegate)null);


        public IEnumerable PositionItemsSource
        {
            get => (IEnumerable)GetValue(PositionItemsSourceProperty);
            set => SetValue(PositionItemsSourceProperty, value);
        }

        private void OnPositionItemsSourceChanged(IEnumerable oldItemsSource, IEnumerable newItemsSource)
        {
            var appMap = this;
            appMap.MapElements.Clear();
            appMap.Pins.Clear();
            Color pinColor = Color.Red;
            object pinColorObj;
            Application.Current.Resources.TryGetValue("redNue", out pinColorObj);
            if(pinColorObj is Color color)
            {
                pinColor = color;
            }
            if (newItemsSource != null && newItemsSource is List<Position> listPosition)
            {
                foreach (var position in listPosition)
                {
                    Circle insideCircle = new Circle
                    {
                        Center = new Position(position.Latitude, position.Longitude),
                        Radius = Distance.FromKilometers(0.4),
                        StrokeColor = pinColor,
                        StrokeWidth = 0,
                        FillColor = pinColor
                    };
                    Circle outsideCircle = new Circle
                    {
                        Center = new Position(position.Latitude, position.Longitude),
                        Radius = Distance.FromKilometers(2.5),
                        StrokeColor = pinColor,
                        StrokeWidth = 4,
                        FillColor = Color.Transparent
                    };
                    appMap.MapElements.Add(insideCircle);
                    appMap.MapElements.Add(outsideCircle);
                    //Pin pin = new Pin()
                    //{
                    //    Label = alert.Title,
                    //    Address = alert.Address,
                    //    Position = new Position(position.Latitude - 0.0025, position.Longitude)
                    //    {
                            
                    //    },
                        
                    //};
                    //appMap.Pins.Add(pin);
                }
            }
        }

        public AppMap()
        {
        }
    }
}
