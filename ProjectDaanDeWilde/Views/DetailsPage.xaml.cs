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
    public partial class DetailsPage : ContentPage
    {
        public User currentUser = new User();
        public int chargerId;
        public DetailsPage(ChargePoint chargePoint, User user)
        {
            InitializeComponent();

            currentUser = user;
            chargerId = chargePoint.Id;

            GetComments();
            ShowData(chargePoint);

            btnPost.Clicked += BtnPost_Clicked;
            btnBack.Clicked += BtnBack_Clicked;
            lvwComments.ItemSelected += LvwComments_ItemSelected;
            Connectivity.ConnectivityChanged += Connectivity_ConnectivityChanged;
        }
        private async Task GetComments()
        {
            List<Comment> comments = await CommentRepository.GetCommentsAsync(chargerId);
            comments.Sort();
            comments.Reverse();
            lvwComments.ItemsSource = comments;
        }

        private void ShowData(ChargePoint chargePoint)
        {
            lblTitle.Text = chargePoint.AddressInfo.Name;
            lblDistance.Text = $"{Math.Round(chargePoint.AddressInfo.Distance, 2)} km";
            lblChargers.Text = $"{chargePoint.ConnectionCount} charger(s)";
            lblStatus.Text = chargePoint.StatusType;
            lblUsage.Text = chargePoint.UsageType;
            lvwConnections.ItemsSource = chargePoint.Connections;
        }
        private void BtnBack_Clicked(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        private async void BtnPost_Clicked(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(entComment.Text))
            {
                Comment comment = new Comment() {ChargerId = chargerId, CommentId = Guid.NewGuid(), CommentText = entComment.Text, DateAndTime = DateTime.Now, Email = currentUser.Email, UserName = currentUser.Name };
                await CommentRepository.PostCommentAsync(comment);
                entComment.Text = null;
                await GetComments();
            }
        }

        private async void LvwComments_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (lvwComments.SelectedItem != null)
            {
                Comment comment = lvwComments.SelectedItem as Comment;
                if (comment.Email == currentUser.Email && comment.UserName == currentUser.Name)
                {
                    string result = await DisplayPromptAsync(title: "Update comment", message: null, initialValue: comment.CommentText, accept: "Update", cancel: "Cancel");
                    if (String.IsNullOrEmpty(result))
                    {
                        await CommentRepository.DelCommentAsync(comment);
                        await GetComments();
                    }
                    else
                    {
                        comment.CommentText = result;
                        await CommentRepository.PutCommentAsync(comment);
                        await GetComments();
                    }
                }
                lvwComments.SelectedItem = null;
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