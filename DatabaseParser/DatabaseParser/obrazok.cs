//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DatabaseParser
{
    using System;
    using System.Collections.Generic;
    
    public partial class obrazok
    {
        public obrazok()
        {
            this.akcia = new HashSet<akcia>();
            this.denne_menu = new HashSet<denne_menu>();
            this.menu = new HashSet<menu>();
        }
    
        public int id_obrazka { get; set; }
        public string metadata { get; set; }
    
        public virtual ICollection<akcia> akcia { get; set; }
        public virtual ICollection<denne_menu> denne_menu { get; set; }
        public virtual ICollection<menu> menu { get; set; }
    }
}