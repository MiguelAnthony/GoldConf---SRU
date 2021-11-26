using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldConf.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string NomApe { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"[a-zA-Z0-9]+")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [DataType(DataType.Password)]
        [MinLength(6)]
        public string Password { get; set; }
        public List<Comprar> Compras { get; set; }
    }
}
