using System;
using System.ComponentModel;  
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTMS.Models

{
    public class Referee
    {
        
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int RefereeID {get; set;}

        [Display(Name = "Identificacion")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Identification {get; set; }

        [Display(Name = "Nombre")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Name {get; set;}

        [Display(Name = "Apellido")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public string LastName {get; set;}

        [Display(Name = "Edad")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int Age {get; set;}

        [Display(Name = "Altura")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public float Height {get; set;}

        [Display(Name = "Peso")]
        [Required(ErrorMessage = "Este campo es requerido")]      
        public float Weight {get; set;}
        
        [Display(Name = "Posición")]        
        [Required(ErrorMessage = "Este campo es requerido")]
        public string Position {get; set;}      

    }
}