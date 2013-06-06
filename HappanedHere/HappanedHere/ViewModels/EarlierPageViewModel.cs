using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappanedHere.ViewModels
{
    public class EarlierPageViewModel : Screen
    {
        readonly INavigationService navigationService;

        public EarlierPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
        }
    }
}
