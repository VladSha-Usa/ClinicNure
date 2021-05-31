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
    public partial class LiqPayPage : ContentPage
    {
        RequestViewModel viewModel;

        public LiqPayPage(RequestViewModel viewModel)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            this.viewModel = viewModel;
            BindingContext = this.viewModel;

            liqPay.Source = "https://www.liqpay.ua/api/3/checkout?data=eyJ2ZXJzaW9uIjozLCJhY3Rpb24iOiJwYXkiLCJhbW91bnQiOiI0MDAiLCJjdXJyZW5jeSI6IlVBSCIsImRlc2NyaXB0aW9uIjoi0JTQvtC00LDRgtC60L7QstCwINC60L7QvdGB0YPQu9GM0YLQsNGG0ZbRjyDQtyDQu9GW0LrQsNGA0LXQvCIsInB1YmxpY19rZXkiOiJzYW5kYm94X2kxMTU0MjM5OTAxMSIsImxhbmd1YWdlIjoidWsiLCJyZXN1bHRfdXJsIjoiaHR0cHM6Ly93d3cubGlxcGF5LnVhLyJ9&signature=XZF/THHvs/E2eldEIF9jZnQAYz8=";
        }

        private async void liqPay_Navigated(object sender, WebNavigatedEventArgs e)
        {
            if ((liqPay.Source as UrlWebViewSource).Url.Equals("https://www.liqpay.ua/en"))
            {
                //
                // Server connection!!!
                //

                RequestMobileType temp = new RequestMobileType()
                {
                    Date = viewModel.Request.Date,
                    Symptoms = viewModel.Request.Symptoms,
                    Patient = viewModel.Request.Patient,
                    Hospital = viewModel.Request.Hospital,
                    Doctor = viewModel.Request.Doctor,
                    State = "Додаткова консультація сплачена",
                    Disease = viewModel.Request.Disease
                };

                viewModel.Request = temp;

                viewModel.IsPayment = false;

                await Navigation.PopAsync();
            }
        }
    }
}