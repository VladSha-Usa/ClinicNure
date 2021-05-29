using MobileApp.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MobileApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new NavigationPage(new StartPage());
            MainPage = new RequestCreatingPage(new Models.User() {  Name = "Мартиненко Олексій Володимирович",
                                                            Email = "oleksii.martynenko@nure.ua", 
                                                            Password = "password" });
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
