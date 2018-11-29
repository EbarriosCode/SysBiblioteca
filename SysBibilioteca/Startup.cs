using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SysBibilioteca.Models;
using SysBibilioteca.Services;

namespace SysBibilioteca
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<BibliotecaDbContext>(options => options.UseInMemoryDatabase("biblioteca_db"));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);


            //Inyección de depndencias
            services.AddTransient<IAutoresService, AutoresService>();
            services.AddTransient<ICategoriaService, CategoriaService>();
            services.AddTransient<IEditorialService, EditorialService>();
            services.AddTransient<IUbicacionService, UbicacionService>();
            services.AddTransient<ILibroService, LibroService>();
            services.AddTransient<IDonadorService, DonadorService>();
            services.AddTransient<IDonacionService, DonacionService>();
            services.AddTransient<ILectorService, LectorService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, BibliotecaDbContext _context)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            /* Creación de registros para iniciar la BD en memoria */
            if (!_context.Autores.Any())
            {
                _context.Autores.AddRange(new List<Autor>
                {
                    new Autor { Nombre = "Eduardo Barrios" },
                    new Autor { Nombre = "Miguel Angel Asturias" },
                    new Autor { Nombre = "Juan López" }
                });

                _context.SaveChanges();
            }

            if (!_context.Categorias.Any())
            {
                _context.Categorias.AddRange(new List<Categoria>
                {
                    new Categoria { Nombre = "Historia" },
                    new Categoria { Nombre = "Matemáticas" },
                    new Categoria { Nombre = "Fílosofia" }
                });

                _context.SaveChanges();
            }

            if (!_context.Editoriales.Any())
            {
                _context.Editoriales.AddRange(new List<Editorial>
                {
                    new Editorial { Nombre = "Mcglaw" },
                    new Editorial { Nombre = "Santillana" },
                    new Editorial { Nombre = "APA" }
                });

                _context.SaveChanges();
            }

            if (!_context.Ubicaciones.Any())
            {
                _context.Ubicaciones.AddRange(new List<Ubicacion>
                {
                    new Ubicacion { Nombre = "5-6" },
                    new Ubicacion { Nombre = "9-21" },
                    new Ubicacion { Nombre = "1-10" }
                });

                _context.SaveChanges();
            }

            if (!_context.Libros.Any())
            {
                _context.Libros.AddRange(new List<Libro>
                {
                    new Libro { Codigo = "089", Titulo = "C++", Paginas = 100, Descripcion = "Programación Avanzada", Ejemplares = 10, AutorId = 1, CategoriaId = 1, EditorialId = 1, UbicacionId = 1 },
                    new Libro { Codigo = "065", Titulo = "Rinoceronte", Paginas = 300, Descripcion = "Lectura", Ejemplares = 5, AutorId = 2, CategoriaId = 2, EditorialId = 2, UbicacionId = 2 }                    
                });

                _context.SaveChanges();
            }

            if (!_context.Donadores.Any())
            {
                _context.Donadores.AddRange(new List<Donador>
                {
                    new Donador { Nombre = "Elmer", Telefono = "55443322", Direccion = "Mazatenango" },
                    new Donador { Nombre = "Esteban", Telefono = "22113322", Direccion = "Quetzaltenango" },
                    new Donador { Nombre = "Alex", Telefono = "66778899", Direccion = "Retalhuleu" }
                });

                _context.SaveChanges();
            }            

            if (!_context.Cargos.Any())
            {
                _context.Cargos.AddRange(new List<Cargo>
                {
                    new Cargo { Nombre = "Docente" },
                    new Cargo { Nombre = "Estudiante" }
                });

                _context.SaveChanges();
            }

            if (!_context.Sexos.Any())
            {
                _context.Sexos.AddRange(new List<Sexo>
                {
                    new Sexo { Nombre = "Masculino" },
                    new Sexo { Nombre = "Femenino" }
                });

                _context.SaveChanges();
            }
            /* FIN Creación de registros para iniciar la BD en memoria */

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
