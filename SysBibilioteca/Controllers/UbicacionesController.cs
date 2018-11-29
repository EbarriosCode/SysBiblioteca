using Microsoft.AspNetCore.Mvc;
using SysBibilioteca.Models;
using SysBibilioteca.Services;
using System;

namespace SysBibilioteca.Controllers
{
    public class UbicacionesController : Controller
    {
        private readonly IUbicacionService _service;

        public UbicacionesController(IUbicacionService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetList());
        }

        [HttpPost]
        public IActionResult Create(string ubicacion)
        {
            if(!string.IsNullOrEmpty(ubicacion))
            {
                var persistencia = new Ubicacion() { Nombre = ubicacion };
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
        public IActionResult Edit(int idEdit, string ubicacionEdit)
        {
            if(idEdit > 0 && !string.IsNullOrEmpty(ubicacionEdit))
            {
                var modified = new Ubicacion()
                {
                    UbicacionId = idEdit,
                    Nombre = ubicacionEdit
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