using Microsoft.EntityFrameworkCore;
using SysBibilioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Services
{
    public interface ILectorService
    {
        IEnumerable<Lector> GetList();
        bool Create(Lector model);
        bool Edit(Lector model);
        Lector Find(int LectorId);
        bool Delete(int LectorId);

        // Listas para los combos modal create
        IEnumerable<Cargo> GetCargos();
        IEnumerable<Sexo> GetSexos();
    }

    public class LectorService : ILectorService
    {
        private readonly BibliotecaDbContext _context;

        public LectorService(BibliotecaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lector> GetList()
        {
            var list = new List<Lector>();

            try
            {
                list = _context.Lectores.Include(x => x.Cargo).Include(y => y.Sexo).ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public bool Create(Lector model)
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

        public bool Edit(Lector model)
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

        public Lector Find(int LectorId)
        {
            var result = new Lector();
            try
            {
                result = _context.Lectores.Single(x => x.LectorId == LectorId);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool Delete(int LectorId)
        {
            var model = new Lector();
            try
            {
                model = Find(LectorId);
                _context.Entry(model).State = EntityState.Deleted;
                _context.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        // Listas para los combos Create
        public IEnumerable<Cargo> GetCargos()
        {
            var list = new List<Cargo>();

            try
            {
                list = _context.Cargos.ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public IEnumerable<Sexo> GetSexos()
        {
            var list = new List<Sexo>();

            try
            {
                list = _context.Sexos.ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }
    }
}
