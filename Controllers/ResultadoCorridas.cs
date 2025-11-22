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
    public class ResultadoCorridas : Controller
    {
        private readonly Contexto _context;
        
        private readonly PontuacaoService _pontuacaoService;

        public ResultadoCorridas(Contexto context, PontuacaoService pontuacaoService)
        {
            _context = context;
            _pontuacaoService = pontuacaoService;
        }

        
        public async Task<IActionResult> Index()
        {
            var contexto = _context.ResultadoCorridas.Include(r => r.Equipe).Include(r => r.Piloto).Include(r => r.Pista).Include(r => r.Temporada);
            return View(await contexto.ToListAsync());
        }

       
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultadoCorrida = await _context.ResultadoCorridas
                .Include(r => r.Equipe)
                .Include(r => r.Piloto)
                .Include(r => r.Pista)
                .Include(r => r.Temporada)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resultadoCorrida == null)
            {
                return NotFound();
            }

            return View(resultadoCorrida);
        }

        
        public IActionResult Create()
        {
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "Id", "Nome_equipe");
            ViewData["PilotoId"] = new SelectList(_context.Pilotos, "Id", "Nome_piloto");
            ViewData["PistaId"] = new SelectList(_context.Pistas, "Id", "Nome");
            ViewData["TemporadaId"] = new SelectList(_context.Temporas, "Id", "Id");
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PilotoId,EquipeId,TemporadaId,PistaId,Posicao,PontosGanhos")] ResultadoCorrida resultadoCorrida)
        {
            if (ModelState.IsValid)
            {
                
                resultadoCorrida.PontosGanhos = _pontuacaoService.PontosPorPosicao(resultadoCorrida.Posicao);

                _context.Add(resultadoCorrida);


                await _pontuacaoService.RegistrarResultadoCorridaAsync(
                    resultadoCorrida.PilotoId,
                    resultadoCorrida.EquipeId, 
                    resultadoCorrida.TemporadaId,
                    resultadoCorrida.PistaId,
                    resultadoCorrida.Posicao
                );

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "Id", "Nome_equipe", resultadoCorrida.EquipeId);
            ViewData["PilotoId"] = new SelectList(_context.Pilotos, "Id", "Nome_piloto", resultadoCorrida.PilotoId);
            ViewData["PistaId"] = new SelectList(_context.Pistas, "Id", "Nome", resultadoCorrida.PistaId);
            ViewData["TemporadaId"] = new SelectList(_context.Temporas, "Id", "Id", resultadoCorrida.TemporadaId);
            return View(resultadoCorrida);
        }

        
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultadoCorrida = await _context.ResultadoCorridas.FindAsync(id);
            if (resultadoCorrida == null)
            {
                return NotFound();
            }
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "Id", "Nome_equipe", resultadoCorrida.EquipeId);
            ViewData["PilotoId"] = new SelectList(_context.Pilotos, "Id", "Nome_piloto", resultadoCorrida.PilotoId);
            ViewData["PistaId"] = new SelectList(_context.Pistas, "Id", "Nome", resultadoCorrida.PistaId);
            ViewData["TemporadaId"] = new SelectList(_context.Temporas, "Id", "Id", resultadoCorrida.TemporadaId);
            return View(resultadoCorrida);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PilotoId,EquipeId,TemporadaId,PistaId,Posicao,PontosGanhos")] ResultadoCorrida resultadoCorrida)
        {
            if (id != resultadoCorrida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    resultadoCorrida.PontosGanhos = _pontuacaoService.PontosPorPosicao(resultadoCorrida.Posicao);

                    _context.Update(resultadoCorrida);

                    await _pontuacaoService.RegistrarResultadoCorridaAsync(
                        resultadoCorrida.PilotoId,
                        resultadoCorrida.EquipeId, 
                        resultadoCorrida.TemporadaId,
                        resultadoCorrida.PistaId,
                        resultadoCorrida.Posicao
                    );

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ResultadoCorridaExists(resultadoCorrida.Id))
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
            ViewData["EquipeId"] = new SelectList(_context.Equipes, "Id", "Nome_equipe", resultadoCorrida.EquipeId);
            ViewData["PilotoId"] = new SelectList(_context.Pilotos, "Id", "Nome_piloto", resultadoCorrida.PilotoId);
            ViewData["PistaId"] = new SelectList(_context.Pistas, "Id", "Nome", resultadoCorrida.PistaId);
            ViewData["TemporadaId"] = new SelectList(_context.Temporas, "Id", "Id", resultadoCorrida.TemporadaId);
            return View(resultadoCorrida);
        }

        
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultadoCorrida = await _context.ResultadoCorridas
                .Include(r => r.Equipe)
                .Include(r => r.Piloto)
                .Include(r => r.Pista)
                .Include(r => r.Temporada)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resultadoCorrida == null)
            {
                return NotFound();
            }

            return View(resultadoCorrida);
        }

   
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resultadoCorrida = await _context.ResultadoCorridas.FindAsync(id);
            if (resultadoCorrida != null)
            {
        
                _context.ResultadoCorridas.Remove(resultadoCorrida);

                
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ResultadoCorridaExists(int id)
        {
            return _context.ResultadoCorridas.Any(e => e.Id == id);
        }
    }
}