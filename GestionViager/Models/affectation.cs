//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GestionViager.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class affectation
    {
        [Required]

        public Nullable<double> value { get; set; }
        public Nullable<int> fonds { get; set; }
        public Nullable<int> propriete { get; set; }
        public int id { get; set; }
    
        public virtual fonds fonds1 { get; set; }
        public virtual Propriete Propriete1 { get; set; }
    }
}