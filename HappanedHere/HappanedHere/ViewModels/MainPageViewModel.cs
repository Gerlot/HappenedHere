using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.Windows;

namespace HappanedHere.ViewModels
{

    public class MainPageViewModel
    {
        readonly INavigationService navigationService;

        public MainPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }

        /*public void GotoPageTwo()
        {
            navigationService.UriFor<MainPageViewModel>()
                .WithParam(x => x.NumberOfTabs, 5)
                .Navigate();
        }*/
    }
}
