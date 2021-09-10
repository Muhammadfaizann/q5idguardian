using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using Plugin.InAppBilling;
using q5id.guardian.ViewModels.ItemViewModels;

namespace q5id.guardian.ViewModels
{
    public class IAPViewModel : BaseViewModel
    {
        public IAPViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
            
        }

        private List<object> mProducts;
        public List<object> Products
        {
            get => mProducts;
            set
            {
                mProducts = value;
                RaisePropertyChanged(nameof(Products));
            }
        }

        public override async Task Initialize()
        {
            await GetProducts();
        }

        public async Task GetProducts()
        {
            if (!CrossInAppBilling.IsSupported)
                return;

            var billing = CrossInAppBilling.Current;
            try
            {
                IsLoading = true;
                var productIds = new string[] { "guardian_iap_test", "guardian_iap_test_one" };

                var connected = await CrossInAppBilling.Current.ConnectAsync();

                if (!connected)
                {
                    //Couldn't connect to billing, could be offline, alert user
                    return;
                }

                //try to purchase item

                var products = await CrossInAppBilling.Current.GetProductInfoAsync(ItemType.InAppPurchase, productIds);
                if (products == null)
                {
                    //Not purchased, alert the user

                }
                else
                {
                    //check purchases
                    var purchases = await billing.GetPurchasesAsync(ItemType.InAppPurchase);
                    var listProduct = new List<object>();

                    //Purchased, save this information
                    foreach (InAppBillingProduct product in products)
                    {
                        //item info here.
                        var isPaid = purchases?.Any(p => p.ProductId == product.ProductId) ?? false;
                        var item = new InAppBillingProductItemViewModel(product)
                        {
                            ItemClickCommand = new Xamarin.Forms.Command(async () =>
                            {
                                if(isPaid == false)
                                {
                                    await MakePurchase(product.ProductId);
                                }
                                
                            }),
                            IsPaid = isPaid
                        };
                        listProduct.Add(item);
                    }
                    Products = listProduct;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                await billing.DisconnectAsync();
                IsLoading = false;
            }
        }

        public async Task<bool> MakePurchase(string productId)
        {
            if (!CrossInAppBilling.IsSupported)
                return false;

            var billing = CrossInAppBilling.Current;
            try
            {
                IsLoading = true;
                var connected = await CrossInAppBilling.Current.ConnectAsync();

                if (!connected)
                {
                    //Couldn't connect to billing, could be offline, alert user
                    return false;
                }

                //try to purchase item

                //try to purchase item
                var purchase = await CrossInAppBilling.Current.PurchaseAsync(productId, ItemType.InAppPurchase);
                if (purchase == null)
                {
                    //Not purchased, alert the user
                }
                else
                {
                    //Purchased, save this information
                    var id = purchase.Id;
                    var token = purchase.PurchaseToken;
                    var state = purchase.State;
                    await App.Current.MainPage.DisplayAlert("Information", "Purchase successfully", "OK");
                }
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error: " + ex.Message);
                return false;
            }
            finally
            {
                await billing.DisconnectAsync();
                IsLoading = false;
            }
        }
    }
}

