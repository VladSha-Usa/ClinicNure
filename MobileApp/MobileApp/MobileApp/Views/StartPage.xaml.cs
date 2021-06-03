using MobileApp.Models;
using MobileApp.Services;
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
    public partial class StartPage : ContentPage
    {
        public StartPage()
        {
            InitializeComponent();

            this.BackgroundColor = Color.FromHex("#AECDDE");
            NavigationPage.SetHasNavigationBar(this, false);
        }

        private async void enteringBtn_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new LoginPage());
        }

        private async void registrationBtn_Clicked(object sender, EventArgs e)
        {
            /*
            ServerConnection<Patient> sc = new ServerConnection<Patient>();
            
            sc.SetUrl("Patients/");

            await sc.Delete("fb:oleksij.martynenko@gmail.com");
            await sc.Delete("g:qwerty2619086@gmail.com");
            */

            /*
            ServerConnection<Symptom> sc = new ServerConnection<Symptom>();

            sc.SetUrl("Symptoms/");

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

            await sc.Add(s1);
            await sc.Add(s2);
            await sc.Add(s3);
            await sc.Add(s4);
            await sc.Add(s5);
            await sc.Add(s6);
            await sc.Add(s7);
            await sc.Add(s8);
            await sc.Add(s9);
            await sc.Add(s0);
            */

            /*
            ServerConnection<Hospital> sc = new ServerConnection<Hospital>();

            sc.SetUrl("Hospitals/");

            DoctorForUser d1 = new DoctorForUser { Email = "petrenko.viktor@siplus.me", Password = "123456", Name = "Петренко В.О.", Speciality = "терапевт" };
            DoctorForUser d2 = new DoctorForUser { Email = "semenenko.olena@siplus.me", Password = "123456", Name = "Семененко О.О.", Speciality = "терапевт" };
            DoctorForUser d3 = new DoctorForUser { Email = "martynenko.oleksii@siplus.me", Password = "123456", Name = "Мартинекно О.В.", Speciality = "терапевт" };
            DoctorForUser d4 = new DoctorForUser { Email = "oleksandrov.oleksandr@siplus.me", Password = "123456", Name = "Олександров О.О.", Speciality = "терапевт" };
            DoctorForUser d5 = new DoctorForUser { Email = "vereshagin.kiril@siplus.me", Password = "123456", Name = "Верещагін К.Ю.", Speciality = "терапевт" };
            DoctorForUser d6 = new DoctorForUser { Email = "shoatakova.oksana@siplus.me", Password = "123456", Name = "Шостакова О.В.", Speciality = "терапевт" };
            DoctorForUser d7 = new DoctorForUser { Email = "semenenko.vasil@siplus.me", Password = "123456", Name = "Семененко В.О.", Speciality = "терапевт" };
            DoctorForUser d8 = new DoctorForUser { Email = "litovka.kristina@siplus.me", Password = "123456", Name = "Літовка К.І.", Speciality = "терапевт" };
            DoctorForUser d9 = new DoctorForUser { Email = "kravchenko.leonid@siplus.me", Password = "123456", Name = "Кравченко Л.Г.", Speciality = "терапевт" };
            DoctorForUser d0 = new DoctorForUser { Email = "fomenko.kyryl@siplus.me", Password = "123456", Name = "Фоменко К.В.", Speciality = "терапевт" };

            Hospital h1 = new Hospital { Name = "Міська поліклініка №1", Doctors = new List<DoctorForUser>() { d1, d2, d0, d5 } };
            Hospital h2 = new Hospital { Name = "Міська поліклініка №2", Doctors = new List<DoctorForUser>() { d3, d4, d8 } };
            Hospital h3 = new Hospital { Name = "Міська поліклініка №3", Doctors = new List<DoctorForUser>() { d6, d7, d9 } };

            await sc.Add(h1);
            await sc.Add(h2);
            await sc.Add(h3);
            */


            await Navigation.PushAsync(new RegistrationPage());
        }
    }
}