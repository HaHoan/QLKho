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
    using System;
    using System.Collections.Generic;
    
    public partial class InputInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public InputInfo()
        {
            this.OutputInfoes = new HashSet<OutputInfo>();
        }
    
        public int Id { get; set; }
        public int IdProduct { get; set; }
        public int IdInput { get; set; }
        public Nullable<int> Count { get; set; }
        public Nullable<double> InputPrice { get; set; }
        public Nullable<double> OutputPrice { get; set; }
        public string States { get; set; }
    
        public virtual Input Input { get; set; }
        public virtual Product Product { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<OutputInfo> OutputInfoes { get; set; }
    }
}