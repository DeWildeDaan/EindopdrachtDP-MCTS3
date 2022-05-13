using ProjectDaanDeWilde.Models;
using ProjectDaanDeWilde.Repositories;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Maps;
using Xamarin.Forms.Xaml;

namespace ProjectDaanDeWilde.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MapViewPage : ContentPage
    {

        public List<ChargePoint> chargePoints = new List<ChargePoint>();
        public AllRefData allRefData = new AllRefData();
        public User currentUser = new User();
        public Location currentLocation = new Location();


        public MapViewPage(List<ChargePoint> points, User user, Location location)
        {
            InitializeComponent();

            chargePoints = points;
            allRefData = RefDataRepository.RefData;
            currentUser = user;
            currentLocation = location;

            GenerateMap();
            AddPins();


            btnBack.Clicked += BtnBack_Clicked;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void AddPins()
        {
            Pin pinLocation = new Pin
            {
                Label = "Current location",
                Type = PinType.Place,
                Position = new Position(currentLocation.Latitude, currentLocation.Longitude)
            };
            map.Pins.Add(pinLocation);

            

            foreach (var point in chargePoints)
            {
                Pin pin = new Pin
                {
                    Label = point.AddressInfo.Name,
                    Address = $"{point.AddressInfo.AddressLine}, {point.AddressInfo.Town} {point.AddressInfo.PostCode}",
                    Type = PinType.SearchResult,
                    Position = new Position(point.AddressInfo.Lat, point.AddressInfo.Long)
                };
                pin.InfoWindowClicked += OnInfoWindowClickedAsync;
                map.Pins.Add(pin);
            }
        }

        private void GenerateMap()
        {
            map.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(currentLocation.Latitude, currentLocation.Longitude), Distance.FromKilometers(15)));
            
        }

        public void OnInfoWindowClickedAsync(object sender, PinClickedEventArgs e)
        {
            Navigation.PushAsync(new DetailsPage(chargePoints.FindAll(n => n.AddressInfo.Name == ((Pin)sender).Label)[0], currentUser));
        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
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