using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Caliburn.Micro;
using System.Windows;
using GART.Controls;
using GART;
using HappanedHere.Views;
using HappanedHere.Resources;
using System.Collections.ObjectModel;
using GART.Data;
using System.Device.Location;
using Microsoft.Phone.Controls.Maps.Platform;
using HappanedHere.Data;
using HappanedHere.Helpers;
using HappanedHere.GeocodeService;
using Microsoft.Phone.Controls.Maps;
using System.Text.RegularExpressions;
using Microsoft.Phone.Shell;

namespace HappanedHere.ViewModels
{
    
    public class ArPageViewModel : Screen
    {
        readonly INavigationService navigationService;
        public static string ArticleUrl;
        public static string ArticleTitle;

        private Random rand = new Random();
        private GeoCoordinate oldPosition;
        private GeoCoordinate currentPosition;        
        private GeoCoordinateWatcher geowatcher;
        private int nearbynumber = 10;
        private double locationChangedTreshold = 100;

        private List<string> nearbyStreets;
        private Stack<GeoCoordinate> nearbyCoordinates;
        private GeoCoordinate currentNearby;
        private BingSearch searcher;

        public ArPageViewModel(INavigationService navigationService)
        {
            this.navigationService = navigationService;
            geowatcher = new GeoCoordinateWatcher(GeoPositionAccuracy.Default);
            nearbyCoordinates = new Stack<GeoCoordinate>();
            nearbyStreets = new List<string>();
            searcher = new BingSearch(this);

            isHeadingIndicatorVisible = false;
            isMapVisible = false;
            isWorldViewVisible = true;
            isProgressBarIndeterminate = true;
            overheadMapRotationSource = RotationSource.North;
            headingIndicatorRotationSource = RotationSource.AttitudeHeading;

            toggleHeadingText = AppResources.ToggleHeading;
            toggleHeadingIcon = new Uri("/Assets/ArPage/AppBar/heading.png", UriKind.Relative);
            showMapText = AppResources.ShowMap;
            showMapIcon = new Uri("/Assets/ArPage/AppBar/map.png", UriKind.Relative);
            refreshArticlesText = AppResources.RefreshArticles;
            articleItems = new ObservableCollection<ARItem>();            
        }

        # region Properties
        private ObservableCollection<ARItem> articleItems;

        public ObservableCollection<ARItem> ArticleItems
        {
            get { return articleItems; }
            set
            {
                articleItems = value;
                NotifyOfPropertyChange(() => ArticleItems);
            }
        }

        private RotationSource overheadMapRotationSource;

        public RotationSource OverheadMapRotationSource 
        {
            get { return overheadMapRotationSource; }
            set
            {
                overheadMapRotationSource = value;
                NotifyOfPropertyChange(() => OverheadMapRotationSource);
            } 
        }

        private RotationSource headingIndicatorRotationSource;

