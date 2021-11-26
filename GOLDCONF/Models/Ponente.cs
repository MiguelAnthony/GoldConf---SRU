using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GoldConf.Models
{
    public class Ponente
    {
        public int Id { get; set; }
        public string Imagen { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string NomApe { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Especialidad { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [EmailAddress]
        public string Email { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        [RegularExpression(@"[0-9]+")]
        [MinLength(7)]
        public string Telefono { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Logros { get; set; }
        [Required(ErrorMessage = "Este campo es obligatorio")]
        public string Experiencia { get; set; }
    }
}
