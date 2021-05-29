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
    class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ServerConnection<User> connection = new ServerConnection<User>();

        bool isRegistrtion;
        public User Patient { get; set; }

        public ICommand RegistrationCommand { protected set; get; }
        public ICommand FBRegCommand { protected set; get; }
        public ICommand GRegCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }

        public INavigation Navigation { get; set; }

        public UserViewModel(bool isRegistrtion)
        {
            this.isRegistrtion = isRegistrtion;

            Patient = new User();
            RegistrationCommand = new Command(RegistrPatient);
            FBRegCommand = new Command(RegistrByFacebook);
            GRegCommand = new Command(RegistrByGoogle);
            BackCommand = new Command(Back);
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private async void RegistrPatient()
        {
            bool isValid = Validation();

            if (isValid)
            {
                await Navigation.PushAsync(new WelcomePage(Patient));
            }
            else
            {

            }
        }

        // Facebook API
        private async void RegistrByFacebook()
        {
            await Navigation.PopAsync();
        }

        // Google API
        private async void RegistrByGoogle()
        {
            await Navigation.PopAsync();
        }

        private async void Back()
        {
            await Navigation.PopAsync();
        }

        private bool Validation()
        {
            return true;
        }
    }
}
