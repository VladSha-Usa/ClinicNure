using MobileApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class CreatingRequestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        ContentPage page;
        RequestServerType request;
        Patient patient;
        List<Hospital> hospitals;
        List<Doctor> doctors;
        List<Symptom> symptoms;
        List<Symptom> selectedSymptoms;

        public List<Hospital> AllHospitals { get; set; }
        public List<Doctor> AllDoctors { get; set; }
        public List<Symptom> AllSymptoms { get; set; }

        public Hospital SelectedHospital { get; set; }
        public Doctor SelectedDoctor { get; set; }

        public ICommand SendCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public INavigation Navigation { get; set; }

        public List<Hospital> Hospitals
        {
            get { return hospitals; }
            set
            {
                hospitals = value;
                OnPropertyChanged("Hospitals");
            }
        }

        public List<Doctor> Doctors
        {
            get { return doctors; }
            set
            {
                doctors = value;
                OnPropertyChanged("Doctors");
            }
        }

        public List<Symptom> Symptoms
        {
            get { return symptoms; }
            set
            {
                symptoms = value;
                OnPropertyChanged("Symptoms");
            }
        }

        public List<Symptom> SelectedSymptoms
        {
            get { return selectedSymptoms; }
            set
            {
                selectedSymptoms = value;
                OnPropertyChanged("SelectedSymptoms");
            }
        }

        public CreatingRequestViewModel(ContentPage page, Patient patient)
        {
            this.page = page;
            this.request = new RequestServerType();
            this.patient = patient;

            hospitals = new List<Hospital>();
            doctors = new List<Doctor>();
            symptoms = new List<Symptom>();
            selectedSymptoms = new List<Symptom>();

            AllHospitals = new List<Hospital>();
            AllDoctors = new List<Doctor>();
            AllSymptoms = new List<Symptom>();

            SendCommand = new Command(Send);
            BackCommand = new Command(Back);
        }

        public async Task GetHospitals()
        {
            //
            // Server connnection!!!
            //

            // Testing Data
            Hospital h1 = new Hospital { Name = "Hospital One" };
            Hospital h2 = new Hospital { Name = "Hospital Two" };
            Hospital h3 = new Hospital { Name = "Hospital Three" };

            AllHospitals.Add(h1);
            AllHospitals.Add(h2);
            AllHospitals.Add(h3);

            List<Hospital> temp = new List<Hospital>();
            temp.AddRange(AllHospitals);
            Hospitals = temp;
            //OnPropertyChanged("Hospitals");
        }

        public async Task GetDoctors()
        {
            //
            // Server connnection!!!
            //

            // Testing Data
            Hospital h1 = new Hospital { Name = "Hospital One" };
            Hospital h2 = new Hospital { Name = "Hospital Two" };
            Hospital h3 = new Hospital { Name = "Hospital Three" };

            Doctor d1 = new Doctor { Name = "Doctor One", Hospital = h1 };
            Doctor d2 = new Doctor { Name = "Doctor Two", Hospital = h1 };
            Doctor d3 = new Doctor { Name = "Doctor Three", Hospital = h2 };
            Doctor d4 = new Doctor { Name = "Doctor Four", Hospital = h3 };
            Doctor d5 = new Doctor { Name = "Doctor Five", Hospital = h1 };
            Doctor d6 = new Doctor { Name = "Doctor Six", Hospital = h1 };
            Doctor d7 = new Doctor { Name = "Doctor Seven", Hospital = h3 };
            Doctor d8 = new Doctor { Name = "Doctor Eight", Hospital = h3 };
            Doctor d9 = new Doctor { Name = "Doctor Nine", Hospital = h2 };
            Doctor d0 = new Doctor { Name = "Doctor Ten", Hospital = h1 };

            AllDoctors.Add(d1);
            AllDoctors.Add(d2);
            AllDoctors.Add(d3);
            AllDoctors.Add(d4);
            AllDoctors.Add(d5);
            AllDoctors.Add(d6);
            AllDoctors.Add(d7);
            AllDoctors.Add(d8);
            AllDoctors.Add(d9);
            AllDoctors.Add(d0);

            List<Doctor> temp = new List<Doctor>();
            temp.AddRange(AllDoctors);
            Doctors = temp;
            //OnPropertyChanged("Doctors");
        }

        public async Task GetSymptoms()
        {
            //
            // Server connnection!!!
            //

            // Testing Data
            Symptom s1 = new Symptom() { Name = "symptom one" };
            Symptom s2 = new Symptom() { Name = "symptom two" };
            Symptom s3 = new Symptom() { Name = "symptom three" };
            Symptom s4 = new Symptom() { Name = "symptom four" };
            Symptom s5 = new Symptom() { Name = "symptom five" };
            Symptom s6 = new Symptom() { Name = "symptom six" };
            Symptom s7 = new Symptom() { Name = "symptom seven" };
            Symptom s8 = new Symptom() { Name = "symptom eight" };
            Symptom s9 = new Symptom() { Name = "symptom nine" };
            Symptom s0 = new Symptom() { Name = "symptom ten" };

            AllSymptoms.Add(s1);
            AllSymptoms.Add(s2);
            AllSymptoms.Add(s3);
            AllSymptoms.Add(s4);
            AllSymptoms.Add(s5);
            AllSymptoms.Add(s6);
            AllSymptoms.Add(s7);
            AllSymptoms.Add(s8);
            AllSymptoms.Add(s9);
            AllSymptoms.Add(s0);

            List<Symptom> temp = new List<Symptom>();
            temp.AddRange(AllSymptoms);
            Symptoms = temp;
            //OnPropertyChanged("Symptoms");
        }

        public void AddSelectedSymptom(Symptom selected, char list)
        {
            List<Symptom> temp = new List<Symptom>();
            if (list == 'a')
            {
                temp.AddRange(SelectedSymptoms);
                temp.Add(selected);
                SelectedSymptoms = temp;

                temp = new List<Symptom>();
                temp.AddRange(Symptoms);
                temp.Remove(selected);
                Symptoms = temp;
            }
            else if (list == 'b')
            {
                temp.AddRange(Symptoms);
                temp.Add(selected);
                Symptoms = temp;

                temp = new List<Symptom>();
                temp.AddRange(SelectedSymptoms);
                temp.Remove(selected);
                SelectedSymptoms = temp;
            }

            OnPropertyChanged("Symptoms");
            OnPropertyChanged("SelectedSymptoms");
        }

        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private async void Send()
        {
            if (SelectedHospital != null &&
                SelectedDoctor != null &&
                SelectedSymptoms?.Count > 0)
            {
                request.Date = DateTime.Now.ToString("dd.MM.yy");
                request.Symptoms = SelectedSymptoms;
                request.Patient = patient;
                request.Hospital = SelectedHospital;
                request.Doctor = SelectedDoctor;

                //
                // Server connection!!!
                //

                await page.DisplayAlert("Запит відправлено", "", "OK");
                await Navigation.PopAsync();
            }
            else
            {
                await page.DisplayAlert("", "Перевірте обрані пункти", "OK");
            }
        }

        private async void Back()
        {
            await Navigation.PopAsync();
        }
    }
}
