using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConceptArchitect.BookManagement
{
    public class User
    {
        [Required]
        public string email { get; set; }
        [Required]
        public string password { get; set; }
        public string? name { get; set; }
        public string? Photo { get; set; }

        public DateTime? BirthDate { get; set; }

        public override string ToString()
        {
            return $"User[Id={email} , Name={name} ]";
        }
    }
}
