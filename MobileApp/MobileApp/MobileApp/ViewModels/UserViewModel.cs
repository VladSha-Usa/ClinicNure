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
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Auth;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    class UserViewModel
    {
        public Models.Patient Patient { get; set; }

        public ICommand RegistrationCommand { protected set; get; }
        public ICommand FBRegCommand { protected set; get; }
        public ICommand GRegCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }

        public INavigation Navigation { get; set; }

        ServerConnection<Models.User> connection = new ServerConnection<Models.User>();
        Page page;
        bool isRegistrtion;

        IFacebookClient facebookService = CrossFacebookClient.Current;
        Account account;
        AccountStore store;

        public UserViewModel(Page page, bool isRegistrtion)
        {
            this.page = page;
            this.isRegistrtion = isRegistrtion;

            Patient = new Models.Patient();
            RegistrationCommand = new Command(RegistrPatient);
            FBRegCommand = new Command(async () => await RegistrByFacebook());
            GRegCommand = new Command(RegistrByGoogle);
            BackCommand = new Command(Back);
            store = AccountStore.Create();
        }

        private async void RegistrPatient()
        {
            bool isValid = ValidatePatient();

            if (isValid)
            {
                //
                // Connect to server!!!
                //

                await Navigation.PushAsync(new WelcomePage(Patient));
            }
            else
            {
                await page.DisplayAlert("", "Перевірте правильність введених даних", "ОК");
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

                            //
                            // Connect to server!!!
                            //

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
                var request = new OAuth2Request("GET", new Uri(AppConstant.Constants.UserInfoUrl), null, e.Account);
                var response = await request.GetResponseAsync();
                if (response != null)
                {
                    string userJson = await response.GetResponseTextAsync();
                    user = JsonConvert.DeserializeObject<AuthHelpers.User>(userJson);
                }

                if (user != null)
                {
                    Patient.Name = user.Name;
                    Patient.Email = user.Email;
                    Patient.Password = null;

                    //
                    // Connect to server!!!
                    //

                    await Navigation.PushAsync(new WelcomePage(Patient));
                }
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

        private bool ValidatePatient()
        {
            return ValidateName() && ValidateEmail() && ValidatePassword();
        }

        public bool ValidateName(string name = null)
        {
            if (Patient.Name == null) return false;

            string nameRegex = @"^[a-zа-яїєіё]+\s[a-zа-яїєіё]+\s?[[a-zа-яїєіё]*]$";

            if (!Regex.IsMatch(Patient.Name, nameRegex, RegexOptions.IgnoreCase))
            {
                return false;
            }

            return true;
        }

        public bool ValidateEmail(string email = null)
        {
            if (Patient.Email == null) return false;

            string emailRegex = @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$";

            if (!Regex.IsMatch(Patient.Email, emailRegex, RegexOptions.IgnoreCase))
            {
                return false;
            }

            return true;
        }

        public bool ValidatePassword(string password = null)
        {
            if (Patient.Password == null) return false;

            string passwordRegex = @"^[a-z0-9_]{6,}$";

            if (!Regex.IsMatch(Patient.Password, passwordRegex, RegexOptions.IgnoreCase))
            {
                return false;
            }

            return true;
        }
    }
}
