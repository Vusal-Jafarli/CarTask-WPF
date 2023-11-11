using CommandMVVM.Commands;
using CommandMVVM.Models;
using CommandMVVM.Services;
using CommandMVVM.ViewModels.PageViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CommandMVVM.ViewModels.WindowViewModels
{
    internal class EditCarViewModel : NotificationService
    {
        public int car_index;
        public ObservableCollection<Car> new_cars_list { get; set; }

        private Car new_car;

        public Car car
        {
            get { return new_car; }
            set
            {
                new_car = value;
                OnPropertyChanged();
            }
        }

        public object obj;



        public ICommand SaveCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        public EditCarViewModel(int index, ObservableCollection<Car> Cars,object obj)
        {
            //'obj' Dashboardin obyektidir.Editleri 'Save' etdikde objectin refresh_data()
            //funksiyasi vasitesile Cars listini yenilemek ucundur deyismek ucundur.
            this.obj = obj;

            car_index = index;
            new_cars_list = Cars;

            this.car = new Car();
            this.car.Make = Cars[car_index].Make;
            this.car.Model = Cars[car_index].Model;
            this.car.Year = Cars[car_index].Year;

            SaveCommand = new RelayCommand(save_execute);
            CancelCommand = new RelayCommand(cancel_execute);

        }
        public EditCarViewModel()
        {

        }
        public void save_execute(object? parameter)
        {
            new_cars_list[car_index] = car;
            DashboardViewModel? dashboard = obj as DashboardViewModel;
            dashboard.refresh_data(new_cars_list);

            Window edit_window = parameter as Window;
            edit_window.Close();
        }

        public void cancel_execute(object? parameter)
        {
            Window edit_window = parameter as Window;
            edit_window.Close();

        }
    }
}
