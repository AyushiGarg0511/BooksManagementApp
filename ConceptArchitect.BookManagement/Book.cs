using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class Book
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string title { get; set; }
        [Required]
        public string desc { get; set; }
        [Required]
        public string authorid { get; set; }
        public string? authorname { get; set; }
        public string? coverphoto { get; set; }

        public override string ToString()
        {
            return $"Author[Id={Id} , Book-Name={title} ]";
        }
    }
}
