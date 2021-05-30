using MobileApp.AuthHelpers;
using MobileApp.Models;
using MobileApp.Services;
using MobileApp.Views;
using Newtonsoft.Json;
using Plugin.FacebookClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    class UserViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public Models.User Patient { get; set; }

        public ICommand RegistrationCommand { protected set; get; }
        public ICommand FBRegCommand { protected set; get; }
        public ICommand GRegCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }

        public INavigation Navigation { get; set; }

        ServerConnection<Models.User> connection = new ServerConnection<Models.User>();
        IFacebookClient facebookService = CrossFacebookClient.Current;
        bool isRegistrtion;
        Account account;
        AccountStore store;

        public UserViewModel(bool isRegistrtion)
        {
            this.isRegistrtion = isRegistrtion;

            Patient = new Models.User();
            RegistrationCommand = new Command(RegistrPatient);
            FBRegCommand = new Command(async () => await RegistrByFacebook());
            GRegCommand = new Command(RegistrByGoogle);
            BackCommand = new Command(Back);
            store = AccountStore.Create();
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
        private async Task RegistrByFacebook()
        {
            try
            {
                if (facebookService.IsLoggedIn)
                {
                    facebookService.Logout();
                }

                EventHandler<FBEventArgs<string>> userDataDelegate = null;

                userDataDelegate = async (object sender, FBEventArgs<string> e) =>
                {
                    if (e == null)
                    {
                        return;
                    }

                    switch (e.Status)
                    {
                        case FacebookActionStatus.Completed:
                            var facebookProfile = await Task.Run(() => JsonConvert.DeserializeObject<FacebookProfile>(e.Data));
                            var socialLoginData = new NetworkAuthData
                            {
                                Email = facebookProfile.Email,
                                Name = $"{facebookProfile.FirstName} {facebookProfile.LastName}",
                                Id = facebookProfile.Id
                            };

                            Patient.Email = socialLoginData.Email;
                            Patient.Name = socialLoginData.Name;

                            await Navigation.PushAsync(new WelcomePage(Patient));
                            break;
                        case FacebookActionStatus.Canceled:
                            break;
                    }

                    facebookService.OnUserData -= userDataDelegate;
                };

                facebookService.OnUserData += userDataDelegate;

                string[] fbRequestFields = { "email", "first_name", "gender", "last_name" };
                string[] fbPermisions = { "email" };
                await facebookService.RequestUserDataAsync(fbRequestFields, fbPermisions);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        // Google API
        private void RegistrByGoogle()
        {
            string clientId = null;
            string redirectUri = null;

            switch (Device.RuntimePlatform)
            {
                case Device.Android:
                    clientId = AppConstant.Constants.AndroidClientId;
                    redirectUri = AppConstant.Constants.AndroidRedirectUrl;
                    break;
            }

            account = store.FindAccountsForService(AppConstant.Constants.AppName).FirstOrDefault();

            var authenticator = new OAuth2Authenticator(
                clientId,
                null,
                AppConstant.Constants.Scope,
                new Uri(AppConstant.Constants.AuthorizeUrl),
                new Uri(redirectUri),
                new Uri(AppConstant.Constants.AccessTokenUrl),
                null,
                true);

            authenticator.Completed += OnAuthCompleted;
            authenticator.Error += OnAuthError;

            AuthenticationState.Authenticator = authenticator;

            var presenter = new Xamarin.Auth.Presenters.OAuthLoginPresenter();
            presenter.Login(authenticator);

            //await Navigation.PopAsync();
        }

        async void OnAuthCompleted(object sender, AuthenticatorCompletedEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            AuthHelpers.User user = null;
            if (e.IsAuthenticated)
            {
                // If the user is authenticated, request their basic user data from Google
                // UserInfoUrl = https://www.googleapis.com/oauth2/v2/userinfo
                var request = new OAuth2Request("GET", new Uri(AppConstant.Constants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    // Deserialize the data and store it in the account store
                    // The users email address will be used to identify data in SimpleDB
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<AuthHelpers.User>(userJson);
                }

                if (user != null)
                {
                    Patient.Name = user.Name;
                    Patient.Email = user.Email;
                    Patient.Password = null;
                    await Navigation.PushAsync(new WelcomePage(Patient));

                    //App.Current.MainPage = new NavigationPage(new WelcomePage(Patient));
                }

                //await store.SaveAsync(account = e.Account, AppConstant.Constants.AppName);
                //await DisplayAlert("Email address", user.Email, "OK");
            }
        }

        void OnAuthError(object sender, AuthenticatorErrorEventArgs e)
        {
            var authenticator = sender as OAuth2Authenticator;
            if (authenticator != null)
            {
                authenticator.Completed -= OnAuthCompleted;
                authenticator.Error -= OnAuthError;
            }

            Debug.WriteLine("Authentication error: " + e.Message);
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
