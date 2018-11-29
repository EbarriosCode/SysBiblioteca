using Microsoft.EntityFrameworkCore;
using SysBibilioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Services
{
    public interface IDonacionService
    {
        IEnumerable<Donacion> GetList();
        bool Create(Donacion model);        
        Donacion Find(int DonacionId);
        Libro FindLibro(int LibroId);
        bool Delete(int DonacionId);

        // Listas para combos
        IEnumerable<Libro> GetLibros();
        IEnumerable<Donador> GetDonadores();
    }

    public class DonacionService : IDonacionService
    {
        private readonly BibliotecaDbContext _context;

        public DonacionService(BibliotecaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Donacion> GetList()
        {
            var list = new List<Donacion>();

            try
            {
                list = _context.Donaciones.Include(x => x.Libro).Include(y => y.Donador).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public bool Create(Donacion model)
        {
            try
            {                
                _context.Entry(model).State = EntityState.Added;

                var libro = FindLibro(model.LibroId);
                int ejemplaresAnterior = libro.Ejemplares;

                libro.Ejemplares = ejemplaresAnterior + model.Cantidad;

                _context.Entry(libro).State = EntityState.Modified;
                _context.SaveChanges();

            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }        

        public Donacion Find(int DonacionId)
        {
            var result = new Donacion();
            try
            {
                result = _context.Donaciones.Single(x => x.DonacionId == DonacionId);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
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

        public bool Delete(int DonacionId)
        {
            var model = new Donacion();
            try
            {
                model = Find(DonacionId);
                    
                //Buscar el libro para restar ejemplares eliminados
                var libro = FindLibro(model.LibroId);
                int ejemplaresAnterior = libro.Ejemplares;

                libro.Ejemplares = ejemplaresAnterior - model.Cantidad;

                _context.Entry(libro).State = EntityState.Modified;

                _context.Entry(model).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        // Metodos para listas de combos para crear
        public IEnumerable<Libro> GetLibros()
        {
            var list = new List<Libro>();

            try
            {
                list = _context.Libros.ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public IEnumerable<Donador> GetDonadores()
        {
            var list = new List<Donador>();

            try
            {
                list = _context.Donadores.ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }
    }
}
