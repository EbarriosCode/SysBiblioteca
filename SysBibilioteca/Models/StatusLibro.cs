using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Models
{
    [Table("StatusLibro")]
    public class StatusLibro
    {
        [Key]
        public int StatusLibroId { get; set; }

        public string Nombre { get; set; }

        public IEnumerable<Prestamo> Prestamos { get; set; }
    }
}
