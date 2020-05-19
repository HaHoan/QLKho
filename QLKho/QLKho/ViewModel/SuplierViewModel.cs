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
    public class SuplierViewModel : BaseViewModel
    {
        private ObservableCollection<Suplier> _List;
        public ObservableCollection<Suplier> List
        {
            get => _List;
            set
            {
                _List = value;
                OnPropertyChanged();
            }
        }

        private Suplier _SelectedItem;
        public Suplier SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    Address = SelectedItem.Address;
                    Email = SelectedItem.Email;
                    Phone = SelectedItem.Phone;
                    if (SelectedItem.ContractDate is DateTime _contractDate)
                    {
                        ContractDate = _contractDate;
                    }
                    MoreInfo = SelectedItem.MoreInfo;
                }
            }
        }
        private string displayName;
        public string DisplayName
        {
            get
            {
                return displayName;
            }
            set
            {
                displayName = value;
                OnPropertyChanged();
            }
        }

        private string address;
        public string Address
        {
            get
            {
                return address;
            }
            set
            {
                address = value;
                OnPropertyChanged();
            }
        }

        private string email;
        public string Email
        {
            get
            {
                return email;
            }
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        private string phone;
        public string Phone
        {
            get
            {
                return phone;
            }
            set
            {
                phone = value;
                OnPropertyChanged();
            }
        }

        private DateTime contractDate;
        public DateTime ContractDate
        {
            get
            {
                return contractDate;
            }
            set
            {
                contractDate = value;
                OnPropertyChanged();
            }
        }

        private string moreInfo;
        public string MoreInfo
        {
            get
            {
                return moreInfo;
            }
            set
            {
                moreInfo = value;
                OnPropertyChanged();
            }
        }

        public ICommand Loaded { get; set; }
        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public SuplierViewModel()
        {
            Loaded = new RelayCommand<object>(
               (p) => { return true; },
               (p) =>
               {
                   // do st when loaded
                   ContractDate = DateTime.Now.Date;
                   List = new ObservableCollection<Suplier>((List<Suplier>)DataProvider.Instance.Supliers.Select());
               });
          
            AddCommand = new RelayCommand<object>(
             (p) =>
             {
                 if (string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(ContractDate.ToString()))
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
               var suplier = List.Where(x => x.DisplayName == DisplayName).FirstOrDefault();
               if (suplier != null)
               {
                   MessageBox.Show("Đã có tên nhà cung cấp này rồi. Hãy nhập tên khác!");
               }
               else
               {
                   List.Add((Suplier)DataProvider.Instance.Supliers.Insert(new Suplier() { DisplayName = DisplayName, Address = Address, Phone = Phone, Email = Email, ContractDate = ContractDate, MoreInfo = MoreInfo }));
               }
           }
           );

            EditCommand = new RelayCommand<object>(
            (p) =>
            {
                if (SelectedItem == null || string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Phone) || string.IsNullOrEmpty(ContractDate.ToString()))
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
              Suplier suplier = new Suplier() { Id = SelectedItem.Id, DisplayName = DisplayName, Address = Address, Phone = Phone, Email = Email, ContractDate = ContractDate, MoreInfo = MoreInfo };
              DataProvider.Instance.Supliers.Update(suplier);
              SelectedItem.DisplayName = suplier.DisplayName;
              SelectedItem.Address = suplier.Address;
              SelectedItem.Phone = suplier.Phone;
              SelectedItem.Email = suplier.Email;
              SelectedItem.ContractDate = suplier.ContractDate;
              SelectedItem.MoreInfo = suplier.MoreInfo;
          }
          );


        }
    }
}
