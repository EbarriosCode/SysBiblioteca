using Microsoft.EntityFrameworkCore;
using SysBibilioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Services
{
    public interface IDonadorService
    {
        IEnumerable<Donador> GetList();
        bool Create(Donador model);
        bool Edit(Donador model);
        Donador Find(int DonadorId);
        bool Delete(int DonadorId);
    }

    public class DonadorService : IDonadorService
    {
        private readonly BibliotecaDbContext _context;

        public DonadorService(BibliotecaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Donador> GetList()
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

        public bool Create(Donador model)
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

        public bool Edit(Donador model)
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

        public Donador Find(int DonadorId)
        {
            var result = new Donador();
            try
            {
                result = _context.Donadores.Single(x => x.DonadorId == DonadorId);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool Delete(int DonadorId)
        {
            var model = new Donador();
            try
            {
                model = Find(DonadorId);
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
