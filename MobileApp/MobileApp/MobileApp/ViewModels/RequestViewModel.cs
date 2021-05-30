using MobileApp.Models;
using MobileApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class RequestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ServerConnection<Request> serverConncetion = new ServerConnection<Request>();

        public Request Request { get; set; }
        public bool IsPayment { get; private set; }

        public ICommand PayCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public INavigation Navigation { get; set; }

        public RequestViewModel(Request request)
        {
            Request = request;

            if (Request.State.Equals("Необхідне обстеження"))
            {
                IsPayment = true;
            }

            PayCommand = new Command(Pay);
            BackCommand = new Command(Back);
        }

        private async void Pay()
        {
            //
            // PrivateBank API
            //

            //
            // Server connection!!!
            //
        }

        private async void Back()
        {
            await Navigation.PopAsync();
        }
    }
}
