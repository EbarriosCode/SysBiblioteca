using Microsoft.AspNetCore.Mvc;
using SysBibilioteca.Models;
using SysBibilioteca.Services;
using System;

namespace SysBibilioteca.Controllers
{
    public class EditorialesController : Controller
    {
        private readonly IEditorialService _service;

        public EditorialesController(IEditorialService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetList());
        }

        [HttpPost]
        public IActionResult Create(string editorial)
        {
            if(!string.IsNullOrEmpty(editorial))
            {
                var persistencia = new Editorial() { Nombre = editorial };
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
        public IActionResult Edit(int idEdit, string editorialEdit)
        {
            if(idEdit > 0 && !string.IsNullOrEmpty(editorialEdit))
            {
                var modified = new Editorial()
                {
                    EditorialId = idEdit,
                    Nombre = editorialEdit
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