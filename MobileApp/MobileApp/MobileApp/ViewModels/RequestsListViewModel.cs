using MobileApp.Models;
using MobileApp.Services;
using MobileApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileApp.ViewModels
{
    public class RequestsListViewModel : INotifyPropertyChanged
    {
        bool isBusy;    // идет ли загрузка с сервера
        User patient;
        DateTime selectedDate;
        List<Request> selectedRequests;

        ServerConnection<Request> serverConncetion = new ServerConnection<Request>();
        public ObservableCollection<Request> Requests { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand GetInfoCommand { protected set; get; }
        public ICommand BackCommand { protected set; get; }
        public ICommand ExitCommand { protected set; get; }

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

        public DateTime SelectedDate
        {
            get { return selectedDate; }
            set
            {
                if (selectedDate != value)
                {
                    selectedDate = value;
                    selectedRequests = Requests.Where(req => req.Date.Equals(selectedDate.ToString("dd.MM.yy"))).ToList<Request>();
                    OnPropertyChanged("SelectedDate");
                    OnPropertyChanged("SelectedRequests");
                }
            }
        }

        public List<Request> SelectedRequests
        {
            get { return selectedRequests; }
        }

        public RequestsListViewModel(User patient)
        {
            this.patient = patient;
            Requests = new ObservableCollection<Request>();
            selectedDate = DateTime.Now;

            GetInfoCommand = new Command(GetInfo);
            BackCommand = new Command(Back);
            ExitCommand = new Command(Exit);
        }



        //
        // Server connection!!!
        //
        public async Task GetRequests()
        {
            /*
            IsBusy = true;
            IEnumerable<Request> requests = await serverConncetion.Get();

            // очищаем список
            //Friends.Clear();
            while (Requests.Any())
            {
                Requests.RemoveAt(Requests.Count - 1);
            }

            // добавляем загруженные данные
            foreach (Request req in requests)
            {
                Requests.Add(req);
            }

            IsBusy = false;
            */

            // Testing Data
            Request req1 = new Request()
            {
                Date = "29.05.21",
                Symptoms = "запаморочення, нудота",
                Patient = patient,
                Hospital = new Hospital() { Name = "КНП \"міська поліклініка №9\"" },
                Doctor = new Doctor() { Speciality = "терапевт", Name = "Марков В.О." },
                State = "оброблено",
                Disease = new Disease() { Name = "отруєння" }
            };
            Request req2 = new Request()
            {
                Date = "30.05.21",
                Symptoms = "запаморочення, нудота",
                Patient = patient,
                Hospital = new Hospital() { Name = "КНП \"міська поліклініка №9\"" },
                Doctor = new Doctor() { Speciality = "терапевт", Name = "Марков В.О." },
                State = "Необхідне обстеження",
                Disease = new Disease() { Name = "потрібне уточнення" }
            };

            Requests.Add(req1);
            Requests.Add(req2);

            selectedRequests = Requests.Where(req => req.Date.Equals(selectedDate.ToString("dd.MM.yy"))).ToList<Request>();
            OnPropertyChanged("SelectedRequests");
        }



        protected void OnPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private async void GetInfo()
        {
            if (selectedRequests.Count > 0)
            {
                await Navigation.PushAsync(new RequestViewingPage(selectedRequests[0]));
            }
        }

        private async void Back()
        {
            await Navigation.PopAsync();
        }

        private async void Exit()
        {
            await Navigation.PopToRootAsync();
        }
    }
}
