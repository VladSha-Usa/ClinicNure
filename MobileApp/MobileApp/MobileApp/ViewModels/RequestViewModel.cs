using MobileApp.Models;
using MobileApp.Services;
using MobileApp.Views;
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

        Request request;
        public Request Request 
        {
            get { return request; }
            set
            {
                request = value;
                OnPropertyChanged("Request");
            }
        }

        public bool IsPayment { get; set; }

        public ICommand PayCommand { get; set; }
        public ICommand BackCommand { get; set; }
        public ICommand LiqPayBackCommand { get; set; }

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
            LiqPayBackCommand = new Command(LiqPayBack);
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private async void Pay()
        {
            await Navigation.PushAsync(new LiqPayPage(this));
        }

        private async void Back()
        {
            await Navigation.PopAsync();
        }

        private async void LiqPayBack()
        {
            await Navigation.PopAsync();
        }
    }
}
