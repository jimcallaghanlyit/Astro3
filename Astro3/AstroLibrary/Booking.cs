//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AstroLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class Booking
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Booking()
        {
            this.Booking_Slot = new HashSet<Booking_Slot>();
            this.Booking_Status_History = new HashSet<Booking_Status_History>();
        }
    
        public int Booking_ID { get; set; }
        public System.DateTime Creation_Date { get; set; }
        public string Slot_Time { get; set; }
        public int User_ID { get; set; }
        public System.DateTime Last_Update_Date { get; set; }
        public string Current_Status { get; set; }
        public string Created_By { get; set; }
        public string Updated_By { get; set; }
        public string Booking_Type { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking_Slot> Booking_Slot { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking_Status_History> Booking_Status_History { get; set; }
        public virtual User User { get; set; }
    }
}
