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
    
    public partial class Log
    {
        public int Log_ID { get; set; }
        public int User_ID { get; set; }
        public string Category { get; set; }
        public string Description { get; set; }
        public System.DateTime Date { get; set; }
    
        public virtual User User { get; set; }
    }
}