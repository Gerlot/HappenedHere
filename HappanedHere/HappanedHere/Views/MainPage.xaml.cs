using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Shell;
using HappanedHere.ViewModels;

namespace HappanedHere.Views
{
    public partial class MainPage : PhoneApplicationPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        # region Properties

        MainPageViewModel viewModel
        {
            get
            {
                return this.DataContext as MainPageViewModel;
            }
        }

        # endregion

        # region Event Handlers

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            viewModel.PinToStart();
        }

        # endregion
    }
}