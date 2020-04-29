using System;
using System.ComponentModel;  
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GTMS.Models

{
    public class Message
    {
        
        [Key]
        public string msgID {get; set;}

        [Display(Name = "Message")]
        public string Description {get; set; }

    }
}