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
    public partial class RequestViewingPage : ContentPage
    {
        Request request;
        RequestViewModel viewModel;

        public RequestViewingPage(Request request)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            this.request = request;
            viewModel = new RequestViewModel(this.request) { Navigation = this.Navigation };
            BindingContext = viewModel;

            if (viewModel.IsPayment)
            {
                payment1.IsVisible = true;
                payment2.IsVisible = true;
                payment3.IsVisible = true;
                paymentBtn.IsVisible = true;
            }
        }

        protected override void OnAppearing()
        {
            if (viewModel.IsPayment)
            {
                payment1.IsVisible = true;
                payment2.IsVisible = true;
                payment3.IsVisible = true;
                paymentBtn.IsVisible = true;
            }
            else
            {
                payment1.IsVisible = false;
                payment2.IsVisible = false;
                payment3.IsVisible = false;
                paymentBtn.IsVisible = false;
            }

            base.OnAppearing();
        }
    }
}