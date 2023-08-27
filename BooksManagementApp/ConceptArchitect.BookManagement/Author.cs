using DataAnnotationsExtensions;
using System.ComponentModel.DataAnnotations;

namespace ConceptArchitect.BookManagement
{
    public class Author
    {
        [Required]
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        [StringLength(500, MinimumLength =5)]
        public string Biography { get; set; }
        public string? Photo { get; set; }
        [Email]
        public string? Email { get; set; }

        public DateTime? BirthDate { get; set; }

        public override string ToString()
        {
            return $"Author[Id={Id} , Name={Name} ]";
        }

    }
}