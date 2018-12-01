using Microsoft.EntityFrameworkCore;
using SysBibilioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Services
{
    public interface IDevolucionService
    {
        IEnumerable<Prestamo> GetPrestamos();
        int Create(Devolucion model);        
        Devolucion Find(int DevolucionId);
        bool Delete(int DevolucionId);
        IEnumerable<Libro> Search(string search);
        IEnumerable<Lector> GetLectores();
        Libro FindLibro(int LibroId);
        Prestamo FindPrestado(int PrestamoId);
    }

    public class DevolucionService : IDevolucionService
    {
        private readonly BibliotecaDbContext _context;

        public DevolucionService(BibliotecaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Prestamo> GetPrestamos()
        {
            var list = new List<Prestamo>();

            try
            {
                list = _context.Prestamos.Include(l => l.Libro).Include(l => l.Lector).Include(x => x.StatusLibro).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public int Create(Devolucion model)
        {
            try
            {
                _context.Entry(model).State = EntityState.Added;

                var libro = FindLibro(model.LibroId);
                int ejemplaresAnterior = libro.Ejemplares;

                libro.Ejemplares = ejemplaresAnterior + model.Cantidad;
                _context.Entry(libro).State = EntityState.Modified;

                var prestamo = FindPrestado(model.PrestamoId);
                prestamo.StatusLibroId = 2;
                _context.Entry(prestamo).State = EntityState.Modified;

                _context.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return model.DevolucionId;
        }       

        public Devolucion Find(int DevolucionId)
        {
            var result = new Devolucion();
            try
            {
                result = _context.Devoluciones.Include(x => x.Libro).Single(x => x.DevolucionId == DevolucionId);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool Delete(int DevolucionId)
        {
            var model = new Devolucion();
            try
            {
                model = Find(DevolucionId);
                _context.Entry(model).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        public IEnumerable<Libro> Search(string search)
        {            
            var result = new List<Libro>();
            try
            {
                result = _context.Libros
                                        .Include(l => l.Autor).Include(l => l.Categoria).Include(l => l.Editorial).Include(l => l.Ubicacion)
                                        .Where(x => x.Titulo.ToLower().Contains(search.ToLower()) || x.Descripcion.ToLower().Contains(search.ToLower()))
                                        .ToList();
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public IEnumerable<Lector> GetLectores()
        {
            var list = new List<Lector>();

            try
            {
                list = _context.Lectores.ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public Libro FindLibro(int LibroId)
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

        public Prestamo FindPrestado(int PrestamoId)
        {
            var result = new Prestamo();
            try
            {
                result = _context.Prestamos.Single(x => x.PrestamoId == PrestamoId);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }
    }
}
