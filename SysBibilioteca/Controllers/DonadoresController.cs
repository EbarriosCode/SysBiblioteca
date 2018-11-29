using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SysBibilioteca.Models;
using SysBibilioteca.Services;

namespace SysBibilioteca.Controllers
{
    public class DonadoresController : Controller
    {
        private readonly IDonadorService _service;

        public DonadoresController(IDonadorService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            return View(_service.GetList());
        }

        [HttpPost]
        public IActionResult Create(string Nombre, string Telefono, string Direccion)
        {
            if (!string.IsNullOrEmpty(Nombre) && !string.IsNullOrEmpty(Telefono) && !string.IsNullOrEmpty(Direccion))
            {
                var persistencia = new Donador()
                {
                    Nombre = Nombre,
                    Telefono = Telefono,
                    Direccion = Direccion
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

        [HttpPost]
        public IActionResult Edit(int idEdit, string NombreEdit, string TelefonoEdit, string DireccionEdit)
        {
            if (idEdit > 0 && !string.IsNullOrEmpty(NombreEdit) && !string.IsNullOrEmpty(TelefonoEdit) && !string.IsNullOrEmpty(DireccionEdit))
            {
                var modified = new Donador()
                {
                    DonadorId = idEdit,
                    Nombre = NombreEdit,
                    Telefono = TelefonoEdit,
                    Direccion = DireccionEdit
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