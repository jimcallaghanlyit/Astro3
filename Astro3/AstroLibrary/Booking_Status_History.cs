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
    
    public partial class Booking_Status_History
    {
        public int History_ID { get; set; }
        public string Status { get; set; }
        public System.DateTime Creation_Date { get; set; }
        public string Created_By { get; set; }
        public string Comments { get; set; }
        public int Booking_ID { get; set; }
    
        public virtual Booking Booking { get; set; }
    }
}
