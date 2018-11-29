using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Models
{
    [Table("Donacion")]
    public class Donacion
    {
        [Key]
        public int DonacionId { get; set; }

        public int Cantidad { get; set; }

        public int LibroId { get; set; }
        public Libro Libro { get; set; }

        public int DonadorId { get; set; }
        public Donador Donador { get; set; }
    }
}
