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
        public enum ArticleType { News, History, Sports };
        public static Stack<ArticleToSearch> articles;

        private Accelerometer accelerometer;

        public ArPage()
        {
            InitializeComponent();
        }

        ArPageViewModel viewModel
        {
            get
            {
                return this.DataContext as ArPageViewModel;
            }
        }

        public class ArticleToSearch
        {
            public GeocodeLocation location;
            public string address;

            public ArticleToSearch(GeocodeLocation l, string a)
            {
                location = l;
                address = a;
            }
        }

        private void orientationHelper_OrientationChanged(object sender, DeviceOrientationChangedEventArgs e)
        {
            if (e.CurrentOrientation.Equals(DeviceOrientation.ScreenSideUp))
            {
                MessageBox.Show("Flat");
            }
            //e.CurrentOrientation
            //DeviceOrientation.ScreenSideUp
        }

        private void accelerometerHelper_ReadingChanged(object sender, AccelerometerHelperReadingEventArgs e)
        {
            
        }      
                

        public void AddArticle(ArticleType type, GeoCoordinate location, string title, Uri url)
        {
            Uri icon = null;
            switch (type)
            {
                case ArticleType.News:
                    icon = new Uri("/Assets/ArPage/news_icon.png", UriKind.Relative);
                    break;
                case ArticleType.History:
                    icon = new Uri("/Assets/ArPage/history_icon.png", UriKind.Relative);
                    break;
                case ArticleType.Sports:
                    icon = new Uri("/Assets/ArPage/sports_icon.png", UriKind.Relative);
                    break;
                default:
                    break;
            }
            ArticleItem item = new ArticleItem()
            {
                GeoLocation = location,
                Title = title,
                Content = "Article",
                DisplayUrl = url.ToString(),
                Icon = icon,
            };

            ARDisplay.ARItems.Add(item);
        }

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

            //accelerometer = new Accelerometer();
            //accelerometer.CurrentValueChanged += accelerometer_CurrentValueChanged;
            //accelerometer.Start();
            AccelerometerHelper.Instance.ReadingChanged += new EventHandler<AccelerometerHelperReadingEventArgs>(accelerometerHelper_ReadingChanged);
            DeviceOrientationHelper.Instance.OrientationChanged += new EventHandler<DeviceOrientationChangedEventArgs>(orientationHelper_OrientationChanged);

            base.OnNavigatedTo(e);
        }

        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            base.OnOrientationChanged(e);
            ARDisplay.HandleOrientationChange(e);            
        }

    }
}