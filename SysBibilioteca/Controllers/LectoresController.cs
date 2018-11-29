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
    public class LectoresController : Controller
    {
        private readonly ILectorService _service;

        public LectoresController(ILectorService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            ViewData["CargoId"] = new SelectList(_service.GetCargos(), "CargoId", "Nombre");
            ViewData["SexoId"] = new SelectList(_service.GetSexos(), "SexoId", "Nombre");

            return View(_service.GetList());
        }

        [HttpPost]
        public IActionResult Create(string Carne, string Nombre, string Telefono, string Direccion, int CargoId, int SexoId)
        {
            if (CargoId > 0 && SexoId > 0 && !string.IsNullOrEmpty(Carne) && !string.IsNullOrEmpty(Nombre) && !string.IsNullOrEmpty(Telefono) && !string.IsNullOrEmpty(Direccion))
            {
                var persistencia = new Lector()
                {
                    Carne = Carne,
                    Nombre = Nombre,
                    Telefono = Telefono,
                    Direccion = Direccion,
                    CargoId = CargoId,
                    SexoId = SexoId
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
        public IActionResult Edit(int idEdit, string CarneEdit, string NombreEdit, string TelefonoEdit, string DireccionEdit, int CargoIdEdit, int SexoIdEdit)
        {
            if (idEdit > 0 && CargoIdEdit > 0 && SexoIdEdit > 0 && !string.IsNullOrEmpty(CarneEdit) && !string.IsNullOrEmpty(NombreEdit) && !string.IsNullOrEmpty(TelefonoEdit) && !string.IsNullOrEmpty(DireccionEdit))
            {
                var modified = new Lector()
                {
                    LectorId = idEdit,
                    Carne = CarneEdit,
                    Nombre = NombreEdit,
                    Telefono = TelefonoEdit,
                    Direccion = DireccionEdit,
                    CargoId = CargoIdEdit,
                    SexoId = SexoIdEdit
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