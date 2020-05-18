using QLKho.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

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
        public MainViewModel()
        {
            Loaded = new RelayCommand<object>(
                (p) => { return true; },
                (p) =>
                {
                    // do st when loaded

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
    }
}
