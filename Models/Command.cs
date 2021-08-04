using System.ComponentModel.DataAnnotations;

namespace API.Models
{
    public class Command 
    {
        public Command()
        {
            
        }
        [Key]
        [Required]
        public int Id { get; set; } 
    
        [Required]
        [MaxLength(250)]
        public string Name { get; set; }    
        
        [Required]
        [MaxLength(250)]
        public string Platform { get; set; }    
        
        [Required]
        public string Description { get; set; }

        public string Link { get; set; }
    }
}