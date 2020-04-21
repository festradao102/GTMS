using System;
using System.ComponentModel;  
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTMS.Models

{
    public class Player
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PlayerID {get; set;}

        [Display(Name = "Identificacion")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int identification {get; set; }

        [ForeignKey("teamName")]
        [Required(ErrorMessage = "Este campo es requerido")]
        [Display(Name = "Equipo")]
        public string team {get; set;}

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string name {get; set;}

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string lastName {get; set;}

        [Display(Name = "Edad")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int age {get; set;}

        [Display(Name = "Altura")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public float height {get; set;}

        [Display(Name = "Peso")]
        [Required(ErrorMessage = "Este campo es requerido")]      
        public float weight {get; set;}
        
        [Display(Name = "Posición")]        
        [Required(ErrorMessage = "Este campo es requerido")]
        public string position {get; set;}      

    }
}