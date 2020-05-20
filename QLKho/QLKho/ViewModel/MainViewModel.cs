﻿using QLKho.Databases;
using QLKho.Databases.Entity_FW;
using QLKho.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Threading;

namespace QLKho.ViewModel
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand Loaded { get; set; }
        public ICommand OpenInputCommand { get; set; }
        public ICommand OpenOnputCommand { get; set; }
        public ICommand OpenProductCommand { get; set; }
        public ICommand OpenUnitCommand { get; set; }
        public ICommand OpenSuplierCommand { get; set; }
        public ICommand OpenCustomerCommand { get; set; }
        public ICommand FilterCommand { get; set; }

        private DateTime dateNow;
        private DispatcherTimer dispatcherTimer;
        private DateTime dateFrom;
        public DateTime DateFrom { get { return dateFrom; } set { dateFrom = value; OnPropertyChanged(); } }

        private DateTime dateTo;
        public DateTime DateTo { get { return dateTo; } set { dateTo = value; OnPropertyChanged(); } }

        private string timeNow;
        public string TimeNow { get { return timeNow; } set { timeNow = value; OnPropertyChanged(); } }

        private int totalInput;
        public int TotalInput { get { return totalInput; } set { totalInput = value; OnPropertyChanged(); } }

        private int totalOutput;
        public int TotalOutput {
            get {
                return totalOutput;
            }
            set {
                totalOutput = value;
                OnPropertyChanged();
                TotalInventory = totalInput - TotalOutput;
            }
        }

        private int totalInventory;
        public int TotalInventory { get { return totalInventory; } set { totalInventory = value; OnPropertyChanged(); } }
        private ObservableCollection<InventoryModel> list;
        public ObservableCollection<InventoryModel> List
        {
            get => list;
            set
            {
                list = value;
                OnPropertyChanged();
            }
        }


        public MainViewModel()
        {
            Loaded = new RelayCommand<object>(
                (p) => { return true; },
                (p) =>
                {
                    // do st when loaded
                    dateNow = DateTime.Now;
                    TimeNow = string.Format("{0:F}", dateNow);
                    dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
                    dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 1);
                    dispatcherTimer.Start();
                    DateTo = DateTime.Now;
                    DateFrom = new DateTime(DateTo.Year, DateTo.Month, 1);
                    TotalInput = DataProvider.Instance.InputInfoes.TotalInput(DateFrom.Date, DateTo.Date);
                    TotalOutput = DataProvider.Instance.OutputInfoes.TotalOutput(DateFrom.Date, DateTo.Date);
                });
            FilterCommand = new RelayCommand<object>(
                (p) =>
                {
                    return DateFrom != null && DateTo != null;
                },
                (p) =>
             {
                 TotalInput = DataProvider.Instance.InputInfoes.TotalInput(DateFrom.Date, DateTo.Date);
                 TotalOutput = DataProvider.Instance.OutputInfoes.TotalOutput(DateFrom.Date, DateTo.Date);
                 List = new ObservableCollection<InventoryModel>((List<InventoryModel>)DataProvider.Instance.OutputInfoes.GetInventory(DateFrom.Date, DateTo.Date));

             });
            OpenInputCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
             {
                 InputWindow inputWindow = new InputWindow();
                 inputWindow.ShowDialog();
             });
            OpenOnputCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                OutputWindow outputWindow = new OutputWindow();
                outputWindow.ShowDialog();
            });
            OpenProductCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                ProductWindow productWindow = new ProductWindow();
                productWindow.ShowDialog();
            });
            OpenUnitCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                UnitWindow unitWindow = new UnitWindow();
                unitWindow.ShowDialog();
            });
            OpenSuplierCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                SuplierWindow suplierWindow = new SuplierWindow();
                suplierWindow.ShowDialog();
            });
            OpenCustomerCommand = new RelayCommand<object>((p) => { return true; }, (p) =>
            {
                CustomerWindow customerWindow = new CustomerWindow();
                customerWindow.ShowDialog();
            });
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            DateTime dateBefore = dateNow;
            dateNow = DateTime.Now;
            if (dateBefore.Day != dateNow.Day)
            {
                // Đã sang ngày mới
                // Kiểm tra ngày cũ có phiếu nhập nào đã tạo mà chưa nhập không? nếu có thì xóa đi
                if (IsHaveInputEmpty(dateBefore) is int Id)
                {
                    DeleteInputEmpty(Id);
                }
                // Tạo phiếu nhập cho ngày mới
                CreateNewInput(dateNow);

                if (IsHaveOutputEmpty(dateBefore) is int IdOutput)
                {
                    DeleteOutputEmpty(IdOutput);
                }
                CreateNewOutput(dateNow);
            }

            TimeNow = string.Format("{0:F}", dateNow);
        }

        private void CreateNewOutput(DateTime dateNow)
        {
            DataProvider.Instance.Outputs.Insert(new Output() { DateOutput = dateNow.Date });
        }

        private void DeleteOutputEmpty(int id)
        {
            DataProvider.Instance.Outputs.Delete(new Output() { Id = id });
        }

        private int IsHaveOutputEmpty(DateTime dateBefore)
        {
            return DataProvider.Instance.Outputs.IsHaveOutputEmpty(dateBefore.Date);
        }

        private void CreateNewInput(DateTime dateNow)
        {
            DataProvider.Instance.Inputs.Insert(new Input() { DateInput = dateNow.Date });
        }

        private void DeleteInputEmpty(int Id)
        {
            DataProvider.Instance.Inputs.Delete(new Input() { Id = Id });
        }

        private int IsHaveInputEmpty(DateTime dateBefore)
        {
            return DataProvider.Instance.Inputs.IsHaveInputEmpty(dateBefore.Date);
        }
    }
}
