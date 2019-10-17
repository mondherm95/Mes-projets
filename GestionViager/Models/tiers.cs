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

    public partial class tiers
    {
        public int id { get; set; }
        [Required]
        [DisplayName("Nom ")]
        public string lastname { get; set; }
        [Required]
        [DisplayName("Prenom ")]
        public string firstname { get; set; }
        [DisplayName("Age ")]
        public Nullable<int> age { get; set; }
        [Required]
        [DisplayName("Telephone ")]
        public Nullable<long> phone { get; set; }
        [Required]
        [DisplayName("Date de naissance")]
        public Nullable<System.DateTime> birth_date { get; set; }
        public Nullable<System.DateTime> confirmation_date { get; set; }
        public Nullable<bool> credirentier { get; set; }
        public Nullable<System.DateTime> death_date { get; set; }
        public Nullable<double> esperance_vie_initiale { get; set; }
        [Required]
        [DisplayName("Sexe")]
        public string gender { get; set; }
        [Required]
        [DisplayName("Telephone2")]
        public Nullable<long> phone2 { get; set; }
        public Nullable<double> esperance_vie_actuelle { get; set; }
        public Nullable<long> id_folder { get; set; }
    
        public virtual folder folder { get; set; }
    }
}
