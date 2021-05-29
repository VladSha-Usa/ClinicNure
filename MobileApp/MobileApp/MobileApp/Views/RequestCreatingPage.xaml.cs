using MobileApp.Models;
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
    public partial class RequestCreatingPage : ContentPage
    {
        public RequestCreatingPage(User patient)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private void hospital_Focused(object sender, FocusEventArgs e)
        {
            doctor.IsVisible = false;
            doctor.IsEnabled = false;
            symptom.IsVisible = false;
            symptom.IsEnabled = false;

            hospitalsListScroll.IsEnabled = true;
            hospitalsListScroll.IsVisible = true;

            scrollViewBorder.IsVisible = true;
        }

        private void hospital_Unfocused(object sender, FocusEventArgs e)
        {
            doctor.IsVisible = true;
            doctor.IsEnabled = true;
            symptom.IsVisible = true;
            symptom.IsEnabled = true;

            hospitalsListScroll.IsEnabled = false;
            hospitalsListScroll.IsVisible = false;

            scrollViewBorder.IsVisible = false;
        }

        private void doctor_Focused(object sender, FocusEventArgs e)
        {
            hospital.IsVisible = false;
            hospital.IsEnabled = false;
            symptom.IsVisible = false;
            symptom.IsEnabled = false;

            AbsoluteLayout.SetLayoutBounds(doctor, new Rectangle(20, 0, 265, AbsoluteLayout.AutoSize));

            doctorsListScroll.IsEnabled = true;
            doctorsListScroll.IsVisible = true;

            scrollViewBorder.IsVisible = true;
        }

        private void doctor_Unfocused(object sender, FocusEventArgs e)
        {
            hospital.IsVisible = true;
            hospital.IsEnabled = true;
            symptom.IsVisible = true;
            symptom.IsEnabled = true;

            AbsoluteLayout.SetLayoutBounds(doctor, new Rectangle(20, 70, 265, AbsoluteLayout.AutoSize));

            doctorsListScroll.IsEnabled = false;
            doctorsListScroll.IsVisible = false;

            scrollViewBorder.IsVisible = false;
        }

        private void symptom_Focused(object sender, FocusEventArgs e)
        {
            hospital.IsVisible = false;
            hospital.IsEnabled = false;
            doctor.IsVisible = false;
            doctor.IsEnabled = false;

            AbsoluteLayout.SetLayoutBounds(symptom, new Rectangle(20, 0, 265, AbsoluteLayout.AutoSize));

            symptomsListScroll.IsEnabled = true;
            symptomsListScroll.IsVisible = true;

            scrollViewBorder.IsVisible = true;
        }

        private void symptom_Unfocused(object sender, FocusEventArgs e)
        {
            hospital.IsVisible = true;
            hospital.IsEnabled = true;
            doctor.IsVisible = true;
            doctor.IsEnabled = true;

            AbsoluteLayout.SetLayoutBounds(symptom, new Rectangle(20, 140, 265, AbsoluteLayout.AutoSize));

            symptomsListScroll.IsEnabled = false;
            symptomsListScroll.IsVisible = false;

            scrollViewBorder.IsVisible = false;
        }
    }
}