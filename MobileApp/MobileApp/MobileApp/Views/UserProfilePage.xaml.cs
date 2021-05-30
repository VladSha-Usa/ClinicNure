using MobileApp.Models;
using MobileApp.ViewModels;
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
        RequestsListViewModel viewModel;

        public UserPage(User patient)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            viewModel = new RequestsListViewModel(patient) { Navigation = this.Navigation };
            BindingContext = viewModel;

            this.patient = patient;
            userName.Text = this.patient.Name;
            userEmail.Text = this.patient.Email;
        }

        protected override async void OnAppearing()
        {
            await viewModel.GetRequests();
            base.OnAppearing();
        }
    }
}