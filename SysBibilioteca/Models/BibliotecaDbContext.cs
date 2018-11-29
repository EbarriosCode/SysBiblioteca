using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Models
{
    public class BibliotecaDbContext : DbContext
    {
        public BibliotecaDbContext(DbContextOptions<BibliotecaDbContext> options)
            : base(options)
        {}

        public DbSet<Autor> Autores { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Editorial> Editoriales { get; set; }
        public DbSet<Ubicacion> Ubicaciones { get; set; }
        public DbSet<Libro> Libros { get; set; }
        public DbSet<Donacion> Donaciones { get; set; }
        public DbSet<Donador> Donadores { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
        public DbSet<Sexo> Sexos { get; set; }
        public DbSet<Lector> Lectores { get; set; }
    }
}
