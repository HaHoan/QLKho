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
    public class UnitViewModel : BaseViewModel
    {
        private ObservableCollection<Unit> _List;
        public ObservableCollection<Unit> List {
            get => _List;
            set
            {
                _List = value;
                OnPropertyChanged();
            }
        }

        private Unit _SelectedItem;
        public Unit SelectedItem
        {
            get => _SelectedItem;
            set
            {
                _SelectedItem = value;
                OnPropertyChanged();
                if (SelectedItem != null)
                {
                    DisplayName = SelectedItem.DisplayName;
                }
            }
        }

        private string _DisplayName;
        public string DisplayName { get => _DisplayName; set { _DisplayName = value; OnPropertyChanged(); } }
        public ICommand AddUnitCommand { get; set; }
        public ICommand EditUnitCommand { get; set; }
        public ICommand Loaded { get; set; }

        public UnitViewModel()
        {
            Loaded = new RelayCommand<object>(
               (p) => { return true; },
               (p) =>
               {
                   List = new ObservableCollection<Unit>((List<Unit>)DataProvider.Instance.Units.Select());

               });
            AddUnitCommand = new RelayCommand<object>(
              (p) =>
            {
                if (string.IsNullOrEmpty(DisplayName))
                {
                    return false;
                } else
                {
                    return true;
                }
            },
            (p) =>
            {
                var unit = List.Where(x => x.DisplayName == DisplayName).FirstOrDefault();
                if (unit != null)
                {
                    MessageBox.Show("Đã có tên đơn vị này rồi. Hãy nhập tên khác!");
                } else
                {
                    List.Add((Unit)DataProvider.Instance.Units.Insert(new Unit() { DisplayName = DisplayName }));
                }
            }
            );
            EditUnitCommand = new RelayCommand<object>(
             (p) =>
             {
                 if (SelectedItem == null || string.IsNullOrEmpty(DisplayName))
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
               var unit = List.Where(x => x.DisplayName == DisplayName).FirstOrDefault();
               if (unit != null)
               {
                   MessageBox.Show("Đã có tên đơn vị này rồi. Hãy nhập tên khác!");
               }
               else
               {
                  
                   DataProvider.Instance.Units.Update(new Unit() { Id = SelectedItem.Id, DisplayName = DisplayName });
                   SelectedItem.DisplayName = DisplayName;
               }
           }
           );
        }
    }
}
