using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Formula1.Models;

namespace Formula1.Controllers
{
    public class PontuacaoPiloto : Controller
    {
        private readonly Contexto _context;

        public PontuacaoPiloto(Contexto context)
        {
            _context = context;
        }

        // GET: PontuacaoPiloto
        public async Task<IActionResult> Index()
        {
            var contexto = _context.PontuacaoPilotoTemporada.Include(p => p.Piloto).Include(p => p.Temporada);
            return View(await contexto.ToListAsync());
        }

        // GET: PontuacaoPiloto/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontuacaoPilotoTemporada = await _context.PontuacaoPilotoTemporada
                .Include(p => p.Piloto)
                .Include(p => p.Temporada)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pontuacaoPilotoTemporada == null)
            {
                return NotFound();
            }

            return View(pontuacaoPilotoTemporada);
        }

        // GET: PontuacaoPiloto/Create
        public IActionResult Create()
        {
            ViewData["PilotoId"] = new SelectList(_context.Pilotos, "Id", "Nome_piloto");
            ViewData["TemporadaId"] = new SelectList(_context.Temporas, "Id", "Id");
            return View();
        }

        // POST: PontuacaoPiloto/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PilotoId,TemporadaId,Pontos")] PontuacaoPilotoTemporada pontuacaoPilotoTemporada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pontuacaoPilotoTemporada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PilotoId"] = new SelectList(_context.Pilotos, "Id", "Nome_piloto", pontuacaoPilotoTemporada.PilotoId);
            ViewData["TemporadaId"] = new SelectList(_context.Temporas, "Id", "Id", pontuacaoPilotoTemporada.TemporadaId);
            return View(pontuacaoPilotoTemporada);
        }

        // GET: PontuacaoPiloto/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontuacaoPilotoTemporada = await _context.PontuacaoPilotoTemporada.FindAsync(id);
            if (pontuacaoPilotoTemporada == null)
            {
                return NotFound();
            }
            ViewData["PilotoId"] = new SelectList(_context.Pilotos, "Id", "Nome_piloto", pontuacaoPilotoTemporada.PilotoId);
            ViewData["TemporadaId"] = new SelectList(_context.Temporas, "Id", "Id", pontuacaoPilotoTemporada.TemporadaId);
            return View(pontuacaoPilotoTemporada);
        }

        // POST: PontuacaoPiloto/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PilotoId,TemporadaId,Pontos")] PontuacaoPilotoTemporada pontuacaoPilotoTemporada)
        {
            if (id != pontuacaoPilotoTemporada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pontuacaoPilotoTemporada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PontuacaoPilotoTemporadaExists(pontuacaoPilotoTemporada.Id))
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
            ViewData["PilotoId"] = new SelectList(_context.Pilotos, "Id", "Nome_piloto", pontuacaoPilotoTemporada.PilotoId);
            ViewData["TemporadaId"] = new SelectList(_context.Temporas, "Id", "Id", pontuacaoPilotoTemporada.TemporadaId);
            return View(pontuacaoPilotoTemporada);
        }

        // GET: PontuacaoPiloto/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontuacaoPilotoTemporada = await _context.PontuacaoPilotoTemporada
                .Include(p => p.Piloto)
                .Include(p => p.Temporada)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pontuacaoPilotoTemporada == null)
            {
                return NotFound();
            }

            return View(pontuacaoPilotoTemporada);
        }

        // POST: PontuacaoPiloto/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pontuacaoPilotoTemporada = await _context.PontuacaoPilotoTemporada.FindAsync(id);
            if (pontuacaoPilotoTemporada != null)
            {
                _context.PontuacaoPilotoTemporada.Remove(pontuacaoPilotoTemporada);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PontuacaoPilotoTemporadaExists(int id)
        {
            return _context.PontuacaoPilotoTemporada.Any(e => e.Id == id);
        }
    }
}
