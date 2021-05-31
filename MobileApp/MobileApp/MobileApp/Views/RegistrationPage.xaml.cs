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
    public partial class RegistrationPage : ContentPage
    {
        UserViewModel viewModel;

        public RegistrationPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            viewModel = new UserViewModel(this, true) { Navigation = this.Navigation };
            BindingContext = viewModel;
        }

        private void name_Focused(object sender, FocusEventArgs e)
        {
            nameBorder.Color = Color.FromHex("#4F89AA");
        }

        private void name_Unfocused(object sender, FocusEventArgs e)
        {
            if (!viewModel.ValidateName())
            {
                nameBorder.Color = Color.Red;
            }
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