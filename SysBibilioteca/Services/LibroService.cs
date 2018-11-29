using Microsoft.EntityFrameworkCore;
using SysBibilioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Services
{
    public interface ILibroService
    {
        IEnumerable<Libro> GetList();
        bool Create(Libro model);
        bool Edit(Libro model);
        Libro Find(int LibroId);
        bool Delete(int LibroId);
        Libro Search(string search);
    }

    public class LibroService : ILibroService
    {
        private readonly BibliotecaDbContext _context;

        public LibroService(BibliotecaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Libro> GetList()
        {
            var list = new List<Libro>();

            try
            {
                list = _context.Libros.Include(l => l.Autor).Include(l => l.Categoria).Include(l => l.Editorial).Include(l => l.Ubicacion).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public bool Create(Libro model)
        {
            try
            {
                _context.Entry(model).State = EntityState.Added;
                _context.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public bool Edit(Libro model)
        {
            try
            {
                _context.Entry(model).State = EntityState.Modified;
                _context.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public Libro Find(int LibroId)
        {
            var result = new Libro();
            try
            {
                result = _context.Libros.Single(x => x.LibroId == LibroId);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool Delete(int LibroId)
        {
            var model = new Libro();
            try
            {
                model = Find(LibroId);
                _context.Entry(model).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public Libro Search(string search)
        {            
            var result = new Libro();
            try
            {
                result = _context.Libros
                                        .Include(l => l.Autor).Include(l => l.Categoria).Include(l => l.Editorial).Include(l => l.Ubicacion)
                                        .Where(x => x.Titulo.ToLower().Contains(search.ToLower()) || x.Descripcion.ToLower().Contains(search.ToLower()) || x.Codigo.Contains(search))
                                        .FirstOrDefault();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
