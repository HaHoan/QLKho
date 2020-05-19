//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace QLKho.Databases.Entity_FW
{
    using QLKho.ViewModel;
    using System;
    using System.Collections.Generic;
    
    public partial class OutputInfo : BaseViewModel
    {
        public int Id { get; set; }

        private int idInputInfo;
        public int IdInputInfo { get { return idInputInfo; } set { idInputInfo = value; OnPropertyChanged(); } }

        private int idOutput;
        public int IdOutput { get { return idOutput; } set { idOutput = value; OnPropertyChanged(); } }

        private Nullable<double> outputPrice;
        public Nullable<double> OutputPrice { get { return outputPrice; } set { outputPrice = value; OnPropertyChanged(); } }


        private Nullable<int> count;
        public Nullable<int> Count { get { return count; } set { count = value; OnPropertyChanged(); } }
        private Nullable<int> idCustomer;
        public Nullable<int> IdCustomer { get { return idCustomer; } set { idCustomer = value; OnPropertyChanged(); } }
        private string states;
        public string States { get { return states; } set { states = value; OnPropertyChanged(); } }

        private Customer customer;
        public Customer Customer { get { return customer; } set { customer = value; OnPropertyChanged(); } }

        private InputInfo inputInfo;
        public InputInfo InputInfo { get { return inputInfo; } set { inputInfo = value; OnPropertyChanged(); } }

        private Output output;
        public Output Output { get { return output; } set { output = value; OnPropertyChanged(); } }
    }
}
