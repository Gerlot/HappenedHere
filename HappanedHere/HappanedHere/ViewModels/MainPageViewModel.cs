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
            this.navigationService = navigationService;
        }

        // Tiles
        public void All()
        {
            MessageBox.Show("All");
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
        public void Settings()
        {
            MessageBox.Show("Settings");
        }

        public void Rate()
        {
            MessageBox.Show("Rate");
        }

        public void About()
        {
            MessageBox.Show("About");
        }

        /*public void GotoPageTwo()
        {
            navigationService.UriFor<MainPageViewModel>()
                .WithParam(x => x.NumberOfTabs, 5)
                .Navigate();
        }*/
    }
}
