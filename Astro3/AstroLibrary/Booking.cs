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
        public int Booking_ID { get; set; }
        public System.DateTime Creation_Date { get; set; }
        public int User_ID { get; set; }
        public System.DateTime Last_Update_Date { get; set; }
        public string Current_Status { get; set; }
        public string Created_By { get; set; }
        public string Updated_By { get; set; }
        public System.DateTime Slot_Date { get; set; }
        public int Slot_time { get; set; }
        public Nullable<int> Frequency { get; set; }
        public string days { get; set; }
    
        public virtual User User { get; set; }
    }
}
