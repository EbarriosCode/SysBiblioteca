using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SysBibilioteca.Models;
using SysBibilioteca.Services;

namespace SysBibilioteca.Controllers
{
    public class DevolucionesController : Controller
    {
        private readonly IDevolucionService _service;

        public DevolucionesController(IDevolucionService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetPrestamos());
        }

        [HttpPost]
        public IActionResult Devolver(int PrestamoIdHidden, int CantidadHidden, int LibroIdHidden)
        {
            if(PrestamoIdHidden > 0 && CantidadHidden > 0 && LibroIdHidden > 0)
            {
                var persistencia = new Devolucion()
                {
                    Cantidad = CantidadHidden,
                    LibroId = LibroIdHidden,
                    PrestamoId = PrestamoIdHidden
                };

                int devolucionId = _service.Create(persistencia);
                TempData["devuelto"] = $"Devolucion Efectuada Correctamente";
            }
            return RedirectToAction("Index");
        }
    }
}