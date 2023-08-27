using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class review
    {
        [Required]
        public string reviewer_email { get; set; }
        [Required]
        public string book_id { get; set; }
        public int? rating { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string details { get; set; }
    }
}
