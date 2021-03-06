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

    public partial class Product : BaseViewModel
    {
        private string displayName;
        private string barCode;
        private int? idUnit;
        private int? idSuplier;
        private string states;
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            this.InputInfoes = new HashSet<InputInfo>();
        }

        public int Id { get; set; }
        public string DisplayName { get { return displayName; } set { displayName = value; OnPropertyChanged(); } }
        public string BarCode { get { return barCode; } set { barCode = value; OnPropertyChanged(); } }
        public Nullable<int> IdUnit { get { return idUnit; } set { idUnit = value; OnPropertyChanged(); } }
        public Nullable<int> IdSuplier { get { return idSuplier; } set { idSuplier = value; OnPropertyChanged(); } }
        public string States { get { return states; } set { states = value; OnPropertyChanged(); } }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<InputInfo> InputInfoes { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        private Suplier suplier;
        public virtual Suplier Suplier { get { return suplier; } set { suplier = value; OnPropertyChanged(); } }

        private Unit unit;
        public virtual Unit Unit { get { return unit; } set { unit = value; OnPropertyChanged(); } }
    }
}
