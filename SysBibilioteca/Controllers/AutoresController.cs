using Microsoft.AspNetCore.Mvc;
using SysBibilioteca.Models;
using SysBibilioteca.Services;
using System;

namespace SysBibilioteca.Controllers
{
    public class AutoresController : Controller
    {
        private readonly IAutoresService _service;

        public AutoresController(IAutoresService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetList());
        }

        [HttpPost]
        public IActionResult Create(string autor)
        {
            if(!string.IsNullOrEmpty(autor))
            {
                var persistencia = new Autor() { Nombre = autor };
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
        public IActionResult Edit(int idEdit, string autorEdit)
        {
            if(idEdit > 0 && !string.IsNullOrEmpty(autorEdit))
            {
                var modified = new Autor()
                {
                    AutorId = idEdit,
                    Nombre = autorEdit
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