using Caliburn.Micro;
using HappanedHere.Resources;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows;

namespace HappanedHere.ViewModels
{
    public class ReadArticlePageViewModel : Screen
    {
        readonly INavigationService navigationService;
        public static string mobilizerServiceUrl = "http://www.instapaper.com/m?u=";
        public static string showSaved = null;

        public string initialUrl;
        public Stack<string> browsingHistory;

        private IsolatedStorageFile isolatedStorage;


        public ReadArticlePageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

            string urlToShow = HttpUtility.UrlEncode(ArPageViewModel.ArticleUrl);
            initialUrl = mobilizerServiceUrl + urlToShow;
            browsingHistory = new Stack<string>();
            browsingHistory.Push(initialUrl);
            articleBrowserUrl = initialUrl;
            IsProgressBarIndeterminate = true;

            viewInExplorerText = AppResources.ViewInExplorer;
            viewInExplorerIcon = new Uri("/Assets/ReadArticlePage/AppBar/explorer.png", UriKind.Relative);
            sendEmailText = AppResources.SendEmail;
            sendEmailIcon = new Uri("/Assets/ReadArticlePage/AppBar/email.png", UriKind.Relative);
            saveText = AppResources.Save;
            saveIcon = new Uri("/Assets/ReadArticlePage/AppBar/save.png", UriKind.Relative);
        }

        # region Properties

        private string articleBrowserUrl;

        public string ArticleBrowserUrl
        {
            get { return articleBrowserUrl; }
            set
            {
                articleBrowserUrl = value;
                NotifyOfPropertyChange(() => ArticleBrowserUrl);
            }
        }

        private bool isProgressBarIndeterminate;

        public bool IsProgressBarIndeterminate
        {
            get { return isProgressBarIndeterminate; }
            set
            {
                isProgressBarIndeterminate = value;
                NotifyOfPropertyChange(() => IsProgressBarIndeterminate);
            }
        }

        # endregion

        # region Methods

        public void GoBack()
        {
            ArticleBrowserUrl = browsingHistory.Pop();
        }

        # endregion

        # region AppBar Properties

        private string viewInExplorerText;

        public string ViewInExplorerText
        {
            get { return viewInExplorerText; }
            set
            {
                viewInExplorerText = value;
                NotifyOfPropertyChange(() => ViewInExplorerText);
            }
        }

        private Uri viewInExplorerIcon;

        public Uri ViewInExplorerIcon
        {
            get { return viewInExplorerIcon; }
            set
            {
                viewInExplorerIcon = value;
                NotifyOfPropertyChange(() => ViewInExplorerIcon);
            }
        }

        private string sendEmailText;

        public string SendEmailText
        {
            get { return sendEmailText; }
            set
            {
                sendEmailText = value;
                NotifyOfPropertyChange(() => SendEmailText);
            }
        }

        private Uri sendEmailIcon;

        public Uri SendEmailIcon
        {
            get { return sendEmailIcon; }
            set
            {
                sendEmailIcon = value;
                NotifyOfPropertyChange(() => SendEmailIcon);
            }
        }

        private string saveText;

        public string SaveText
        {
            get { return saveText; }
            set
            {
                saveText = value;
                NotifyOfPropertyChange(() => SaveText);
            }
        }

        private Uri saveIcon;

        public Uri SaveIcon
        {
            get { return saveIcon; }
            set
            {
                saveIcon = value;
                NotifyOfPropertyChange(() => SaveIcon);
            }
        }

        # endregion

        # region AppBar Methods

        public void ViewInExplorer()
        {
            WebBrowserTask wbt = new WebBrowserTask();
            wbt.URL = ArticleBrowserUrl;
            wbt.Show();
        }

        public void SendEmail()
        {
            EmailComposeTask emailcomposer = new EmailComposeTask();
            emailcomposer.Subject = AppResources.ShareViaEmailSubject;
            emailcomposer.Body = AppResources.ShareViaEmailBody + ArPageViewModel.ArticleUrl;
            emailcomposer.Show();
        }

        public void Save(string htmlContent)
        {
            if (!isolatedStorage.DirectoryExists(EarlierPageViewModel.directoryName))
            {
                isolatedStorage.CreateDirectory(EarlierPageViewModel.directoryName);
            }

            try
            {
                using (var stream = new IsolatedStorageFileStream(EarlierPageViewModel.directoryName + "/" + ArPageViewModel.ArticleTitle + ".txt", FileMode.CreateNew, isolatedStorage))
                {
                    using (StreamWriter writer = new StreamWriter(stream))
                    {
                        writer.WriteLine(htmlContent);
                        writer.Close();
                    }

                }
            }
            catch (Exception)
            {
            }

        }

        # endregion
    }
}
