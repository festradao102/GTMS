using System;
using System.ComponentModel;  
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTMS.Models

{
    public class ConfigValues
    {
        
        [Key]
        public string ValueID {get; set;}

        [Display(Name = "Posición")]
        public string Description {get; set; }

    }
}