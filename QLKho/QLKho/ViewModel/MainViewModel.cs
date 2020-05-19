using QLKho.Databases;
using QLKho.Databases.Entity_FW;
using System;
using System.Collections.Generic;
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

        private string timeNow;
        private DateTime dateNow;
        private DispatcherTimer dispatcherTimer;

        public string TimeNow { get { return timeNow; } set { timeNow = value; OnPropertyChanged(); } }
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

                if(IsHaveOutputEmpty(dateBefore) is int IdOutput)
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
