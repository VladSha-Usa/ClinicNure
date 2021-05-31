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
    public partial class LoginPage : ContentPage
    {
        UserViewModel viewModel;

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel = new UserViewModel(this, false) { Navigation = this.Navigation};
            BindingContext = viewModel;
        }

        private void email_Focused(object sender, FocusEventArgs e)
        {
            emailBorder.Color = Color.FromHex("#4F89AA");
        }

        private void email_Unfocused(object sender, FocusEventArgs e)
        {
            if (!viewModel.ValidateEmail())
            {
                emailBorder.Color = Color.Red;
            }
        }

        private void password_Focused(object sender, FocusEventArgs e)
        {
            passwordBorder.Color = Color.FromHex("#4F89AA");
        }

        private void password_Unfocused(object sender, FocusEventArgs e)
        {
            if (!viewModel.ValidatePassword())
            {
                passwordBorder.Color = Color.Red;
            }
        }
    }
}