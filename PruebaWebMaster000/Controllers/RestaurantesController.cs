using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaWebMaster000.Models;

namespace PruebaWebMaster000.Controllers
{
    public class RestaurantesController : Controller
    {
        private readonly BaseMasterContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public RestaurantesController(BaseMasterContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            this._hostEnvironment = hostEnvironment;
        }

        // GET: Restaurantes
        [Authorize]
        public async Task<IActionResult> Index()
        {
            var baseMasterContext = _context.Restaurantes.Include(r => r.IdHorariosNavigation);
            return View(await baseMasterContext.ToListAsync());
        }

        [Authorize]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantes = await _context.Restaurantes
                .Include(r => r.IdHorariosNavigation)
                .FirstOrDefaultAsync(m => m.IdRestaurante == id);
            if (restaurantes == null)
            {
                return NotFound();
            }

            return View(restaurantes);
        }

        // GET: Restaurantes/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["IdHorarios"] = new SelectList(_context.Horarios, "IdHorarios", "HorariosAtencion");
            return View();
        }

        // POST: Restaurantes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdRestaurante,IdHorarios,InformacionGeneral,Logo,ImagenItemDestacado,Calificacion")] Restaurantes restaurantes, IFormFile imglogo, IFormFile imgdestacada)
        {
            if (ModelState.IsValid)
            {
                if (imglogo != null)

                {
                    if (imglogo.Length > 0)

                    //Convert Image to byte and save to database

                    {

                        byte[] p1 = null;
                        using (var fs1 = imglogo.OpenReadStream())
                        using (var ms1 = new MemoryStream())
                        {
                            fs1.CopyTo(ms1);
                            p1 = ms1.ToArray();
                        }
                        restaurantes.Logo = p1;

                    }
                }
                if (imgdestacada != null)

                {
                    if (imgdestacada.Length > 0)

                    //Convert Image to byte and save to database

                    {

                        byte[] p2 = null;
                        using (var fs2 = imgdestacada.OpenReadStream())
                        using (var ms2 = new MemoryStream())
                        {
                            fs2.CopyTo(ms2);
                            p2 = ms2.ToArray();
                        }
                        restaurantes.ImagenItemDestacado = p2;

                    }
                }

                _context.Add(restaurantes);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHorarios"] = new SelectList(_context.Horarios, "IdHorarios", "HorariosAtencion", restaurantes.IdHorarios);
            return View(restaurantes);
        }

        public ActionResult convertirImagen(int codigo)
        {

            using (var context = new BaseMasterContext())
            {
                var imagen = (from i in context.Restaurantes
                              where i.IdRestaurante == codigo
                              select i).FirstOrDefault();


                return File(imagen.Logo, "Imagenes/jpg");


            }


        }
        public ActionResult convertirImagen1(int codigo1)
        {

            using (var context = new BaseMasterContext())
            {

                var imagen2 = (from a in context.Restaurantes
                               where a.IdRestaurante == codigo1
                               select a).FirstOrDefault();

                return File(imagen2.ImagenItemDestacado, "Imagenes/jpg");


            }


        }

        [Authorize]
        // GET: Restaurantes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantes = await _context.Restaurantes.FindAsync(id);
            if (restaurantes == null)
            {
                return NotFound();
            }
            ViewData["IdHorarios"] = new SelectList(_context.Horarios, "IdHorarios", "IdHorarios", restaurantes.IdHorarios);
            return View(restaurantes);
        }

        // POST: Restaurantes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdRestaurante,IdHorarios,InformacionGeneral,Logo,ImagenItemDestacado,Calificacion")] Restaurantes restaurantes)
        {
            if (id != restaurantes.IdRestaurante)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(restaurantes);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RestaurantesExists(restaurantes.IdRestaurante))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdHorarios"] = new SelectList(_context.Horarios, "IdHorarios", "IdHorarios", restaurantes.IdHorarios);
            return View(restaurantes);
        }

        // GET: Restaurantes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var restaurantes = await _context.Restaurantes
                .Include(r => r.IdHorariosNavigation)
                .FirstOrDefaultAsync(m => m.IdRestaurante == id);
            if (restaurantes == null)
            {
                return NotFound();
            }

            return View(restaurantes);
        }

        // POST: Restaurantes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var restaurantes = await _context.Restaurantes.FindAsync(id);
            _context.Restaurantes.Remove(restaurantes);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RestaurantesExists(int id)
        {
            return _context.Restaurantes.Any(e => e.IdRestaurante == id);
        }
    }
}
