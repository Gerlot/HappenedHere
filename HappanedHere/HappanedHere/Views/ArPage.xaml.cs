using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using GART;

namespace HappanedHere.Views
{
    public partial class ArPage : PhoneApplicationPage
    {
        public ArPage()
        {
            InitializeComponent();
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
        }

        protected override void OnOrientationChanged(OrientationChangedEventArgs e)
        {
            //ARDisplay.h


            base.OnOrientationChanged(e);
        }

        /*private void PhoneApplicationPage_OrientationChanged(object sender, OrientationChangedEventArgs e)
        {
            //ARDisplay.handle
        }*/
    }
}