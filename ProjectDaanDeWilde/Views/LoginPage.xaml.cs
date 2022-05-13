using ProjectDaanDeWilde.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProjectDaanDeWilde.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            
            entEmail.Text = "daan.de.wilde@student.howest.be";
            entUser.Text = "Daan";

            CheckConnectivity();

            btnLogin.Clicked += BtnLogin_Clicked;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }

        private void CheckConnectivity()
        {
            var current = Connectivity.NetworkAccess;

            if (current != NetworkAccess.Internet)
            {
                Navigation.PushAsync(new NoNetworkPage());
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

        private void BtnLogin_Clicked(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(entEmail.Text) && !string.IsNullOrEmpty(entUser.Text))
            {
                User currentUser = new User() {Name = entUser.Text, Email = entEmail.Text };
                Navigation.PushAsync(new MainPage(currentUser));
            }
        }
    }
}