using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Models
{
    [Table("Ubicacion")]
    public class Ubicacion    
    {
        [Key]
        public int UbicacionId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido")]
        [StringLength(50, ErrorMessage = "La longitud válida para este campo es de {0} caracteres")]
        public string Nombre { get; set; }

        public IEnumerable<Libro> Libros { get; set; }
    }
}
