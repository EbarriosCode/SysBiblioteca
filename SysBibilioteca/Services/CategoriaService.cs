using Microsoft.EntityFrameworkCore;
using SysBibilioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Services
{
    public interface ICategoriaService
    {
        IEnumerable<Categoria> GetList();
        bool Create(Categoria model);
        bool Edit(Categoria model);
        Categoria Find(int CategoriaId);
        bool Delete(int CategoriaId);
    }

    public class CategoriaService : ICategoriaService
    {
        private readonly BibliotecaDbContext _context;

        public CategoriaService(BibliotecaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Categoria> GetList()
        {
            var list = new List<Categoria>();

            try
            {
                list = _context.Categorias.ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public bool Create(Categoria model)
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

        public bool Edit(Categoria model)
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

        public Categoria Find(int CategoriaId)
        {
            var result = new Categoria();
            try
            {
                result = _context.Categorias.Single(x => x.CategoriaId == CategoriaId);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool Delete(int CategoriaId)
        {
            var model = new Categoria();
            try
            {
                model = Find(CategoriaId);
                _context.Entry(model).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
    }
}
