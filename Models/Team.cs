using System;
using System.ComponentModel;  
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GTMS.Models

{
    public class Team
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int uniqueTeamID {get; set;}

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string teamName {get; set;}

        [Display(Name = "Entrenador")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string trainer {get; set;}

        public List<Player> Players {get; set;}
        public Team () {}

    }
}