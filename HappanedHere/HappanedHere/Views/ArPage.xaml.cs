using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Device.Location;
using System.Device;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Controls.Maps;
using Microsoft.Phone.Controls.Maps.PlatformServices;
using Microsoft.Phone.Shell;
using GART;
using GART.Controls;
using GART.BaseControls; 
using GART.Data;
using System.Windows.Media;
using HappanedHere.Data;
using HappanedHere.GeocodeService;
using HappanedHere.Helpers;
using Microsoft.Phone.Applications.Common;
using Microsoft.Devices.Sensors;
using HappanedHere.ViewModels;

namespace HappanedHere.Views
{
    public partial class ArPage : PhoneApplicationPage
    {
        public ArPage()
        {
            InitializeComponent();
        }

        # region Properties

        ArPageViewModel viewModel
        {
            get
            {
                return this.DataContext as ArPageViewModel;
            }
        }

        # endregion

        # region Event Handlers

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Stop AR and other services
            ARDisplay.StopServices();
            viewModel.StopServices();
            
            /*if (accelerometer != null)
            {
                accelerometer.Stop();
            }*/
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Start AR and other services
            ARDisplay.StartServices();
            viewModel.StartServices();

            base.OnNavigatedTo(e);
        }

        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            base.OnOrientationChanged(e);
            ARDisplay.HandleOrientationChange(e);            
        }

        private void Grid_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        {
            Grid grid = sender as Grid;
            ArticleItem article = grid.DataContext as ArticleItem;            
            viewModel.ReadArticle(article.Url, article.Title);
        }

        # endregion

    }
}