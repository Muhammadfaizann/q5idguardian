using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Plugin.InAppBilling;

namespace q5id.guardian.Services
{
    public class InAppBillingService
    {
        private const string SUBSCRIPTION_PRODUCT_ID = "one_month_subscription_role_guardian_app";

        private static InAppBillingService mInstances = null;

        public static InAppBillingService Instances
        {
            get
            {
                if (mInstances == null)
                {
                    mInstances = new InAppBillingService();
                }
                return mInstances;
            }
        }

        public async Task<IEnumerable<InAppBillingProduct>> GetProducts()
        {
            if (!CrossInAppBilling.IsSupported)
                return null;
            IEnumerable<InAppBillingProduct> result = null;
            try
            {
                var productIds = new string[]
                {
                    SUBSCRIPTION_PRODUCT_ID,
                };

                var connected = await CrossInAppBilling.Current.ConnectAsync();

                if (connected)
                {
                    //Couldn't connect to billing, could be offline, alert user
                    result = await CrossInAppBilling.Current.GetProductInfoAsync(ItemType.Subscription, productIds);
                }
                //try to purchase item
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error GetProducts: " + ex.Message);
            }
            finally
            {
                await CrossInAppBilling.Current.DisconnectAsync();
            }
            return result;
        }

        public async Task<IEnumerable<InAppBillingPurchase>> GetProductPurchases()
        {
            if (!CrossInAppBilling.IsSupported)
                return null;
            IEnumerable<InAppBillingPurchase> result = null;
            try
            {
                
                var connected = await CrossInAppBilling.Current.ConnectAsync();

                if (connected)
                {
                    //Couldn't connect to billing, could be offline, alert user
                    var purchases = await CrossInAppBilling.Current.GetPurchasesAsync(ItemType.Subscription);
                    if(purchases != null)
                    {
                        result = purchases.Where(item =>
                        {
                            return item.ProductId == SUBSCRIPTION_PRODUCT_ID;
                        }).OrderByDescending(p=>p.TransactionDateUtc);
                    }
                    
                }
                //try to purchase item
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error GetProductPurchases: " + ex.Message);
            }
            finally
            {
                await CrossInAppBilling.Current.DisconnectAsync();
            }
            return result;
        }

        public async Task<InAppBillingPurchase> MakePurchase()
        {
            if (!CrossInAppBilling.IsSupported)
                return null;

            var billing = CrossInAppBilling.Current;
            try
            {
                var connected = await CrossInAppBilling.Current.ConnectAsync();

                if (!connected)
                {
                    //Couldn't connect to billing, could be offline, alert user
                    return null;
                }
                //try to purchase item
                var purchase = await CrossInAppBilling.Current.PurchaseAsync(SUBSCRIPTION_PRODUCT_ID, ItemType.InAppPurchase);
                await CrossInAppBilling.Current.FinishTransaction(purchase);
                return purchase;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error MakePurchase: " + ex.Message);
                return null;
            }
            finally
            {
                await billing.DisconnectAsync();
            }
        }
    }
}
