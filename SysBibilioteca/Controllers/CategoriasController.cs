using Microsoft.AspNetCore.Mvc;
using SysBibilioteca.Models;
using SysBibilioteca.Services;
using System;

namespace SysBibilioteca.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly ICategoriaService _service;

        public CategoriasController(ICategoriaService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetList());
        }

        [HttpPost]
        public IActionResult Create(string categoria)
        {
            if(!string.IsNullOrEmpty(categoria))
            {
                var persistencia = new Categoria() { Nombre = categoria };
                _service.Create(persistencia);

                TempData["created"] = "Registro Creado Correctamente";
                return RedirectToAction("Index");
            }

            else
            {
                TempData["created"] = "Error";
                return RedirectToAction("Index");
            }            
        }

       [HttpPost]
        public IActionResult Edit(int idEdit, string categoriaEdit)
        {
            if(idEdit > 0 && !string.IsNullOrEmpty(categoriaEdit))
            {
                var modified = new Categoria()
                {
                    CategoriaId = idEdit,
                    Nombre = categoriaEdit
                };

                _service.Edit(modified);

                TempData["edited"] = "Registro Editado Correctamente";
            }

            return RedirectToAction("Index");
        }
        
        public IActionResult Delete(int id)
        {            
            if (id < 1)
            {
                return NotFound();
            }
         
            _service.Delete(id);
            return RedirectToAction("Index");
        }
    }
}