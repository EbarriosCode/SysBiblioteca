using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Models
{
    [Table("Sexo")]
    public class Sexo
    {
        [Key]
        public int SexoId { get; set; }

        public string Nombre { get; set; }

        public IEnumerable<Lector> Lector { get; set; }
    }
}
