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
    public partial class ReadArticlePage : PhoneApplicationPage
    {
        public ReadArticlePage()
        {
            InitializeComponent();
            if (ReadArticlePageViewModel.showSaved != null)
            {
                ArticleBrowser.NavigateToString(ReadArticlePageViewModel.showSaved);
            }
        }

        # region Properties

        ReadArticlePageViewModel viewModel
        {
            get
            {
                return this.DataContext as ReadArticlePageViewModel;
            }
        }

        # endregion

        # region Event Handlers

        private void PhoneApplicationPage_BackKeyPress(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (viewModel.browsingHistory.Count != 0)
            {
                viewModel.GoBack();
                e.Cancel = true;
            }            
        }

        private void ArticleBrowser_LoadCompleted(object sender, NavigationEventArgs e)
        {
            viewModel.IsProgressBarIndeterminate = false;
        }

        private void ArticleBrowser_Navigating(object sender, NavigatingEventArgs e)
        {
            viewModel.IsProgressBarIndeterminate = true;
        }

        # endregion

        private void Save_Click(object sender, EventArgs e)
        {
            viewModel.Save(ArticleBrowser.SaveToString());
        }

        
    }
}