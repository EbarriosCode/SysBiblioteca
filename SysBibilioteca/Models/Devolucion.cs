using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Models
{
    [Table("Devolucion")]
    public class Devolucion
    {
        [Key]
        public int DevolucionId { get; set; }

        public int Cantidad { get; set; }

        public int LibroId { get; set; }
        public Libro Libro { get; set; }

        public int PrestamoId { get; set; }
        public Prestamo Prestamo { get; set; }
    }
}
