using Microsoft.EntityFrameworkCore;
using SysBibilioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Services
{
    public interface IAutoresService
    {
        IEnumerable<Autor> GetList();
        bool Create(Autor model);
        bool Edit(Autor model);
        Autor FindBautismo(int AutorId);
        bool Delete(int AutorId);
    }

    public class AutoresService : IAutoresService
    {
        private readonly BibliotecaDbContext _context;

        public AutoresService(BibliotecaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Autor> GetList()
        {
            var autores = new List<Autor>();

            try
            {
                autores = _context.Autores.ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return autores;
        }

        public bool Create(Autor model)
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

        public bool Edit(Autor model)
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

        public Autor FindBautismo(int AutorId)
        {
            var result = new Autor();
            try
            {
                result = _context.Autores.Single(x => x.AutorId == AutorId);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool Delete(int AutorId)
        {
            var model = new Autor();
            try
            {
                model = FindBautismo(AutorId);
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
