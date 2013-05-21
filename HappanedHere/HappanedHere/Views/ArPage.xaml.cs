using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using System.Device.Location;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GART;
using GART.Controls;
using GART.BaseControls; 
using GART.Data;
using System.Windows.Media;

namespace HappanedHere.Views
{
    public partial class ArPage : PhoneApplicationPage
    {
        private Random rand = new Random();

        public ArPage()
        {
            InitializeComponent();
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
                    current.Latitude + ((double)rand.Next(-60, 60)) / 100000,
                    current.Longitude + ((double)rand.Next(-60, 60)) / 100000);

                AddLabel(offset, "Location " + i);
            }
        }

        protected override void OnNavigatedFrom(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Stop AR services
            ARDisplay.StopServices();

            base.OnNavigatedFrom(e);
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            // Start AR services
            ARDisplay.StartServices();

            base.OnNavigatedTo(e);
        }

        private void ShowHeading_Click(object sender, EventArgs e)
        {
            UIHelper.ToggleVisibility(HeadingIndicator);
        }

        private void ShowMap_Click(object sender, EventArgs e)
        {
            UIHelper.ToggleVisibility(OverheadMap);
            bool mapOn = OverheadMap.Visibility == Visibility.Visible && HeadingIndicator.Visibility != Visibility.Visible;
            bool mapOff = OverheadMap.Visibility != Visibility.Visible && HeadingIndicator.Visibility == Visibility.Visible;
            // Toggle heading indicator and world view with map
            if (mapOn || mapOff)
            {
                UIHelper.ToggleVisibility(HeadingIndicator);
                UIHelper.ToggleVisibility(WorldView);
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
    }
}