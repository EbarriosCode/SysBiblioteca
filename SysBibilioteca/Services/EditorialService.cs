using Microsoft.EntityFrameworkCore;
using SysBibilioteca.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SysBibilioteca.Services
{
    public interface IEditorialService
    {
        IEnumerable<Editorial> GetList();
        bool Create(Editorial model);
        bool Edit(Editorial model);
        Editorial Find(int EditorialId);
        bool Delete(int EditorialId);
    }

    public class EditorialService : IEditorialService
    {
        private readonly BibliotecaDbContext _context;

        public EditorialService(BibliotecaDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Editorial> GetList()
        {
            var list = new List<Editorial>();

            try
            {
                list = _context.Editoriales.ToList();
            }
            catch (Exception)
            {
                throw;
            }

            return list;
        }

        public bool Create(Editorial model)
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

        public bool Edit(Editorial model)
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

        public Editorial Find(int EditorialId)
        {
            var result = new Editorial();
            try
            {
                result = _context.Editoriales.Single(x => x.EditorialId == EditorialId);
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool Delete(int EditorialId)
        {
            var model = new Editorial();
            try
            {
                model = Find(EditorialId);
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
