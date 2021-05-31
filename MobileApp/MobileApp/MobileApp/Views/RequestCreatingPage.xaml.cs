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
    public partial class RequestCreatingPage : ContentPage
    {
        User patient;
        CreatingRequestViewModel viewModel;

        public RequestCreatingPage(User patient)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            this.patient = patient;
            this.viewModel = new CreatingRequestViewModel(this, this.patient) { Navigation = this.Navigation };
            BindingContext = viewModel;
        }

        protected override async void OnAppearing()
        {
            await viewModel.GetHospitals();
            await viewModel.GetDoctors();
            await viewModel.GetSymptoms();
            base.OnAppearing();
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

            List<Hospital> temp = new List<Hospital>();
            temp.AddRange(viewModel.AllHospitals);
            viewModel.Hospitals = temp;

            symptomsScroll.Margin = new Thickness(0, 200, 0, 0);

            //viewModel.Hospitals = viewModel.AllHospitals;
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

            symptomsScroll.Margin = new Thickness(0, -50, 0, 0);
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

            List<Doctor> temp = new List<Doctor>();
            temp.AddRange(viewModel.AllDoctors);
            viewModel.Doctors = temp.Where(doc => doc.Hospital.Name.Equals(hospital.Text)).ToList();

            symptomsScroll.Margin = new Thickness(0, 200, 0, 0);

            //viewModel.Doctors = viewModel.AllDoctors;
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

            symptomsScroll.Margin = new Thickness(0, -50, 0, 0);
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

            symptomsScroll.Margin = new Thickness(0, 200, 0, 0);
            //viewModel.Symptoms = viewModel.Symptoms;
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

            symptomsScroll.Margin = new Thickness(0, -50, 0, 0);
        }

        private void hospitalsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Hospital selected = hospitalsList.SelectedItem as Hospital;
            hospital.Text = selected.Name;
            viewModel.SelectedHospital = selected;
            doctor.Text = null;
            viewModel.SelectedDoctor = null;
        }

        private void doctorsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Doctor selected = doctorsList.SelectedItem as Doctor;
            doctor.Text = selected.Name;
            viewModel.SelectedDoctor = selected;
        }

        private void symptomsList_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            symptomsScroll.IsEnabled = true;
            symptomsScroll.IsVisible = true;
            symptomsStack.IsEnabled = true;
            symptomsStack.IsVisible = true;

            Symptom selected = symptomsList.SelectedItem as Symptom;
            viewModel.AddSelectedSymptom(selected, 'a'); 
        }

        private void symptoms_ItemSelected(object sender, ItemTappedEventArgs e)
        {
            Symptom selected = symptoms.SelectedItem as Symptom;
            viewModel.AddSelectedSymptom(selected, 'b');

            if (viewModel.SelectedSymptoms.Count == 0)
            {
                symptomsScroll.IsEnabled = false;
                symptomsScroll.IsVisible = false;
                symptomsStack.IsEnabled = false;
                symptomsStack.IsVisible = false;
            }
        }
    }
}