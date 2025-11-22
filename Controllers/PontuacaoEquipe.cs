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
    public class PontuacaoEquipe : Controller
    {
        private readonly Contexto _context;

        public PontuacaoEquipe(Contexto context)
        {
            _context = context;
        }

        // GET: PontuacaoEquipe
        public async Task<IActionResult> Index()
        {
            var contexto = _context.PontuacaoEquipeTemporada.Include(p => p.Equipe).Include(p => p.Temporada);
            return View(await contexto.ToListAsync());
        }

        // GET: PontuacaoEquipe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontuacaoEquipeTemporada = await _context.PontuacaoEquipeTemporada
                .Include(p => p.Equipe)
                .Include(p => p.Temporada)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pontuacaoEquipeTemporada == null)
            {
                return NotFound();
            }

            return View(pontuacaoEquipeTemporada);
        }

        // GET: PontuacaoEquipe/Create
        public IActionResult Create()
        {
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "Id", "Nome_equipe");
            ViewData["TemporadaId"] = new SelectList(_context.Temporas, "Id", "Id");
            return View();
        }

        // POST: PontuacaoEquipe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,EquipeId,TemporadaId,Pontos")] PontuacaoEquipeTemporada pontuacaoEquipeTemporada)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pontuacaoEquipeTemporada);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "Id", "Nome_equipe", pontuacaoEquipeTemporada.EquipeId);
            ViewData["TemporadaId"] = new SelectList(_context.Temporas, "Id", "Id", pontuacaoEquipeTemporada.TemporadaId);
            return View(pontuacaoEquipeTemporada);
        }

        // GET: PontuacaoEquipe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontuacaoEquipeTemporada = await _context.PontuacaoEquipeTemporada.FindAsync(id);
            if (pontuacaoEquipeTemporada == null)
            {
                return NotFound();
            }
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "Id", "Nome_equipe", pontuacaoEquipeTemporada.EquipeId);
            ViewData["TemporadaId"] = new SelectList(_context.Temporas, "Id", "Id", pontuacaoEquipeTemporada.TemporadaId);
            return View(pontuacaoEquipeTemporada);
        }

        // POST: PontuacaoEquipe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,EquipeId,TemporadaId,Pontos")] PontuacaoEquipeTemporada pontuacaoEquipeTemporada)
        {
            if (id != pontuacaoEquipeTemporada.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pontuacaoEquipeTemporada);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PontuacaoEquipeTemporadaExists(pontuacaoEquipeTemporada.Id))
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
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "Id", "Nome_equipe", pontuacaoEquipeTemporada.EquipeId);
            ViewData["TemporadaId"] = new SelectList(_context.Temporas, "Id", "Id", pontuacaoEquipeTemporada.TemporadaId);
            return View(pontuacaoEquipeTemporada);
        }

        // GET: PontuacaoEquipe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pontuacaoEquipeTemporada = await _context.PontuacaoEquipeTemporada
                .Include(p => p.Equipe)
                .Include(p => p.Temporada)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pontuacaoEquipeTemporada == null)
            {
                return NotFound();
            }

            return View(pontuacaoEquipeTemporada);
        }

        // POST: PontuacaoEquipe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pontuacaoEquipeTemporada = await _context.PontuacaoEquipeTemporada.FindAsync(id);
            if (pontuacaoEquipeTemporada != null)
            {
                _context.PontuacaoEquipeTemporada.Remove(pontuacaoEquipeTemporada);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PontuacaoEquipeTemporadaExists(int id)
        {
            return _context.PontuacaoEquipeTemporada.Any(e => e.Id == id);
        }
    }
}
