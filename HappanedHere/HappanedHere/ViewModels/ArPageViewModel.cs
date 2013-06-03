﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.Windows;
using GART.Controls;
using GART;
using HappanedHere.Views;
using HappanedHere.Resources;

namespace HappanedHere.ViewModels
{
    
    public class ArPageViewModel : Screen
    {
        readonly INavigationService navigationService;

        public ArPageViewModel(INavigationService navigationService)
        {
            isHeadingIndicatorVisible = false;
            isMapVisible = false;
            isWorldViewVisible = true;
            toggleHeadingText = AppResources.ToggleHeading;
            toggleHeadingIcon = new Uri("/Assets/ArPage/AppBar/heading.png", UriKind.Relative);
            showMapText = AppResources.ShowMap;
            showMapIcon = new Uri("/Assets/ArPage/AppBar/map.png", UriKind.Relative);
            addLocationsText = "Add example locations";
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

        private bool isMapVisible;

        public bool IsMapVisible
        {
            get { return isMapVisible; }
            set
            {
                isMapVisible = value;
                NotifyOfPropertyChange(() => IsMapVisible);
            }
        }

        private bool isWorldViewVisible;

        public bool IsWorldViewIndicatorVisible
        {
            get { return isWorldViewVisible; }
            set
            {
                isWorldViewVisible = value;
                NotifyOfPropertyChange(() => IsWorldViewIndicatorVisible);
            }
        }

        // AppBar
        private string toggleHeadingText;

        public string ToggleHeadingText
        {
            get { return toggleHeadingText; }
            set
            {
                toggleHeadingText = value;
                NotifyOfPropertyChange(() => ToggleHeadingText);
            }
        }

        private Uri toggleHeadingIcon;

        public Uri ToggleHeadingIcon
        {
            get { return toggleHeadingIcon; }
            set
            {
                toggleHeadingIcon = value;
                NotifyOfPropertyChange(() => ToggleHeadingIcon);
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

        private string addLocationsText;

        public string AddLocationsText
        {
            get { return addLocationsText; }
            set
            {
                addLocationsText = value;
                NotifyOfPropertyChange(() => AddLocationsText);
            }
        }
    }
}
