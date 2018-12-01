using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Models
{
    [Table("Libro")]
    public class Libro
    {
        [Key]
        public int LibroId { get; set; }

        public string Codigo { get; set; }
        public string Titulo { get; set; }
        public int Paginas { get; set; }
        public string Descripcion { get; set; }
        public int Ejemplares { get; set; }

        public int AutorId { get; set; }
        public Autor Autor { get; set; }

        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        public int EditorialId { get; set; }
        public Editorial Editorial { get; set; }

        public int UbicacionId { get; set; }
        public Ubicacion Ubicacion { get; set; }

        public IEnumerable<Prestamo> Prestamos { get; set; }
        public IEnumerable<Devolucion> Devoluciones { get; set; }
    }
}
