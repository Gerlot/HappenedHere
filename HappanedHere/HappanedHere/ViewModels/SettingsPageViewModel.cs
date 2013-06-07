using Caliburn.Micro;
using HappanedHere.Resources;
using Microsoft.Phone.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;

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

        # region Settings Porperties

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

        # endregion

        # region About Properties

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

        # endregion

        # region About Methods

        public void DeveloperEmail()
        {
            DateTime now = DateTime.Now;
            EmailComposeTask emailcomposer = new EmailComposeTask();
            emailcomposer.To = "gbalazs6@gmail.com";
            emailcomposer.Subject = "HappenedHere Feedback " + now.ToLocalTime().ToString();
            emailcomposer.Show();
        }

        # endregion
    }
}
