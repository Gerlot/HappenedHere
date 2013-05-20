using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.Windows;
using GART.Controls;
using GART;
using HappanedHere.Views;

namespace HappanedHere.ViewModels
{
    
    public class ArPageViewModel : Screen
    {
        readonly INavigationService navigationService;

        public ArPageViewModel(INavigationService navigationService)
        {
            isHeadingIndicatorVisible = false;
            showHeadingText = "Show Heading";
            showHeadingIcon = new Uri("/Assets/ArPage/AppBar/heading.png", UriKind.Relative);
            showMapText = "Show Map";
            showMapIcon = new Uri("/Assets/ArPage/AppBar/map.png", UriKind.Relative);
            this.navigationService = navigationService;
        }

        private bool isHeadingIndicatorVisible;

        public bool IsHeadingIndicatorVisible
        {
            get { return isHeadingIndicatorVisible; }
            set
            {
                isHeadingIndicatorVisible = value;
                NotifyOfPropertyChange(() => IsHeadingIndicatorVisible);
            }
        }

        // AppBar
        private string showHeadingText;

        public string ShowHeadingText
        {
            get { return showHeadingText; }
            set
            {
                showHeadingText = value;
                NotifyOfPropertyChange(() => ShowHeadingText);
            }
        }

        private Uri showHeadingIcon;

        public Uri ShowHeadingIcon
        {
            get { return showHeadingIcon; }
            set
            {
                showHeadingIcon = value;
                NotifyOfPropertyChange(() => ShowHeadingIcon);
            }
        }

        private string showMapText;

        public string ShowMapText
        {
            get { return showMapText; }
            set
            {
                showMapText = value;
                NotifyOfPropertyChange(() => ShowMapText);
            }
        }

        private Uri showMapIcon;

        public Uri ShowMapIcon
        {
            get { return showMapIcon; }
            set
            {
                showMapIcon = value;
                NotifyOfPropertyChange(() => ShowMapIcon);
            }
        }
    }
}
