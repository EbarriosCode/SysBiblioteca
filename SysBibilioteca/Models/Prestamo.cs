using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Models
{
    [Table("Prestamo")]
    public class Prestamo
    {
        [Key]
        public int PrestamoId { get; set; }

        public string FechaPrestamo { get; set; }        
        public int Cantidad { get; set; }

        public int LibroId { get; set; }
        public Libro Libro { get; set; }

        public int LectorId { get; set; }
        public Lector Lector { get; set; }

        public int StatusLibroId { get; set; }
        public StatusLibro StatusLibro { get; set; }
    }
}
