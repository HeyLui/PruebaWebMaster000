using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PruebaWebMaster000.Models;

namespace PruebaWebMaster000.Controllers
{
    [Authorize]
    public class CalificacionsController : Controller
    {
        private readonly BaseMasterContext _context;

        public CalificacionsController(BaseMasterContext context)
        {
            _context = context;
        }

        // GET: Calificacions
        public async Task<IActionResult> Index()
        {
            var baseMasterContext = _context.Calificacion.Include(c => c.IdRestauranteNavigation);
            return View(await baseMasterContext.ToListAsync());
        }

        // GET: Calificacions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacion
                .Include(c => c.IdRestauranteNavigation)
                .FirstOrDefaultAsync(m => m.IdVotos == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // GET: Calificacions/Create
        public IActionResult Create()
        {
            ViewData["IdRestaurante"] = new SelectList(_context.Restaurantes, "IdRestaurante", "IdRestaurante");
            return View();
        }

        // POST: Calificacions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("IdVotos,IdRestaurante,Calificacion1,Usuario,Comentario")] Calificacion calificacion)
        {
            if (ModelState.IsValid)
            {
                _context.Add(calificacion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdRestaurante"] = new SelectList(_context.Restaurantes, "IdRestaurante", "IdRestaurante", calificacion.IdRestaurante);
            return View(calificacion);
        }

        // GET: Calificacions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacion.FindAsync(id);
            if (calificacion == null)
            {
                return NotFound();
            }
            ViewData["IdRestaurante"] = new SelectList(_context.Restaurantes, "IdRestaurante", "IdRestaurante", calificacion.IdRestaurante);
            return View(calificacion);
        }

        // POST: Calificacions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("IdVotos,IdRestaurante,Calificacion1,Usuario,Comentario")] Calificacion calificacion)
        {
            if (id != calificacion.IdVotos)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(calificacion);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CalificacionExists(calificacion.IdVotos))
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
            ViewData["IdRestaurante"] = new SelectList(_context.Restaurantes, "IdRestaurante", "IdRestaurante", calificacion.IdRestaurante);
            return View(calificacion);
        }

        // GET: Calificacions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var calificacion = await _context.Calificacion
                .Include(c => c.IdRestauranteNavigation)
                .FirstOrDefaultAsync(m => m.IdVotos == id);
            if (calificacion == null)
            {
                return NotFound();
            }

            return View(calificacion);
        }

        // POST: Calificacions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var calificacion = await _context.Calificacion.FindAsync(id);
            _context.Calificacion.Remove(calificacion);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CalificacionExists(int id)
        {
            return _context.Calificacion.Any(e => e.IdVotos == id);
        }
    }
}
