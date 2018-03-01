using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using TheShop.DAL.DTO;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace TheShop
{
    /// <summary>
    /// Class for calling API service over HttpClient
    /// </summary>
    public class ApiService
    {
        //uri for API methods
        public static string GetArticlesUri = "api/ArticleApi/GetArticles";
        public static string UpdateArticleUri = "api/ArticleApi/UpdateArticle";
        public static string GetArticleByIdUri = "api/ArticleApi/GetArticleById";
        public static string GetSuppliersUri = "api/SupplierApi/GetSuppliers";
        public static string GetBuyersUri = "api/BuyerApi/GetBuyers";
        public static string GetOrderSellUri = "api/OrderSellApi/GetOrderSell";

        private HttpClient client;

        //HttpClient header
        public HttpClient GetClient()
        {
            this.client = new HttpClient();
            client.BaseAddress = new Uri("http://localhost:55176/");
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            return client;
        }

        public static async Task<T> GetGenericById<T>(HttpClient client, string uri, int itemId, string type)
        {
            T t = default(T);

            using (client)
            {
                HttpResponseMessage res = await client.GetAsync(uri + $"?{type}={itemId}");
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    t = await res.Content.ReadAsAsync<T>();
                }
                return t;
            }
        }

        public static async Task<List<T>> GetGenericListAsync<T>(HttpClient client, string uri)
        {
            List<T> t = default(List<T>);

            using (client)
            {
                HttpResponseMessage res = await client.GetAsync(uri);
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    t = await res.Content.ReadAsAsync <List<T>>();
                }
                return t;
            }
        }

        public static async Task<SuccessOrderSellDTO> GetOrderSellAsync(HttpClient client, string uri, int articleId, int buyerId, int orderSellPrice, TransactionType transactionType)
        {
            SuccessOrderSellDTO success = new SuccessOrderSellDTO();

            using (client)
            {
                HttpResponseMessage res = await client.GetAsync(uri + $"?articleId={articleId}&buyerId={buyerId}&orderSellPrice={orderSellPrice}&transactionType={transactionType}");
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    success = await res.Content.ReadAsAsync<SuccessOrderSellDTO>();
                }
                return success;
            }
        }

        public static async Task<bool> UpdateArticle(HttpClient client, string uri, ArticleDTO article)
        {
            bool rez = false;

            using (client)
            {
                var content = JsonConvert.SerializeObject(article);
                var buffer = System.Text.Encoding.UTF8.GetBytes(content);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                HttpResponseMessage res = await client.PostAsync(uri, byteContent);
                res.EnsureSuccessStatusCode();
                if (res.IsSuccessStatusCode)
                {
                    rez = await res.Content.ReadAsAsync<bool>();
                }
                return rez;
            }
        }
    }
}
