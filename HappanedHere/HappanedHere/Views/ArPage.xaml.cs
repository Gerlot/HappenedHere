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

namespace HappanedHere.Views
{
    public partial class ArPage : PhoneApplicationPage
    {
        private Random rand = new Random();
        private GeoCoordinate oldPosition;
        private GeoCoordinate currentPosition;
        private GeoCoordinateWatcher geowatcher;
        private int nearbynumber = 1;
        private double locationChangedTreshold = 100;

        private Accelerometer accelerometer;

        public ArPage()
        {
            InitializeComponent();
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

        private void geowatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (currentPosition != null)
            {
                oldPosition = currentPosition;
                currentPosition = e.Position.Location;
                if (oldPosition.GetDistanceTo(currentPosition) > locationChangedTreshold)
                {
                    //refreshArticles();
                }
            }
            else
            {
                currentPosition = e.Position.Location;
                //refreshArticles();
            }                        
        }

        private void refreshArticles()
        {
            for (int i = 0; i < nearbynumber; i++)
            {
                // Create a new location based on the users location plus
                // a random offset.
                GeoCoordinate nearbyPosition = new GeoCoordinate(

                    currentPosition.Latitude + ((double)rand.Next(-300, 300)) / 100000,
                    currentPosition.Longitude + ((double)rand.Next(-300, 300)) / 100000,
                    0);

                MessageBox.Show("request: " + nearbyPosition.Latitude + ", " + nearbyPosition.Longitude);

                // Reverse GeoCoding
                ReverseGeocodeRequest reverseGeocodeRequest = new ReverseGeocodeRequest();
                reverseGeocodeRequest.Credentials = new Credentials();
                string AppId = App.Current.Resources["BingApplicationId"] as string;
                reverseGeocodeRequest.Credentials.ApplicationId = AppId;
                reverseGeocodeRequest.Location = nearbyPosition;

                GeocodeServiceClient geocodeClient = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
                geocodeClient.ReverseGeocodeCompleted += geocodeClient_ReverseGeocodeCompleted;
                geocodeClient.ReverseGeocodeAsync(reverseGeocodeRequest);
            }
        }

        private void geocodeClient_ReverseGeocodeCompleted(object sender, ReverseGeocodeCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result.Results.First().Address.AddressLine != "")
                {
                    Deployment.Current.Dispatcher.BeginInvoke(() =>
                    {
                        foreach (var item in e.Result.Results.First().Locations)
                        {
                            MessageBox.Show("result: " + item.Latitude + ", " + item.Longitude);
                        }
                        
                    });

                    //BingSearch.searchNewsByAddress(e.Result.Results.First().Address.AddressLine);
                }
            }
            else
            {
                MessageBox.Show("Error getting adresses!");
            }
        }

        private void AddLabel(GeoCoordinate location, string label)
        {
            // We'll use the specified text for the content and we'll let 
            // the system automatically project the item into world space
            // for us based on the Geo location.
            ARItem item = new ARItem()
            {
                Content = label,
                GeoLocation = location,
            };

            ARDisplay.ARItems.Add(item);
        }

        private void AddNearbyLabels()
        {
            // Start with the current location
            GeoCoordinate current = ARDisplay.Location;

            // We'll add three Labels
            for (int i = 0; i < 3; i++)
            {
                // Create a new location based on the users location plus
                // a random offset.
                GeoCoordinate offset = new GeoCoordinate(
                    current.Latitude + ((double)rand.Next(-70, 70)) / 100000,
                    current.Longitude + ((double)rand.Next(-70, 70)) / 100000,
                    0);

                ArticleItem item = new ArticleItem()
                {
                    GeoLocation = offset,
                    Title = "Index.hu - A new article item from index",
                    Content = "Article " + i,
                    URL = "http://www.index.hu/something",
                    Icon = new Uri("/Assets/ArPage/news_icon.png", UriKind.Relative),
                };

                //AddLabel(offset, "Article " + i);
                ARDisplay.ARItems.Add(item);
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Stop AR services
            ARDisplay.StopServices();
            if (geowatcher != null)
            {
                geowatcher.Stop();
            }
            /*if (accelerometer != null)
            {
                accelerometer.Stop();
            }*/
            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Start AR services
            ARDisplay.StartServices();

            //accelerometer = new Accelerometer();
            //accelerometer.CurrentValueChanged += accelerometer_CurrentValueChanged;
            //accelerometer.Start();
            AccelerometerHelper.Instance.ReadingChanged += new EventHandler<AccelerometerHelperReadingEventArgs>(accelerometerHelper_ReadingChanged);
            DeviceOrientationHelper.Instance.OrientationChanged += new EventHandler<DeviceOrientationChangedEventArgs>(orientationHelper_OrientationChanged);

            base.OnNavigatedTo(e);
        }

        private void ToggleHeading_Click(object sender, EventArgs e)
        {
            //UIHelper.ToggleVisibility(HeadingIndicator);
            if (OverheadMap.RotationSource == RotationSource.North)
            {
                OverheadMap.RotationSource = RotationSource.AttitudeHeading;
                HeadingIndicator.RotationSource = RotationSource.North;
            }
            else
            {
                OverheadMap.RotationSource = RotationSource.North;
                HeadingIndicator.RotationSource = RotationSource.AttitudeHeading;
            }
        }

        private void ShowMap_Click(object sender, EventArgs e)
        {
            UIHelper.ToggleVisibility(OverheadMap);
            UIHelper.ToggleVisibility(WorldView);
            bool mapOn = OverheadMap.Visibility == Visibility.Visible && HeadingIndicator.Visibility != Visibility.Visible;
            bool mapOff = OverheadMap.Visibility != Visibility.Visible && HeadingIndicator.Visibility == Visibility.Visible;
            // Toggle heading indicator if neccesary with map
            if (mapOn || mapOff)
            {
                UIHelper.ToggleVisibility(HeadingIndicator);
            }
        }

        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            base.OnOrientationChanged(e);
            ARDisplay.HandleOrientationChange(e);            
        }

        private void AddLocations_Click(object sender, EventArgs e)
        {
            AddNearbyLabels();
        }

        private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        {
            geowatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            geowatcher.Start();
            geowatcher.PositionChanged += geowatcher_PositionChanged;
        }
    }
}