using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Plugin.InAppBilling;
using Flurl.Http;
using Newtonsoft.Json;

namespace q5id.guardian.Services
{
    public class InAppBillingService
    {
        private const string SUBSCRIPTION_PRODUCT_ID = "guardian_subscriber";
        // Sandbox -- use https://buy.itunes.apple.com/verifyReceipt for Prod
        //private const string VALIDATE_ENDPOINT = "https://sandbox.itunes.apple.com/verifyReceipt";
        private const string VALIDATE_ENDPOINT = "https://buy.itunes.apple.com/verifyReceipt";
        private const string SHARED_SECRET = "aeaa56dd11194cefb38cc3be2afe075a";


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
        public async Task<bool> IsExpiredReceipt(string purchaseToken)
        {
            var res = await VALIDATE_ENDPOINT.PostJsonAsync(new IAPVerifiedRequest()
            {
                ReceiptData = purchaseToken,
                Password = SHARED_SECRET,
                ExcludeOldTransactions = true
            }).ReceiveJson<IAPVerifiedResponse>();
            if(res.ExpirationIntent != null && (res.ExpirationIntent == "1" || res.ExpirationIntent == "2" ||
                res.ExpirationIntent == "3" || res.ExpirationIntent == "4" || res.ExpirationIntent == "5"))
            {
                return true;
            }
            else 
            {
                try
                {
                    var current = new DateTimeOffset(DateTime.UtcNow).ToUnixTimeMilliseconds();
                    return current > Convert.ToDouble(res.LastReceiptInfo.ExpiresDate);
                }
                catch
                {
                    return false;
                }
               
            }
            
        }
    }

    public class IAPVerifiedResponse
    {
        [JsonProperty("expiration_intent")]
        public string ExpirationIntent { get; set; }

        [JsonProperty("latest_expired_receipt_info")]
        public LastReceiptInfo LastReceiptInfo { get; set; }
    }

    public class LastReceiptInfo
    {
        [JsonProperty("expires_date")]
        public string ExpiresDate { get; set; }
    }
    public class IAPVerifiedRequest
    {
        [JsonProperty("receipt-data")]
        public string ReceiptData { get; set; }

        [JsonProperty("password")]
        public string Password { get; set; }

        [JsonProperty("exclude-old-transactions")]
        public bool ExcludeOldTransactions { get; set; }
    }
}
