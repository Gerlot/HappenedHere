using Caliburn.Micro;
using HappanedHere.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HappanedHere.ViewModels
{

    public class SettingsPageViewModel : Screen
    {
        readonly INavigationService navigationService;

        public SettingsPageViewModel(INavigationService navigationService)
        {
            useLocationHeaderText = AppResources.UseLocation;
            appNameText = AppResources.Title;
            this.navigationService = navigationService;
        }

        // Settings
        private string useLocationHeaderText;

        public string UseLocationHeaderText
        {
            get { return useLocationHeaderText; }
            set
            {
                useLocationHeaderText = value;
                NotifyOfPropertyChange(() => UseLocationHeaderText);
            }
        }

        // About
        private string appNameText;

        public string AppNameText
        {
            get { return appNameText; }
            set
            {
                appNameText = value;
                NotifyOfPropertyChange(() => AppNameText);
            }
        }
    }
}
