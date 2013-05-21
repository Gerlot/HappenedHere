using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.Windows;

namespace HappanedHere.ViewModels
{

    public class MainPageViewModel : Screen
    {
        readonly INavigationService navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            searchText = "Search";
            searchIcon = new Uri("/Assets/MainPage/AppBar/search.png", UriKind.Relative);
            settingsText = "Settings";
            rateText = "Rate+review";
            this.navigationService = navigationService;
        }

        // Tiles
        public void All()
        {
            navigationService.UriFor<ArPageViewModel>()
                .Navigate();
        }

        public void News()
        {
            MessageBox.Show("News");
        }

        public void History()
        {
            MessageBox.Show("History");
        }

        public void Sports()
        {
            MessageBox.Show("Sports");
        }

        public void Earlier()
        {
            MessageBox.Show("Earlier");
        }

        // AppBar
        private string searchText;

        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value;
                NotifyOfPropertyChange(() => SearchText);
            }
        }

        private Uri searchIcon;

        public Uri SearchIcon
        {
            get { return searchIcon; }
            set
            {
                searchIcon = value;
                NotifyOfPropertyChange(() => SearchIcon);
            }
        }

        public void Search()
        {
            MessageBox.Show("Search");
        }

        private string settingsText;

        public string SettingsText 
        {
            get { return settingsText; }
            set
            {
                settingsText = value;
                NotifyOfPropertyChange(() => SettingsText);
            }
        }

        public void Settings()
        {
            MessageBox.Show("Settings");
        }

        private string rateText;

        public string RateText
        {
            get { return rateText; }
            set
            {
                rateText = value;
                NotifyOfPropertyChange(() => RateText);
            }
        }

        public void Rate()
        {
            MessageBox.Show("Rate");
        }

        /*public void GotoPageTwo()
        {
            navigationService.UriFor<MainPageViewModel>()
                .WithParam(x => x.NumberOfTabs, 5)
                .Navigate();
        }*/
    }
}
