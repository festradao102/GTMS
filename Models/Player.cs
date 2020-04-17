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
        public int uniqueID {get; set;}

        [Display(Name = "Identificacion")]
        [Required(ErrorMessage = "Este campo es requerido")]
        public int id {get; set; }

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
         
        public Player () {}

        public Player (int pid, string pname, string plastName, int page, float pheight, float pweight, string pposition)
        {
            this.id = pid;
            this.name = pname;
            this.lastName = plastName;
            this.age = page;
            this.height = pheight;
            this.weight = pweight;
            this.position = pposition;
        }

    }
}