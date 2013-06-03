using Bing;
using System;
using System.Collections.Generic;
using System.Data.Services.Client;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;

namespace HappanedHere.Helpers
{
    public class BingSearch
    {
        public static void searchNewsByAddress(string address)
        {
            string rootUri = "https://api.datamarket.azure.com/Bing/Search";
            var bingContainer = new BingSearchContainer(new Uri(rootUri));

            string accountKey = App.Current.Resources["AzureAccountKey"] as string;
            bingContainer.UseDefaultCredentials = false;
            bingContainer.Credentials = new NetworkCredential(accountKey, accountKey);

            DateTime now = DateTime.Now;
            string searchTerm = address + " " + now.Year.ToString() + " (site:origo.hu OR site:index.hu) -site:ingatlanapro.origo.hu -site:forum.index.hu";
            MessageBox.Show(searchTerm);

            var newsQuery = bingContainer.Web(searchTerm, null, "DisableQueryAlterations", null, null, null, null, null);
            newsQuery.BeginExecute(_onNewsQueryComplete, newsQuery);
        }

        // Handle the query callback. 
        private static void _onNewsQueryComplete(IAsyncResult newsResults)
        {
            DataServiceQuery<Bing.WebResult> query = newsResults.AsyncState as DataServiceQuery<Bing.WebResult>;

            var resultList = new List<string>();

            int i = 0;
            foreach (var result in query.EndExecute(newsResults))
            {
                if (i <= 6)
                {
                    resultList.Add(result.Title);
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        // MessageBox.Show(i + ": " + result.Title + ", " + result.Url);
                    });
                    i++;
                }
                else
                {
                    break;
                }
            }
        }

    }
}
