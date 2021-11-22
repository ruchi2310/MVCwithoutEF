using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace MVCwithoutEF.Models
{
    public class BookViewModel
    {      
        
        [Key]
        public int Bookid { get; set; }
        [Required]    
        public string Title { get; set; }
        [Required]
        public int Author { get; set; }
        [Range(1,int.MaxValue,ErrorMessage ="Should be greater than or equal 1 ")]
        public int Price { get; set; }
    }
}
