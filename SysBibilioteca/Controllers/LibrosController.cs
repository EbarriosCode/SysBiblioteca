using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SysBibilioteca.Models;
using SysBibilioteca.Services;

namespace SysBibilioteca.Controllers
{
    public class LibrosController : Controller
    {
        private readonly BibliotecaDbContext _context;
        private readonly ILibroService _service;

        public LibrosController(BibliotecaDbContext context, ILibroService service)
        {
            _context = context;
            _service = service;
        }

        // GET: Libros
        public IActionResult Index()
        {            
            return View(_service.GetList());
        }

        [HttpGet]
        public IActionResult Consulta()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ConsultaAjax(string Search)
        {
            var libro = _service.Search(Search);
            return PartialView("_ResultadoConsultaPartial",libro);
        }

        // GET: Libros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .Include(l => l.Editorial)
                .Include(l => l.Ubicacion)
                .FirstOrDefaultAsync(m => m.LibroId == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // GET: Libros/Create
        public IActionResult Create()
        {
            ViewData["AutorId"] = new SelectList(_context.Autores, "AutorId", "Nombre");
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nombre");
            ViewData["EditorialId"] = new SelectList(_context.Editoriales, "EditorialId", "Nombre");
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "UbicacionId", "Nombre");
            return View();
        }

        // POST: Libros/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LibroId,Codigo,Titulo,Paginas,Descripcion,Ejemplares,AutorId,CategoriaId,EditorialId,UbicacionId")] Libro libro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(libro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Autores, "AutorId", "Nombre", libro.AutorId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nombre", libro.CategoriaId);
            ViewData["EditorialId"] = new SelectList(_context.Editoriales, "EditorialId", "Nombre", libro.EditorialId);
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "UbicacionId", "Nombre", libro.UbicacionId);
            return View(libro);
        }

        // GET: Libros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }
            ViewData["AutorId"] = new SelectList(_context.Autores, "AutorId", "Nombre", libro.AutorId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nombre", libro.CategoriaId);
            ViewData["EditorialId"] = new SelectList(_context.Editoriales, "EditorialId", "Nombre", libro.EditorialId);
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "UbicacionId", "Nombre", libro.UbicacionId);
            return View(libro);
        }

        // POST: Libros/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LibroId,Codigo,Titulo,Paginas,Descripcion,Ejemplares,AutorId,CategoriaId,EditorialId,UbicacionId")] Libro libro)
        {
            if (id != libro.LibroId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibroExists(libro.LibroId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["AutorId"] = new SelectList(_context.Autores, "AutorId", "Nombre", libro.AutorId);
            ViewData["CategoriaId"] = new SelectList(_context.Categorias, "CategoriaId", "Nombre", libro.CategoriaId);
            ViewData["EditorialId"] = new SelectList(_context.Editoriales, "EditorialId", "Nombre", libro.EditorialId);
            ViewData["UbicacionId"] = new SelectList(_context.Ubicaciones, "UbicacionId", "Nombre", libro.UbicacionId);
            return View(libro);
        }

        // GET: Libros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .Include(l => l.Editorial)
                .Include(l => l.Ubicacion)
                .FirstOrDefaultAsync(m => m.LibroId == id);
            if (libro == null)
            {
                return NotFound();
            }

            return View(libro);
        }

        // POST: Libros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libro = await _context.Libros.FindAsync(id);
            _context.Libros.Remove(libro);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibroExists(int id)
        {
            return _context.Libros.Any(e => e.LibroId == id);
        }
    }
}
