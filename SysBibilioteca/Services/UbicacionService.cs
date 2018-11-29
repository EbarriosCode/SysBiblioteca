using Microsoft.EntityFrameworkCore;
using SysBibilioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SysBibilioteca.Services
{
    public interface IUbicacionService
    {
        IEnumerable<Ubicacion> GetList();
        bool Create(Ubicacion model);
        bool Edit(Ubicacion model);
        Ubicacion Find(int UbicacionId);
        bool Delete(int UbicacionId);
    }

    public class UbicacionService : IUbicacionService
    {
        private readonly BibliotecaDbContext _context;

        public UbicacionService(BibliotecaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Ubicacion> GetList()
        {
            var list = new List<Ubicacion>();

            try
            {
                list = _context.Ubicaciones.ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public bool Create(Ubicacion model)
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

        public bool Edit(Ubicacion model)
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

        public Ubicacion Find(int UbicacionId)
        {
            var result = new Ubicacion();
            try
            {
                result = _context.Ubicaciones.Single(x => x.UbicacionId == UbicacionId);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool Delete(int UbicacionId)
        {
            var model = new Ubicacion();
            try
            {
                model = Find(UbicacionId);
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
