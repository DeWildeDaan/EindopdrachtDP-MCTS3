using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using System.Diagnostics;
using System.Threading;
using ProjectDaanDeWilde.Views;
using ProjectDaanDeWilde.Models;
using ProjectDaanDeWilde.Repositories;
using Xamarin.Essentials;

namespace ProjectDaanDeWilde
{
    public partial class MainPage : ContentPage
    {
        public List<ChargePoint> chargePoints = new List<ChargePoint>();
        public User currentUser = new User();
        public Location location = new Location();
        CancellationTokenSource cts;

        public MainPage(User user)
        {
            InitializeComponent();
            
            currentUser = user;
            btnList.Opacity = 0.5;
            btnMap.Opacity = 0.5;

            GetLocationAsync();

            btnMap.Clicked += BtnMap_Clicked;
            btnList.Clicked += BtnList_Clicked;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;

        }

        private async Task GetLocationAsync()
        {
            try
            {
                var request = new GeolocationRequest(GeolocationAccuracy.High, TimeSpan.FromSeconds(10));
                cts = new CancellationTokenSource();
                location = await Geolocation.GetLocationAsync(request, cts.Token);
                if (location != null)
                {
                    GetDataAsync();
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

        private async Task GetDataAsync()
        {
            chargePoints = await ChargerRepository.GetChargePointsAsync(location.Latitude, location.Longitude, 10);
            RefDataRepository.GetRefDataFile();
            //await RefDataRepository.GetRefDataAsync();
            btnMap.Opacity = 1;
            btnList.Opacity = 1;
            
        }

        private async void BtnMap_Clicked(object sender, EventArgs e)
        {
            if (chargePoints.Count > 0)
            {
                Navigation.PushAsync(new MapViewPage(chargePoints, currentUser, location));
            }
            else { await DisplayAlert("Alert", "Data is still being loaded.", "OK"); }

        }

        private async void BtnList_Clicked(object sender, EventArgs e)
        {
            if (chargePoints.Count > 0)
            {
                Navigation.PushAsync(new ListViewPage(chargePoints, currentUser));
            }
            else { await DisplayAlert("Alert", "Data is still being loaded.", "OK"); }
            
        }

        private void Connectivity_ConnectivityChanged(object sender, ConnectivityChangedEventArgs e)
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                Navigation.PushAsync(new NoNetworkPage());
            }
        }
    }

}
