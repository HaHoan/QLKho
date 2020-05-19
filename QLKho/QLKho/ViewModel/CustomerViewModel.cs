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
    public class CustomerViewModel : BaseViewModel
    {
        private ObservableCollection<Customer> _List;
        public ObservableCollection<Customer> List
        {
            get => _List;
            set
            {
                _List = value;
                OnPropertyChanged();
            }
        }

        private Customer _SelectedItem;
        public Customer SelectedItem
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

        public CustomerViewModel()
        {
            Loaded = new RelayCommand<object>(
               (p) => { return true; },
               (p) =>
               {
                   // do st when loaded
                   List = new ObservableCollection<Customer>((List<Customer>)DataProvider.Instance.Customers.Select());
               });

            AddCommand = new RelayCommand<object>(
             (p) =>
             {
                 if (string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Phone))
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
               var customer = List.Where(x => x.DisplayName == DisplayName).FirstOrDefault();
               if (customer != null)
               {
                   MessageBox.Show("Đã có tên nhà cung cấp này rồi. Hãy nhập tên khác!");
               }
               else
               {
                   List.Add((Customer)DataProvider.Instance.Customers.Insert(new Customer() { DisplayName = DisplayName, Address = Address, Phone = Phone, Email = Email, MoreInfo = MoreInfo }));
               }
           }
           );

            EditCommand = new RelayCommand<object>(
            (p) =>
            {
                if (SelectedItem == null || string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(Address) || string.IsNullOrEmpty(Phone))
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
              Customer customer = new Customer() { Id = SelectedItem.Id, DisplayName = DisplayName, Address = Address, Phone = Phone, Email = Email, MoreInfo = MoreInfo };
              DataProvider.Instance.Customers.Update(customer);
              SelectedItem.DisplayName = customer.DisplayName;
              SelectedItem.Address = customer.Address;
              SelectedItem.Phone = customer.Phone;
              SelectedItem.Email = customer.Email;
              SelectedItem.MoreInfo = customer.MoreInfo;
          }
          );
        }
    }
}
