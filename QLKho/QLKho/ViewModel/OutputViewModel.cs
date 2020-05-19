using QLKho.Databases;
using QLKho.Databases.Entity_FW;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace QLKho.ViewModel
{
    public class OutputViewModel : BaseViewModel
    {
        private ObservableCollection<OutputInfo> list;
        public ObservableCollection<OutputInfo> List
        {
            get => list;
            set
            {
                list = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<InputInfo> listInputInfo;
        public ObservableCollection<InputInfo> ListInputInfo
        {
            get => listInputInfo;
            set
            {
                listInputInfo = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Output> listOutput;
        public ObservableCollection<Output> ListOutput
        {
            get => listOutput;
            set
            {
                listOutput = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Customer> listCustomer;
        public ObservableCollection<Customer> ListCustomer
        {
            get => listCustomer;
            set
            {
                listCustomer = value;
                OnPropertyChanged();
            }
        }
        private Customer selectedCustomer;
        public Customer SelectedCustomer
        {
            get
            {
                return selectedCustomer;
            }
            set
            {
                selectedCustomer = value;
                OnPropertyChanged();
            }
        }
        private OutputInfo _SelectedItem;
        public OutputInfo SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    BarCode = SelectedItem.InputInfo.Product.BarCode;
                    if (SelectedItem.Output.DateOutput is DateTime date)
                    {
                        DateOutput = date;
                    }

                    if (SelectedItem.Count is int count)
                    {
                        Count = count;
                    }

                    InputPrice = (double)SelectedItem.InputInfo.InputPrice;
                    OutputPrice = (double)SelectedItem.OutputPrice;
                    States = SelectedItem.States;
                    if (SelectedItem.IdCustomer != null)
                    {
                        SelectedCustomer = ListCustomer.Where(x => x.Id == SelectedItem.IdCustomer).FirstOrDefault();
                    }
                    else
                    {
                        SelectedCustomer = null;
                    }
                }
            }
        }
        private string barCode;
        public string BarCode
        {
            get => barCode;
            set
            {
                barCode = value;
                OnPropertyChanged();
                InputInfo inputInfo = ListInputInfo.Where(x => x.Product.BarCode == BarCode).LastOrDefault();
                if(inputInfo != null)
                {
                    InputPrice = (double)inputInfo.InputPrice;
                    OutputPrice = (double)inputInfo.OutputPrice;
                } else
                {
                    InputPrice = 0;
                    OutputPrice = 0;
                }
                
            }
        }

        private DateTime dateOutput;
        public DateTime DateOutput { get => dateOutput; set { dateOutput = value; OnPropertyChanged(); } }

        private int count;
        public int Count { get => count; set { count = value; OnPropertyChanged(); } }

        private double inputPrice;
        public double InputPrice { get => inputPrice; set { inputPrice = value; OnPropertyChanged(); } }


        private double outputPrice;
        public double OutputPrice { get => outputPrice; set { outputPrice = value; OnPropertyChanged(); } }

        private string states;
        public string States { get => states; set { states = value; OnPropertyChanged(); } }

        private string customerName;
        public string CustomerName { get => customerName; set { customerName = value; OnPropertyChanged(); } }

        public ICommand Loaded { get;  set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }

        public OutputViewModel()
        {
            Loaded = new RelayCommand<object>(
               (p) => { return true; },
               (p) =>
               {
                   DateOutput = DateTime.Now.Date;
                   List = new ObservableCollection<OutputInfo>((List<OutputInfo>)DataProvider.Instance.OutputInfoes.Select());
                   ListInputInfo = new ObservableCollection<InputInfo>((List<InputInfo>)DataProvider.Instance.InputInfoes.Select());
                   ListOutput = new ObservableCollection<Output>((List<Output>)DataProvider.Instance.Outputs.Select());
                   ListCustomer = new ObservableCollection<Customer>((List<Customer>)DataProvider.Instance.Customers.Select());

               });
          
            AddCommand = new RelayCommand<object>(
          (p) =>
          {
              if (string.IsNullOrEmpty(BarCode) || Count == 0 || DateOutput == null)
              {
                  return false;
              }
              else
              {
                  return true;
              }
          },
        (p) =>
        {
            InputInfo inputInfo = ListInputInfo.Where(x => x.Product.BarCode == BarCode).FirstOrDefault();
            if (inputInfo == null)
            {
                MessageBox.Show("Sản phẩm này chưa được nhập!");
                return;
            }
            Output output = ListOutput.Where(x => x.DateOutput == DateOutput).FirstOrDefault();
            if (output == null)
            {
                MessageBox.Show(string.Format("Chưa có phiếu hóa đơn của ngày {0}, Hãy tạo phiếu hóa đơn trước!", DateOutput.ToString()));
                return;
            }

            Customer customer = ListCustomer.Where(x => x.DisplayName == CustomerName).FirstOrDefault();
            if (customer == null)
            {
                MessageBox.Show("Chưa có khách hàng này!");
                return;
            }

            List.Add((OutputInfo)DataProvider.Instance.OutputInfoes.Insert(
                new OutputInfo()
                {
                    IdInputInfo = inputInfo.Id,
                    IdOutput = output.Id,
                    States = States,
                    Count = Count,
                    OutputPrice = OutputPrice,
                    InputInfo = inputInfo,
                    Output = output,
                    Customer = SelectedCustomer,
                    IdCustomer = SelectedCustomer.Id
                })
                );
        }
        );
            EditCommand = new RelayCommand<object>(
           (p) =>
           {
               if (string.IsNullOrEmpty(BarCode) || Count == 0 || DateOutput == null)
               {
                   return false;
               }
               else
               {
                   return true;
               }
           },
         (p) =>
         {
             InputInfo inputInfo = ListInputInfo.Where(x => x.Product.BarCode == BarCode).FirstOrDefault();

             if (inputInfo == null)
             {
                 MessageBox.Show("Sản phẩm này chưa được nhập!");
                 return;
             }
             Output output = ListOutput.Where(x => x.DateOutput == DateOutput).FirstOrDefault();
             if (output == null)
             {
                 MessageBox.Show(string.Format("Chưa có phiếu hóa đơn của ngày {0}, Hãy tạo phiếu hóa đơn trước!", DateOutput.ToString()));
                 return;
             }

             OutputInfo outputInfo = new OutputInfo()
             {
                 Id = SelectedItem.Id,
                 IdInputInfo = inputInfo.Id,
                 IdOutput = output.Id,
                 States = States,
                 Count = Count,
                 OutputPrice = OutputPrice,
                 InputInfo = inputInfo,
                 Output = output,
                 Customer = SelectedCustomer,
                 IdCustomer = SelectedCustomer.Id
             };
             if (DataProvider.Instance.OutputInfoes.Update(outputInfo) > 0)
             {
                 SelectedItem.IdOutput = outputInfo.IdOutput;
                 SelectedItem.IdInputInfo = outputInfo.IdInputInfo;
                 SelectedItem.Count = outputInfo.Count;
                 SelectedItem.OutputPrice = outputInfo.OutputPrice;
                 SelectedItem.States = outputInfo.States;
                 SelectedItem.InputInfo = outputInfo.InputInfo;
                 SelectedItem.Output = outputInfo.Output;
                 SelectedItem.Customer = outputInfo.Customer;
                 SelectedItem.IdCustomer = outputInfo.IdCustomer;
             }

         }
         );
        }
    }
}
