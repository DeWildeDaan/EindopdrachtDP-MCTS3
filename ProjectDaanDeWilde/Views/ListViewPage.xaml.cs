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
using Xamarin.Forms.Xaml;

namespace ProjectDaanDeWilde.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ListViewPage : ContentPage
    {
        public List<ChargePoint> chargePoints = new List<ChargePoint>();
        public AllRefData allRefData = new AllRefData();
        public User currentUser = new User();

        public ListViewPage(List<ChargePoint> points, User user)
        {
            InitializeComponent();

            chargePoints = points;
            allRefData = RefDataRepository.RefData;
            currentUser = user;

            ShowChargePoints();

            btnBack.Clicked += BtnBack_Clicked;
            lvwChargePoints.ItemSelected += LvwChargePoints_ItemSelected;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void ShowChargePoints()
        {
            chargePoints.Sort();
            lvwChargePoints.ItemsSource = chargePoints;
        }

        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private void LvwChargePoints_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lvwChargePoints.SelectedItem != null)
            {
                Navigation.PushAsync(new DetailsPage(lvwChargePoints.SelectedItem as ChargePoint, currentUser));
                lvwChargePoints.SelectedItem = null;
            }

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