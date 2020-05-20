using QLKho.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLKho.Model
{
    public class InventoryModel : BaseViewModel
    {
        public string BarCode { get; set; }
        public string DisplayName { get; set; }

        public int TotalInput { get; set; }

        private int totalOutput;
        public int TotalOutput
        {
            get => totalOutput;
            set
            {
                totalOutput = value;
                Inventory = TotalInput - TotalOutput;
                ;
            }
        }
        public int Inventory { get; set; }

    }
}
