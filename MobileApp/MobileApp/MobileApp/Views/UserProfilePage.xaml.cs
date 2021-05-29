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
    public partial class UserPage : ContentPage
    {
        User patient;

        public UserPage(User patient)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            this.patient = patient;
            userName.Text = this.patient.Name;
            userEmail.Text = this.patient.Email;
        }

        private async void exitBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }
    }
}