        public RotationSource HeadingIndicatorRotationSource
        {
            get { return headingIndicatorRotationSource; }
            set
            {
                headingIndicatorRotationSource = value;
                NotifyOfPropertyChange(() => HeadingIndicatorRotationSource);
            }
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

        public bool IsWorldViewVisible
        {
            get { return isWorldViewVisible; }
            set
            {
                isWorldViewVisible = value;
                NotifyOfPropertyChange(() => IsWorldViewVisible);
            }
        }

        private bool isProgressBarIndeterminate;

        public bool IsProgressBarIndeterminate
        {
            get { return isProgressBarIndeterminate; }
            set
            {
                isProgressBarIndeterminate = value;
                NotifyOfPropertyChange(() => IsProgressBarIndeterminate);
            }
        }

        # endregion

        # region Methods

        public void StartServices()
        {
            geowatcher.Start();
            geowatcher.PositionChanged += geowatcher_PositionChanged;
        }

        private void geowatcher_PositionChanged(object sender, GeoPositionChangedEventArgs<GeoCoordinate> e)
        {
            if (currentPosition != null)
            {
                oldPosition = currentPosition;
                currentPosition = e.Position.Location;
                if (oldPosition.GetDistanceTo(currentPosition) > locationChangedTreshold)
                {
                    //RefreshArticles();
                }
            }
            else
            {
                currentPosition = e.Position.Location;                
                RefreshArticles();
            }
        }

        public void StopServices()
        {
            if (geowatcher != null)
            {
                geowatcher.Stop();
            }
        }

        public void ReadArticle(string url, string title)
        {
            //MessageBox.Show(url);
            if (url != null && url != "")
            {
                ArticleUrl = url;
                ArticleTitle = title;
                navigationService.UriFor<ReadArticlePageViewModel>().Navigate();
            }            
        }

        public void addArticlesToView(List<HappanedHere.Helpers.BingSearch.ArticleSearchResult> resultList)
        {
            foreach (var item in resultList)
            {
                bool exists = false;
                foreach (ArticleItem existing in ArticleItems)
                {
                    if (existing.Title.Equals(item.title))
                    {
                        exists = true;
                        break;
                    }
                }

                if (!exists)
                {
                    GeoCoordinate articlePosition = new GeoCoordinate(

                    currentNearby.Latitude + ((double)rand.Next(-40, 40)) / 100000,
                    currentNearby.Longitude + ((double)rand.Next(-40, 40)) / 100000,
                    currentNearby.Altitude + (double)rand.Next(-80, 80));
                    ArticleItem articleItem = new ArticleItem()
                    {
                        GeoLocation = articlePosition,
                        Title = item.title,
                        Content = item.title,
                        DisplayUrl = item.displayUrl,
                        Url = item.url,
                        Icon = new Uri("/Assets/ArPage/news_icon.png", UriKind.Relative),
                    };

                    ArticleItems.Add(articleItem);
                }                
            }

            tryNextNearbyLocation();
        }

        public void tryNextNearbyLocation()
        {
            if (nearbyCoordinates.Count != 0)
            {
                GeoCoordinate c = nearbyCoordinates.Pop();
                //MessageBox.Show("next request: " + c.Latitude + ", " + c.Longitude);
                reverseGeocode(c);
            }
            else
            {
                IsProgressBarIndeterminate = false;
            }
        }

        private void reverseGeocode(GeoCoordinate location)
        {
            ReverseGeocodeRequest reverseGeocodeRequest = new ReverseGeocodeRequest();
            reverseGeocodeRequest.Credentials = new Credentials();
            string AppId = App.Current.Resources["BingApplicationId"] as string;
            reverseGeocodeRequest.Credentials.ApplicationId = AppId;
            reverseGeocodeRequest.Location = location;

            GeocodeServiceClient geocodeClient = new GeocodeServiceClient("BasicHttpBinding_IGeocodeService");
            geocodeClient.ReverseGeocodeCompleted += geocodeClient_ReverseGeocodeCompleted;
            geocodeClient.ReverseGeocodeAsync(reverseGeocodeRequest);
        }

        private void geocodeClient_ReverseGeocodeCompleted(object sender, ReverseGeocodeCompletedEventArgs e)
        {
            if (e.Error == null)
            {
                if (e.Result.Results != null && e.Result.Results.Count != 0)
                {
                    int i = 0;
                    foreach (var item in e.Result.Results)
                    {
                        if (item.Address.AddressLine != "")
                        {
                            i++;
                            double latitude = e.Result.Results.First().Locations.First().Latitude;
                            double longitude = e.Result.Results.First().Locations.First().Longitude;
                            currentNearby = new GeoCoordinate(latitude, longitude);

                            // Remove (house) numbers from address
                            string address = item.Address.AddressLine;
                            address = Regex.Replace(address, @"[\d-]", string.Empty).Trim();

                            // Check if address already searched
                            if (!nearbyStreets.Contains(address))
                            {
                                nearbyStreets.Add(address);
                                searcher.searchNewsByAddress("\"" + address + "\"");
                                break;
                            }
                            else
                            {
                                tryNextNearbyLocation();
                            }
                        }
                    }
                    if (i == 0)
                    {
                        tryNextNearbyLocation();
                    }
                }
                else
                {
                    tryNextNearbyLocation();
                }
            }
            else
            {
                currentNearby = null;
                tryNextNearbyLocation();
            }
        }       

        # endregion

        # region AppBar Properties
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

        private string refreshArticlesText;

        public string RefreshArticlesText
        {
            get { return refreshArticlesText; }
            set
            {
                refreshArticlesText = value;
                NotifyOfPropertyChange(() => RefreshArticlesText);
            }
        }
        # endregion

        # region AppBar Methods

        public void ToggleHeading()
        {
            if (OverheadMapRotationSource == RotationSource.North)
            {
                OverheadMapRotationSource = RotationSource.AttitudeHeading;
                HeadingIndicatorRotationSource = RotationSource.North;
            }
            else
            {
                OverheadMapRotationSource = RotationSource.North;
                HeadingIndicatorRotationSource = RotationSource.AttitudeHeading;
            }
        }

        public void ShowMap()
        {
            if (IsMapVisible)
            {
                IsMapVisible = false;
                IsHeadingIndicatorVisible = false;
                IsWorldViewVisible = true;
            }
            else
            {
                IsMapVisible = true;
                IsHeadingIndicatorVisible = true;
                IsWorldViewVisible = false;
            }
        }

        public void RefreshArticles()
        {
            // Clear ArticleItems and stored nearby locations
            ArticleItems.Clear();
            nearbyCoordinates.Clear();
            nearbyStreets.Clear();

            // Show Progress Bar
            IsProgressBarIndeterminate = true;

            // Pick random nearby locations
            for (int i = 0; i < nearbynumber; i++)
            {
                GeoCoordinate nearbyPosition = new GeoCoordinate(

                    currentPosition.Latitude + ((double)rand.Next(-250, 250)) / 100000,
                    currentPosition.Longitude + ((double)rand.Next(-250, 250)) / 100000,
                    0);

                nearbyCoordinates.Push(nearbyPosition);
            }
            GeoCoordinate c = nearbyCoordinates.Pop();
            //MessageBox.Show("refresh request: " + c.Latitude + ", " + c.Longitude);
            reverseGeocode(c);
        }

        /*public void AddLocations()
        {
            if (currentPosition != null)
            {
                // We'll add three Labels
                for (int i = 0; i < 3; i++)
                {
                    // Create a new location based on the users location plus
                    // a random offset.
                    GeoCoordinate offset = new GeoCoordinate(
                        currentPosition.Latitude + ((double)rand.Next(-70, 70)) / 100000,
                        currentPosition.Longitude + ((double)rand.Next(-70, 70)) / 100000,
                        0);

                    ArticleItem articleItem = new ArticleItem()
                    {
                        GeoLocation = offset,
                        Title = "Index.hu - A new article item from index",
                        Content = "Article " + i,
                        DisplayUrl = "http://www.index.hu/something",
                        Icon = new Uri("/Assets/ArPage/news_icon.png", UriKind.Relative),
                    };

                    MessageBox.Show(articleItem.DisplayUrl);
                    
                    ArticleItems.Add(articleItem);
                }
            }   
        }*/

        # endregion
    }
}
