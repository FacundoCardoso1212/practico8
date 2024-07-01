using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using practico8.Models;

namespace practico8.Controllers
{
    public class AlquileresController : Controller
    {
        private readonly Practico8Context _context;

        public AlquileresController(Practico8Context context)
        {
            _context = context;
        }

        // GET: Alquileres
        public async Task<IActionResult> Index()
        {
            var practico8Context = _context.Alquileres.Include(a => a.IdClienteNavigation).Include(a => a.IdCopiaNavigation);
            var alquileres = await practico8Context.ToListAsync();

            foreach (var alquiler in alquileres)
            {
                await alquiler.SetPeliculaTituloAsync(_context);
            }

            return View(await practico8Context.ToListAsync());
        }

        // GET: Alquileres/Details/5
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquilere = await _context.Alquileres
                .Include(a => a.IdClienteNavigation)
                .Include(a => a.IdCopiaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alquilere == null)
            {
                return NotFound();
            }

            return View(alquilere);
        }

        // GET: Alquileres/Create
        public IActionResult Create()
        {
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id");
            ViewData["IdCopia"] = new SelectList(_context.Copias, "Id", "Id");
            
            return View();
        }

        // POST: Alquileres/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCopia,IdCliente,FechaAlquiler,FechaTope,FechaEntregada")] Alquilere alquilere)
        {
            if (ModelState.IsValid)
            {
                alquilere.FechaTope = alquilere.FechaAlquiler.AddDays(3);
                _context.Add(alquilere);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", alquilere.IdCliente);
            ViewData["IdCopia"] = new SelectList(_context.Copias, "Id", "Id", alquilere.IdCopia);
            return View(alquilere);
        }

        public string EstadoAlquiler(DateTime? fechaEntregada)
        {
            if (fechaEntregada == null) { return "Sin entregar"; }

            if (fechaEntregada < DateTime.Now)
            {
                return "Entregada";
            }
            else
            {
                return "Activa";
            }
        }

        public async Task<IActionResult>AlquilerXId(long id)
        {
            var alquileres =  await _context.Alquileres.Include(a=> a.IdClienteNavigation).Include(a=> a.IdCopiaNavigation).ThenInclude(c => c.IdPeliculaNavigation).Where(a => a.IdCliente == id).OrderBy(a=> a.FechaEntregada == null ? 0 : 1).ToListAsync();
            return View ("index", alquileres);
        }
      

        // GET: Alquileres/Edit/5
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquilere = await _context.Alquileres.FindAsync(id);
            if (alquilere == null)
            {
                return NotFound();
            }
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", alquilere.IdCliente);
            ViewData["IdCopia"] = new SelectList(_context.Copias, "Id", "Id", alquilere.IdCopia);
            return View(alquilere);
        }

        // POST: Alquileres/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(long id, [Bind("Id,IdCopia,IdCliente,FechaAlquiler,FechaTope,FechaEntregada")] Alquilere alquilere)
        {
            if (id != alquilere.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(alquilere);
                    await _context.SaveChangesAsync();
                }
                catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
                {
                    if (!AlquilereExists(alquilere.Id))
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
            ViewData["IdCliente"] = new SelectList(_context.Clientes, "Id", "Id", alquilere.IdCliente);
            ViewData["IdCopia"] = new SelectList(_context.Copias, "Id", "Id", alquilere.IdCopia);
            return View(alquilere);
        }

        // GET: Alquileres/Delete/5
        public async Task<IActionResult> Delete(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var alquilere = await _context.Alquileres
                .Include(a => a.IdClienteNavigation)
                .Include(a => a.IdCopiaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (alquilere == null)
            {
                return NotFound();
            }

            return View(alquilere);
        }

        // POST: Alquileres/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var alquilere = await _context.Alquileres.FindAsync(id);
            if (alquilere != null)
            {
                _context.Alquileres.Remove(alquilere);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AlquilereExists(long id)
        {
            return _context.Alquileres.Any(e => e.Id == id);
        }
    }
}
