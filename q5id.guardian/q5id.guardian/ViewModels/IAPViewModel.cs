using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using MvvmCross.Navigation;
using Plugin.InAppBilling;
using q5id.guardian.Models;
using q5id.guardian.Services;
using q5id.guardian.Services.Bases;
using q5id.guardian.ViewModels.ItemViewModels;

namespace q5id.guardian.ViewModels
{
    public class IAPViewModel : BaseViewModel<User>
    {
        public IAPViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
            GetContactEntity();
        }

        private User mUser;
        public User User
        {
            get => mUser;
            set
            {
                mUser = value;
                RaisePropertyChanged(nameof(User));
            }
        }

        public override void Prepare(User parameter)
        {
            User = parameter;
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
                var productIds = new string[]
                {
                    "subscription_one_month",
                };

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
                    //var purchases = await billing.GetPurchasesAsync(ItemType.InAppPurchase);
                    var listProduct = new List<object>();

                    //Purchased, save this information
                    foreach (InAppBillingProduct product in products)
                    {
                        //item info here.
                        //var isPaid = purchases?.Any(p => p.ProductId == product.ProductId) ?? false;
                        var item = new InAppBillingProductItemViewModel(product)
                        {
                            OnItemClicked = async () =>
                            {
                                await MakePurchase(product.ProductId);

                            },
                            IsPaid = false
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
                    await UpdateUserSubscription(id);
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

        private StructureEntity ContactEntity = null;
        private void GetContactEntity()
        {
            var settings = Utils.Utils.GetSettings();
            if (settings != null)
            {
                ContactEntity = Utils.Utils.GetSettings().Find((StructureEntity entity) =>
                {
                    return entity.EntityName == Utils.Constansts.CONTACT_ENTITY_SETTING_KEY;
                });
            }
        }

        private async Task UpdateUserSubscription(string subscritionId)
        {
            if (ContactEntity == null)
            {
                return;
            }
            IsLoading = true;

            var userToPost = User;
            userToPost.SubscriptionId = subscritionId;
            userToPost.SubscriptionExpiredDate = DateTime.UtcNow.AddDays(30).ToString();
            ApiResponse<EntityResponse<User>> response;
            response = await AppService.Instances.UpdateUser(ContactEntity.Id, userToPost);
            IsLoading = false;
            if (response.IsSuccess && response.ResponseObject != null)
            {
                await App.Current.MainPage.DisplayAlert("Updated Successfully", "", "OK");
                if (User != null)
                {
                    await NavigationService.Close(this);
                }
            }

        }
    }
}

