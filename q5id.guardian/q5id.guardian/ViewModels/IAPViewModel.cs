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
    public class IAPViewModel : BaseViewModel<User, User>
    {
        public IAPViewModel(IMvxNavigationService navigationService, ILoggerFactory logProvider) : base(navigationService, logProvider)
        {
        }

        private bool mIsSuccessPurchase = false;
        public bool IsSuccessPurchase
        {
            get => mIsSuccessPurchase;
            set
            {
                mIsSuccessPurchase = value;
            }
        }

        private User mUser;
        public User User
        {
            get => mUser;
            set
            {
                mUser = value;
            }
        }

        public override void Prepare(User parameter)
        {
            User = parameter;
        }

        protected override async Task ClosePage()
        {
            await NavigationService.Close(this, User);
        }

        private List<object> mProducts;
        public List<object> Products
        {
            get => mProducts;
            set
            {
                mProducts = value;
            }
        }

        public override async Task Initialize()
        {
            await GetProducts();
        }

        public async Task GetProducts()
        {
            IsLoading = true;
            var products = await InAppBillingService.Instances.GetProducts();
            IsLoading = false;
            if (products != null)
            {
                var listProduct = new List<object>();
                foreach (InAppBillingProduct product in products)
                {
                    //item info here.
                    var item = new InAppBillingProductItemViewModel(product)
                    {
                        OnItemClicked = async () =>
                        {
                            await MakePurchase();
                        },
                        IsPaid = false
                    };
                    listProduct.Add(item);
                }
                Products = listProduct;
            }
        }

        public async Task<bool> MakePurchase()
        {
            IsLoading = true;
            var makePurchaseResponse = await InAppBillingService.Instances.MakePurchase();
            IsLoading = false;
            return makePurchaseResponse != null;
        }

        private async Task UpdateUserSubscription(string subscritionId)
        {
            IsLoading = true;
            var choices = Utils.Utils.GetChoices();
            Choice userRoleChoice = choices.Find((Choice obj) =>
            {
                return obj.Name == Utils.Constansts.USER_ROLE_SETTING_KEY;
            });
            ItemChoice subscriptionChoice = userRoleChoice.Items.Find(item =>
            {
                return item.Name == Utils.Constansts.USER_ROLE_SUBSCRIBER_KEY;
            });
            var userToPost = User;
            userToPost.SubscriptionExpiredDate = DateTime.UtcNow.AddDays(30).ToString();
            if(subscriptionChoice != null)
            {
                userToPost.RoleId = subscriptionChoice.Id;
            }
            ApiResponse<AppServiceResponse<User>> response;
            response = await AppApiManager.Instances.UpdateUser(userToPost);
            IsLoading = false;
            if (response.IsSuccess && response.ResponseObject != null)
            {
                User = response.ResponseObject.Result;
                IsSuccessPurchase = true;
            }

        }
    }
}

