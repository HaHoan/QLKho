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
    public class InputViewModel : BaseViewModel
    {
        private ObservableCollection<InputInfo> list;
        public ObservableCollection<InputInfo> List
        {
            get => list;
            set
            {
                list = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Product> listProduct;
        public ObservableCollection<Product> ListProduct
        {
            get => listProduct;
            set
            {
                listProduct = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Input> listInput;
        public ObservableCollection<Input> ListInput
        {
            get => listInput;
            set
            {
                listInput = value;
                OnPropertyChanged();
            }
        }

        private InputInfo _SelectedItem;
        public InputInfo SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    BarCode = SelectedItem.Product.BarCode;
                    if (SelectedItem.Input.DateInput is DateTime date)
                    {
                        DateInput = date;
                    }

                    if (SelectedItem.Count is int count)
                    {
                        Count = count;
                    }

                    InputPrice = (double)SelectedItem.InputPrice;
                    OutputPrice = (double)SelectedItem.OutputPrice;
                    States = SelectedItem.States;
                }
            }
        }
        private string barCode;
        public string BarCode { get => barCode; set { barCode = value; OnPropertyChanged(); } }

        private DateTime dateInput;
        public DateTime DateInput { get => dateInput; set { dateInput = value; OnPropertyChanged(); } }

        private int count;
        public int Count { get => count; set { count = value; OnPropertyChanged(); } }

        private double inputPrice;
        public double InputPrice { get => inputPrice; set { inputPrice = value; OnPropertyChanged(); } }


        private double outputPrice;
        public double OutputPrice { get => outputPrice; set { outputPrice = value; OnPropertyChanged(); } }

        private string states;
        public string States { get => states; set { states = value; OnPropertyChanged(); } }

        public ICommand Loaded { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public InputViewModel()
        {
            Loaded = new RelayCommand<object>(
               (p) => { return true; },
               (p) =>
               {
                   DateInput = DateTime.Now.Date;
                   List = new ObservableCollection<InputInfo>((List<InputInfo>)DataProvider.Instance.InputInfoes.Select());
                   ListProduct = new ObservableCollection<Product>((List<Product>)DataProvider.Instance.Products.Select());
                   ListInput = new ObservableCollection<Input>((List<Input>)DataProvider.Instance.Inputs.Select());

               });
          
            AddCommand = new RelayCommand<object>(
            (p) =>
            {
                if (string.IsNullOrEmpty(BarCode) || Count == 0 || DateInput == null)
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
              Product product = ListProduct.Where(x => x.BarCode == BarCode).FirstOrDefault();
              if (product == null)
              {
                  MessageBox.Show("Không có sản phẩm này!");
                  return;
              }
              Input input = ListInput.Where(x => x.DateInput == DateInput).FirstOrDefault();
              if (input == null)
              {
                  MessageBox.Show(string.Format("Chưa có phiếu hóa đơn của ngày {0},  \n Hãy tạo phiếu hóa đơn trước!", DateInput.ToString()));
                  return;
              }
              if(product != null && input != null)
              {
                  MessageBox.Show("Sản phẩm này đã được nhập rồi! \n Bấm sửa để thêm số lượng hoặc sửa giá cả.");
                  return;
              }
              List.Add((InputInfo)DataProvider.Instance.InputInfoes.Insert(
                  new InputInfo()
                  {
                      IdProduct = product.Id,
                      IdInput = input.Id,
                      States = States,
                      Count = Count,
                      InputPrice = InputPrice,
                      OutputPrice = OutputPrice,
                      Product = product,
                      Input = input
                  })
                  );
          }
          );

            EditCommand = new RelayCommand<object>(
            (p) =>
            {
                if (string.IsNullOrEmpty(BarCode) || Count == 0 || DateInput == null)
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
              Product product = ListProduct.Where(x => x.BarCode == BarCode).FirstOrDefault();
              if (product == null)
              {
                  MessageBox.Show("Không có sản phẩm này!");
                  return;
              }
              Input input = ListInput.Where(x => x.DateInput == DateInput).FirstOrDefault();
              if (input == null)
              {
                  MessageBox.Show(string.Format("Chưa có phiếu hóa đơn của ngày {0}, Hãy tạo phiếu hóa đơn trước!", DateInput.ToString()));
                  return;
              }
              InputInfo inputInfo = new InputInfo()
              {
                  Id = SelectedItem.Id,
                  IdProduct = product.Id,
                  IdInput = input.Id,
                  States = States,
                  Count = Count,
                  InputPrice = InputPrice,
                  OutputPrice = OutputPrice,
                  Product = product,
                  Input = input
              };
              if (DataProvider.Instance.InputInfoes.Update(inputInfo) > 0)
              {
                  SelectedItem.IdInput = inputInfo.IdInput;
                  SelectedItem.IdProduct = inputInfo.IdProduct;
                  SelectedItem.Count = inputInfo.Count;
                  SelectedItem.InputPrice = inputInfo.InputPrice;
                  SelectedItem.OutputPrice = inputInfo.OutputPrice;
                  SelectedItem.States = inputInfo.States;
                  SelectedItem.Product = inputInfo.Product;
                  SelectedItem.Input = inputInfo.Input;
              }

          }
          );
        }
    }
}

