using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.Windows;
using HappanedHere.Resources;
using HappanedHere.Data;
using Microsoft.Phone.Net.NetworkInformation;
using Microsoft.Phone.Shell;

namespace HappanedHere.ViewModels
{

    public class MainPageViewModel : Screen
    {
        readonly INavigationService navigationService;
        private static readonly string EarlierTileUriSource = "Source=EarlierTile";
        private AppSettings settings;

        public MainPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            settings = new AppSettings();
            searchText = AppResources.Search;
            searchIcon = new Uri("/Assets/MainPage/AppBar/search.png", UriKind.Relative);
            settingsText = AppResources.Settings;
            rateText = AppResources.Rate;
        }

        # region Tiles
        public void All()
        {
            GoToArView();
        }

        public void News()
        {
            GoToArView();
        }

        public void History()
        {
            GoToArView();
        }

        public void Sports()
        {
            GoToArView();   
        }

        public void Earlier()
        {
            navigationService.UriFor<EarlierPageViewModel>().Navigate();
        }

        # endregion

        # region Methods

        public void PinToStart()
        {
            ShellTile tile = this.FindTile(EarlierTileUriSource);
            if (tile == null)
            {
                StandardTileData tileData = new StandardTileData
                {
                    Title = "Secondary Tile",
                    BackgroundImage = new Uri("/Assets/MainPage/earlier.png", UriKind.Relative),
                    Count = EarlierPageViewModel.savedArticleNumber,
                    BackTitle = "Secondary Tile",
                    BackBackgroundImage = new Uri("", UriKind.Relative),
                    BackContent = "WPG Add Remove Tile Sample"
                };

                string tileUri = string.Concat("/Views/EarlierPage.xaml?", EarlierTileUriSource);
                ShellTile.Create(new Uri(tileUri, UriKind.Relative), tileData);
            }
        }

        private void UpdateTile()
        {
            ShellTile tile = this.FindTile(EarlierTileUriSource);
            if (tile != null)
            {
                StandardTileData tileData = new StandardTileData
                {
                    Title = "Secondary Tile",
                    BackgroundImage = new Uri("/Assets/MainPage/earlier.png", UriKind.Relative),
                    Count = EarlierPageViewModel.savedArticleNumber,
                    BackTitle = "Secondary Tile",
                    BackBackgroundImage = new Uri("", UriKind.Relative),
                    BackContent = "WPG Add Remove Tile Sample"
                };
                tile.Update(tileData);
            }
        }

        private void GoToArView()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                if (settings.UseLocationSetting)
                {
                    navigationService.UriFor<ArPageViewModel>().Navigate();
                }
                else
                {
                    MessageBoxResult result = MessageBox.Show(AppResources.LetFindLocationDescription,
                        AppResources.LetFindLocationTitle, MessageBoxButton.OKCancel);
                    if (result == MessageBoxResult.OK)
                    {
                        settings.UseLocationSetting = true;
                        navigationService.UriFor<ArPageViewModel>().Navigate();
                    }
                }
            }
            else
            {
                MessageBox.Show("The Augmented Reallity View can't work without internet.", "No network connection", MessageBoxButton.OK);
            }
            
        }

        private ShellTile FindTile(string partOfUri)
        {
            ShellTile shellTile = ShellTile.ActiveTiles.FirstOrDefault(
                tile => tile.NavigationUri.ToString().Contains(partOfUri));

            return shellTile;
        }

        # endregion

        # region AppBar Properties
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

        # endregion

        # region AppBar Methods

        public void Search()
        {
            MessageBox.Show("Search");
        }

        public void Settings()
        {
            navigationService.UriFor<SettingsPageViewModel>()
                .Navigate();
        }

        public void Rate()
        {
            MessageBox.Show("Rate");
        }

        # endregion

        /*public void GotoPageTwo()
        {
            navigationService.UriFor<MainPageViewModel>()
                .WithParam(x => x.NumberOfTabs, 5)
                .Navigate();
        }*/
    }
}
