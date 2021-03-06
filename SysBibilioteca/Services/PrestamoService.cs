﻿using Microsoft.EntityFrameworkCore;
using SysBibilioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Services
{
    public interface IPrestamoService
    {
        IEnumerable<Prestamo> GetList();
        int Create(Prestamo model);
        Prestamo Find(int PrestamoId);
        bool Delete(int PrestamoId);
        IEnumerable<Libro> Search(string search);
        IEnumerable<Lector> GetLectores();
        Libro FindLibro(int LibroId);
    }

    public class PrestamoService : IPrestamoService
    {
        private readonly BibliotecaDbContext _context;

        public PrestamoService(BibliotecaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Prestamo> GetList()
        {
            var list = new List<Prestamo>();

            try
            {
                list = _context.Prestamos.Include(l => l.Libro).Include(l => l.Lector).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public int Create(Prestamo model)
        {
            try
            {
                _context.Entry(model).State = EntityState.Added;

                var libro = FindLibro(model.LibroId);
                int ejemplaresAnterior = libro.Ejemplares;

                libro.Ejemplares = ejemplaresAnterior - model.Cantidad;

                _context.Entry(libro).State = EntityState.Modified;
                _context.SaveChanges();

            }
            catch (Exception)
            {
                throw;
            }
            return model.PrestamoId;
        }       

        public Prestamo Find(int PrestamoId)
        {
            var result = new Prestamo();
            try
            {
                result = _context.Prestamos.Include(x => x.Libro).Include(y => y.Lector).Single(x => x.PrestamoId == PrestamoId);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool Delete(int PrestamoId)
        {
            var model = new Prestamo();
            try
            {
                model = Find(PrestamoId);
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
    }
}
