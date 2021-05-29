using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        User patient;

        public WelcomePage(User patient)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            this.patient = patient;

            // Binding Path testing
            //DisplayAlert("isValid", $"{ isValid }", "OK");

            userName.Text = this.patient.Name.Split(' ')[0];
        }

        private async void profileBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new UserPage(patient));
        }

        private async void createRequestBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new RequestCreatingPage(patient));
        }
    }
}