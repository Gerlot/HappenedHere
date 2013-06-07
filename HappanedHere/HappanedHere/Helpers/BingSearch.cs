using Bing;
using HappanedHere.ViewModels;
using HappanedHere.Views;
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
        private static int resultNumber = 20;

        private ArPageViewModel arPageViewModel;

        public BingSearch(ArPageViewModel arPageViewModel)
        {
            this.arPageViewModel = arPageViewModel;
        }

        public class ArticleSearchResult
        {
            public string title;
            public string displayUrl;
            public string url;

            public ArticleSearchResult(string title, string displayUrl, string url)
            {
                this.title = title;
                this.displayUrl = displayUrl;
                this.url = url;
            }
        }

        public void searchNewsByAddress(string address)
        {
            string rootUri = "https://api.datamarket.azure.com/Bing/Search";
            var bingContainer = new BingSearchContainer(new Uri(rootUri));

            string accountKey = App.Current.Resources["AzureAccountKey"] as string;
            bingContainer.UseDefaultCredentials = false;
            bingContainer.Credentials = new NetworkCredential(accountKey, accountKey);

            string searchTerm = address + " (site:hir24.hu OR site:origo.hu OR site:index.hu) -site:ingatlanapro.origo.hu -site:forum.index.hu -site:cimkezes.origo.hu -site:admin.index.hu";
            /*Deployment.Current.Dispatcher.BeginInvoke(() =>
            {
                MessageBox.Show(searchTerm);
            });*/

            var newsQuery = bingContainer.Web(searchTerm, null, "DisableQueryAlterations", null, null, null, null, null);
            newsQuery.AddQueryOption("$top", 20);
            var result = newsQuery.BeginExecute(_onNewsQueryComplete, newsQuery);
        }

        // Handle the query callback. 
        private void _onNewsQueryComplete(IAsyncResult newsResults)
        {
            var resultList = new List<ArticleSearchResult>();

            try
            {
                DataServiceQuery<Bing.WebResult> query = newsResults.AsyncState as DataServiceQuery<Bing.WebResult>;
                
                foreach (var result in query.EndExecute(newsResults).Take(resultNumber))
                {
                    DateTime now = DateTime.Now;
                    if (result.Url.Contains(now.Year.ToString()))
                    {
                        /*Deployment.Current.Dispatcher.BeginInvoke(() =>
                        {
                            MessageBox.Show(result.Url);                        
                        });*/
                        resultList.Add(new ArticleSearchResult(result.Title, result.DisplayUrl, result.Url));
                    }
                }
            }
            catch (DataServiceQueryException)
            {
            }

            if (resultList.Count != 0)
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    arPageViewModel.addArticlesToView(resultList);
                });
            }
            else
            {
                Deployment.Current.Dispatcher.BeginInvoke(() =>
                {
                    //MessageBox.Show("No result");
                    arPageViewModel.tryNextNearbyLocation();
                });
            }
        }
    }
}
