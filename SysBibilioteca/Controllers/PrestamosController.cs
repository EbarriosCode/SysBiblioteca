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
    public class PrestamosController : Controller
    {
        private readonly IPrestamoService _service;

        public PrestamosController(IPrestamoService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult BusquedaLibros(string Search)
        {
            var libro = _service.Search(Search);

            //Cargar lista de lectores para el modal de prestamo
            ViewData["LectorId"] = new SelectList(_service.GetLectores(), "LectorId", "Nombre");

            return PartialView("_LibrosPartial", libro);
        }

        [HttpPost]
        public IActionResult Create(int LibroId, int Cantidad, int LectorId)
        {
            if (LibroId > 0 && Cantidad > 0 && LectorId > 0)
            {
                var persistencia = new Prestamo()
                {
                    FechaPrestamo = DateTime.Now.ToString("dd/MM/yyyy hh:mm tt"),
                    Cantidad = Cantidad,
                    LibroId = LibroId,
                    LectorId = LectorId,
                    StatusLibroId = 1
                };

                int createdId = _service.Create(persistencia);
                var aux = _service.Find(createdId);

                TempData["created"] = $"Prestamo Efectuado Correctamente {aux.FechaPrestamo} - Datos del Registro => Cantidad: {aux.Cantidad}, Libro: {aux.Libro.Titulo}, Entregado a: {aux.Lector.Nombre}.";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}