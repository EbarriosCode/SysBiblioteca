using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Models
{
    [Table("Lector")]
    public class Lector
    {
        [Key]
        public int LectorId { get; set; }

        public string Carne { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Direccion { get; set; }

        public int CargoId { get; set; }
        public Cargo Cargo { get; set; }

        public int SexoId { get; set; }
        public Sexo Sexo { get; set; }

        public IEnumerable<Prestamo> Prestamos { get; set; }
    }
}
