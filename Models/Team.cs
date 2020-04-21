using System;
using System.ComponentModel;  
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GTMS.Models

{
    public class Team
    {        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int TeamID {get; set;}

        [Key]
        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string TeamName {get; set;}

        [Display(Name = "Entrenador")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Trainer {get; set;}

        public List<Player> Players {get; set;}
        public Team () {}

    }
}