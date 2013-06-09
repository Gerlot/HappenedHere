using Caliburn.Micro;
using HappanedHere.Resources;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Text;
using System.Windows;

namespace HappanedHere.ViewModels
{
    public class EarlierPageViewModel : Screen
    {
        readonly INavigationService navigationService;
        private IsolatedStorageFile isolatedStorage;

        public static string directoryName = "SavedArticles";
        public static string newsDirectoryName = "SavedNewsArticles";
        public static string historyDirectoryName = "SavedHistoryArticles";
        public static string sportDirectoryName = "SavedSportArticles";
        public static int savedArticleNumber = 0;

        public EarlierPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            isolatedStorage = IsolatedStorageFile.GetUserStoreForApplication();

            // Populate all articles
            articles = new ObservableCollection<string>();
            if (isolatedStorage.DirectoryExists(directoryName))
            {
                string[] filenames = isolatedStorage.GetFileNames("./" + directoryName + "/*.*");
                foreach (string item in filenames)
                {
                    articles.Add(item.Remove(item.Length - 4));
                }
                if (articles.Count == 0)
                {
                    articles.Add(AppResources.NoSavedArticles);
                }
            }
            else
            {
                articles.Add(AppResources.NoSavedArticles);
            }

            // Populate news articles
            newsArticles = new ObservableCollection<string>();
            if (isolatedStorage.DirectoryExists(newsDirectoryName))
            {
                string[] filenames = isolatedStorage.GetFileNames("./" + directoryName + "/*.*");
                foreach (string item in filenames)
                {
                    newsArticles.Add(item);
                }
            }
            else
            {
                newsArticles.Add(AppResources.NoSavedArticles);
            }

            // Populate history articles
            historyArticles = new ObservableCollection<string>();
            if (isolatedStorage.DirectoryExists(historyDirectoryName))
            {
                string[] filenames = isolatedStorage.GetFileNames("./" + directoryName + "/*.*");
                foreach (string item in filenames)
                {
                    historyArticles.Add(item);
                }
            }
            else
            {
                historyArticles.Add(AppResources.NoSavedArticles);
            }

            // Populate sport articles
            sportArticles = new ObservableCollection<string>();
            if (isolatedStorage.DirectoryExists(sportDirectoryName))
            {
                string[] filenames = isolatedStorage.GetFileNames("./" + directoryName + "/*.*");
                foreach (string item in filenames)
                {
                    sportArticles.Add(item);
                }
            }
            else
            {
                sportArticles.Add(AppResources.NoSavedArticles);
            }
        }

        # region Properties

        private ObservableCollection<string> articles;

        public ObservableCollection<string> Articles
        {
            get { return articles; }
            set
            {
                articles = value;
                NotifyOfPropertyChange(() => Articles);
            }

        }

        private string selectedArticle;

        public string SelectedArticle
        {
            get { return selectedArticle; }
            set
            {
                selectedArticle = value;
                NotifyOfPropertyChange(() => SelectedArticle);
                OpenArticle(value);
            }
        }

        private ObservableCollection<string> newsArticles;

        public ObservableCollection<string> NewsArticles
        {
            get { return newsArticles; }
            set
            {
                newsArticles = value;
                NotifyOfPropertyChange(() => NewsArticles);
            }

        }

        private string selectedNewsArticle;

        public string SelectedNewsArticle
        {
            get { return selectedNewsArticle; }
            set
            {
                selectedNewsArticle = value;
                NotifyOfPropertyChange(() => SelectedNewsArticle);
                MessageBox.Show(value);
            }
        }

        private ObservableCollection<string> historyArticles;

        public ObservableCollection<string> HistoryArticles
        {
            get { return historyArticles; }
            set
            {
                historyArticles = value;
                NotifyOfPropertyChange(() => HistoryArticles);
            }

        }

        private string selectedHistoryArticle;

        public string SelectedHistoryArticle
        {
            get { return selectedHistoryArticle; }
            set
            {
                selectedHistoryArticle = value;
                NotifyOfPropertyChange(() => SelectedHistoryArticle);
                MessageBox.Show(value);
            }
        }

        private ObservableCollection<string> sportArticles;

        public ObservableCollection<string> SportArticles
        {
            get { return sportArticles; }
            set
            {
                sportArticles = value;
                NotifyOfPropertyChange(() => SportArticles);
            }

        }

        private string selectedSportArticle;

        public string SelectedSportArticle
        {
            get { return selectedSportArticle; }
            set
            {
                selectedSportArticle = value;
                NotifyOfPropertyChange(() => SelectedSportArticle);
                MessageBox.Show(value);
            }
        }

        # endregion

        # region Methods

        private void OpenArticle(string fileName)
        {
            string content;
            using (var fileStream = isolatedStorage.OpenFile(directoryName + "/" + fileName + ".txt", FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fileStream))
                {
                    content = reader.ReadToEnd();
                }
            }
            ReadArticlePageViewModel.showSaved = content;
            //navigationService.UriFor<ReadArticlePageViewModel>().Navigate();

            MessageBox.Show(content);
        }

        # endregion
    }
}
