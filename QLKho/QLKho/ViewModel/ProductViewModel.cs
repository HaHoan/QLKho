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
    public class ProductViewModel : BaseViewModel
    {
        private ObservableCollection<Product> _List;
        public ObservableCollection<Product> List
        {
            get => _List;
            set
            {
                _List = value;
                OnPropertyChanged();
            }
        }

        private Product _SelectedItem;
        public Product SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                    BarCode = SelectedItem.BarCode;
                    States = SelectedItem.States;
                    if (SelectedItem.IdUnit is int IdUnit)
                    {
                        this.IdUnit = IdUnit;
                        SelectedUnit = ListUnit.Where(x => x.Id == IdUnit).FirstOrDefault();
                    }
                    if (SelectedItem.IdSuplier is int IdSuplier)
                    {
                        this.IdSuplier = IdSuplier;
                        SelectedSuplier = ListSuplier.Where(x => x.Id == IdSuplier).FirstOrDefault();
                    }
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

        private string barCode;
        public string BarCode
        {
            get
            {
                return barCode;
            }
            set
            {
                barCode = value;
                OnPropertyChanged();
            }
        }

        private string states;
        public string States
        {
            get
            {
                return states;
            }
            set
            {
                states = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Unit> listUnit;

        private Unit _SelectedUnit;
        private Nullable<int> IdUnit;
        public Unit SelectedUnit
        {
            get => _SelectedUnit;
            set
            {
                _SelectedUnit = value;
                OnPropertyChanged();
                if (SelectedUnit != null)
                {
                    IdUnit = SelectedUnit.Id;
                } else
                {
                    IdUnit = null;
                }
            }
        }
        public ObservableCollection<Unit> ListUnit
        {
            get => listUnit;
            set
            {
                listUnit = value;
                OnPropertyChanged();
            }
        }

        private ObservableCollection<Suplier> listSuplier;
        public ObservableCollection<Suplier> ListSuplier
        {
            get => listSuplier;
            set
            {
                listSuplier = value;
                OnPropertyChanged();
            }
        }

        private Suplier _SelectedSuplier;
        private Nullable<int> IdSuplier;
        public Suplier SelectedSuplier
        {
            get => _SelectedSuplier;
            set
            {
                _SelectedSuplier = value;
                OnPropertyChanged();
                if (SelectedSuplier != null)
                {
                    IdSuplier = SelectedSuplier.Id;
                } else
                {
                    IdSuplier = null;
                }
            }
        }

        public ICommand AddCommand { get; set; }
        public ICommand EditCommand { get; set; }
        public ICommand Loaded { get; set; }

        public ProductViewModel()
        {
            Loaded = new RelayCommand<object>(
               (p) => { return true; },
               (p) =>
               {
                   ListUnit = new ObservableCollection<Unit>((List<Unit>)DataProvider.Instance.Units.Select());
                   ListSuplier = new ObservableCollection<Suplier>((List<Suplier>)DataProvider.Instance.Supliers.Select());
                   List = new ObservableCollection<Product>((List<Product>)DataProvider.Instance.Products.Select());
               });
           
            AddCommand = new RelayCommand<object>(
            (p) =>
            {
                if (string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(BarCode) || IdUnit == null || IdSuplier == null)
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
              var product = List.Where(x => x.BarCode == BarCode).FirstOrDefault();
              var unit = ListUnit.Where(x => x.Id == IdUnit).FirstOrDefault();
              var suplier = ListSuplier.Where(x => x.Id == IdSuplier).FirstOrDefault();
              if (product != null)
              {
                  MessageBox.Show("Đã có sản phẩm này rồi!");
                  return;
              }
              else
              {
                  List.Add((Product)DataProvider.Instance.Products.Insert(new Product() { DisplayName = DisplayName, BarCode = BarCode, States = States, IdUnit = IdUnit, IdSuplier = IdSuplier, Unit = unit, Suplier = suplier}));
              }
          }
          );

            EditCommand = new RelayCommand<object>(
            (p) =>
            {
                if (SelectedItem == null || string.IsNullOrEmpty(DisplayName) || string.IsNullOrEmpty(BarCode) || IdUnit == null || IdSuplier == null)
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
              Product product = new Product() { Id = SelectedItem.Id, DisplayName = DisplayName, BarCode = BarCode, States = States, IdUnit = IdUnit, IdSuplier = IdSuplier };
              DataProvider.Instance.Products.Update(product);
              SelectedItem.DisplayName = product.DisplayName;
              SelectedItem.BarCode = product.BarCode;
              SelectedItem.IdUnit = product.IdUnit;
              SelectedItem.Unit = SelectedUnit;
              SelectedItem.IdSuplier = product.IdSuplier;
              SelectedItem.Suplier = SelectedSuplier;
              SelectedItem.States = product.States;
          }
          );
        }
    }
}

