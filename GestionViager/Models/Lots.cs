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
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public partial class Lots
    {
        public int id { get; set; }
        public Nullable<int> id_Propriete { get; set; }
        [Required]
        [DisplayName("Surface ")]
        public Nullable<double> surface { get; set; }
        [Required]
        [DisplayName("Etage ")]
        public Nullable<int> etage { get; set; }
        [Required]
        [DisplayName("Natue du Lot ")]
        public string nature_lot { get; set; }
      
        public bool carrez { get; set; }
        public bool balcon { get; set; }
        public Nullable<double> surface_balcon { get; set; }
        public bool terrasse { get; set; }
        public Nullable<double> surface_terrasse { get; set; }
        public bool jardin { get; set; }
        public Nullable<double> surface_jardin { get; set; }
        public string commentaire { get; set; }
    
        public virtual Propriete Propriete { get; set; }
    }
}
