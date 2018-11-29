using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SysBibilioteca.Models;
using SysBibilioteca.Services;

namespace SysBibilioteca.Controllers
{
    public class DonacionesController : Controller
    {
        private readonly IDonacionService _service;

        public DonacionesController(IDonacionService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {           
            ViewData["LibroId"] = new SelectList(_service.GetLibros(), "LibroId", "Titulo");
            ViewData["DonadorId"] = new SelectList(_service.GetDonadores(), "DonadorId", "Nombre");

            return View(_service.GetList());
        }

        [HttpPost]
        public IActionResult Create(int Cantidad, int LibroId, int DonadorId)
        {
            if (Cantidad > 0 && LibroId > 0 && DonadorId > 0)
            {
                var persistencia = new Donacion()
                {
                    Cantidad = Cantidad,
                    LibroId = LibroId,
                    DonadorId = DonadorId
                };

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