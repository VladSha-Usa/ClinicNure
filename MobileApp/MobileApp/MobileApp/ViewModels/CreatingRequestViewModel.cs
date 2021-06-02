using MobileApp.Models;
using MobileApp.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
        Request request;
        Patient patient;
        List<Hospital> hospitals;
        List<DoctorForUser> doctors;
        List<Symptom> symptoms;
        List<Symptom> selectedSymptoms;

        ServerConnection<Request> connection = new ServerConnection<Request>();
        private bool isBusy;

        public List<Hospital> AllHospitals { get; set; }
        //public List<DoctorForUser> AllDoctors { get; set; }
        public List<Symptom> AllSymptoms { get; set; }

        public Hospital SelectedHospital { get; set; }
        public DoctorForUser SelectedDoctor { get; set; }

        public ICommand SendCommand { get; set; }
        public ICommand BackCommand { get; set; }

        public INavigation Navigation { get; set; }

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                isBusy = value;
                OnPropertyChanged("IsBusy");
                OnPropertyChanged("IsLoaded");
            }
        }

        public bool IsLoaded
        {
            get { return !isBusy; }
        }

        public List<Hospital> Hospitals
        {
            get { return hospitals; }
            set
            {
                hospitals = value;
                OnPropertyChanged("Hospitals");
            }
        }

        public List<DoctorForUser> Doctors
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
            this.request = new Request();
            this.patient = patient;

            hospitals = new List<Hospital>();
            doctors = new List<DoctorForUser>();
            symptoms = new List<Symptom>();
            selectedSymptoms = new List<Symptom>();

            AllHospitals = new List<Hospital>();
            //AllDoctors = new List<DoctorForUser>();
            AllSymptoms = new List<Symptom>();

            SendCommand = new Command(Send);
            BackCommand = new Command(Back);

            // CHECK URL
            connection.SetUrl("Requests/");
        }

        //
        // Server connnection!!!
        //
        public async Task GetHospitals()
        {
            ServerConnection<Hospital> hospConnection = new ServerConnection<Hospital>();
            hospConnection.SetUrl("Hospitals/");

            IsBusy = true;
            IEnumerable<Hospital> hosps = await hospConnection.Get();

            if (hosps != null)
            {
                // Clear list
                while (AllHospitals.Any())
                {
                    AllHospitals.RemoveAt(AllHospitals.Count - 1);
                }

                // Add data
                foreach (Hospital h in hosps)
                {
                    AllHospitals.Add(h);
                }

                IsBusy = false;
            }
            else
            {
                // Testing Data
                DoctorForUser d1 = new DoctorForUser { Name = "Петренко В.О." };
                DoctorForUser d2 = new DoctorForUser { Name = "Семененко О.О." };
                DoctorForUser d3 = new DoctorForUser { Name = "Мартинекно О.В." };
                DoctorForUser d4 = new DoctorForUser { Name = "Олександров О.О." };
                DoctorForUser d5 = new DoctorForUser { Name = "Верещагін К.Ю." };
                DoctorForUser d6 = new DoctorForUser { Name = "Шостакова О.В." };
                DoctorForUser d7 = new DoctorForUser { Name = "Семененко В.О." };
                DoctorForUser d8 = new DoctorForUser { Name = "Літовка К.І." };
                DoctorForUser d9 = new DoctorForUser { Name = "Кравченко Л.Г." };
                DoctorForUser d0 = new DoctorForUser { Name = "Фоменко К.В." };

                Hospital h1 = new Hospital { Name = "Міська поліклініка №1", Doctors = new List<DoctorForUser>() { d1, d2, d0, d5 } };
                Hospital h2 = new Hospital { Name = "Міська поліклініка №2", Doctors = new List<DoctorForUser>() { d3, d4, d8 } };
                Hospital h3 = new Hospital { Name = "Міська поліклініка №3", Doctors = new List<DoctorForUser>() { d6, d7, d9 } };

                AllHospitals.Add(h1);
                AllHospitals.Add(h2);
                AllHospitals.Add(h3);

                /*
                List<Hospital> temp = new List<Hospital>();
                temp.AddRange(AllHospitals);
                Hospitals = temp;
                */

                //OnPropertyChanged("Hospitals");
            }

            List<Hospital> temp = new List<Hospital>();
            temp.AddRange(AllHospitals);
            Hospitals = temp;
        }

        //
        // Server connnection!!!
        //
        public async Task GetSymptoms()
        {
            ServerConnection<Symptom> symptConnection = new ServerConnection<Symptom>();
            symptConnection.SetUrl("Hospitals/");
            IsBusy = true;
            IEnumerable<Symptom> hosps = await symptConnection.Get();

            if (hosps != null)
            {
                // Clear list
                while (AllHospitals.Any())
                {
                    AllHospitals.RemoveAt(AllHospitals.Count - 1);
                }

                // Add data
                foreach (Symptom s in hosps)
                {
                    AllSymptoms.Add(s);
                }

                IsBusy = false;
            }
            else
            {
                // Testing Data
                Symptom s1 = new Symptom() { Name = "біль у животі" };
                Symptom s2 = new Symptom() { Name = "біль у спині" };
                Symptom s3 = new Symptom() { Name = "головний біль" };
                Symptom s4 = new Symptom() { Name = "запаморочення" };
                Symptom s5 = new Symptom() { Name = "кашель" };
                Symptom s6 = new Symptom() { Name = "нудота" };
                Symptom s7 = new Symptom() { Name = "нежить" };
                Symptom s8 = new Symptom() { Name = "підвищена температура" };
                Symptom s9 = new Symptom() { Name = "судоми (ноги)" };
                Symptom s0 = new Symptom() { Name = "судоми (руки)" };

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

                /*
                List<Symptom> temp = new List<Symptom>();
                temp.AddRange(AllSymptoms);
                Symptoms = temp;
                */

                //OnPropertyChanged("Symptoms");
            }

            List<Symptom> temp = new List<Symptom>();
            temp.AddRange(AllSymptoms);
            Symptoms = temp;
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

                bool result = await connection.Add(request);

                if (result)
                {
                    await page.DisplayAlert("Запит відправлено", "", "OK");
                    await Navigation.PopAsync();
                }
                else
                {
                    await page.DisplayAlert("", "Проблеми із доступом до серверу", "ОК");
                }
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
