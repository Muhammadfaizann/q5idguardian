using System;
using System.Collections;
using Rg.Plugins.Popup.Extensions;
using System.Diagnostics;
using Rg.Plugins.Popup.Pages;
using Xamarin.Forms;

namespace q5id.guardian.Controls
{
    public partial class AppPickerListPage : PopupPage
    {
        private AppPopupPickerView mPickerView;

        public AppPickerListPage(AppPopupPickerView mPickerView)
        {
            InitializeComponent();
            this.mPickerView = mPickerView;
        }

        public void UpdateList(IEnumerable dataSource, DataTemplate dataTemplate)
        {
            //LvOptions.ItemsSource = dataSource;
            //LvOptions.ItemTemplate = dataTemplate;
            StackContent.Children.Clear();
            if(dataSource != null && dataTemplate != null)
            {
                GestureRecognizer gestureItem = new GestureRecognizer();
                int index = 0;
                foreach (object data in dataSource)
                {
                    View itemContent = dataTemplate.CreateContent() as View;
                    itemContent.BindingContext = data;
                    TapGestureRecognizer tapItemGestureRecognizer = new TapGestureRecognizer();
                    int currentIndex = index;
                    tapItemGestureRecognizer.Tapped += delegate (object sender, EventArgs e) {
                        OnItemTapped(data);
                    };
                    itemContent.GestureRecognizers.Add(tapItemGestureRecognizer);
                    StackContent.Children.Add(itemContent);
                    index++;
                }
            }
        }

        private async void OnItemTapped(object dataSource)
        {
            await Navigation.PopPopupAsync();
            mPickerView.SetSelectedObject(dataSource);
        }

        async void OnCloseClicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PopPopupAsync();
        }
    }
}
