using Formula1.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace Formula1.Controllers
{
    public class ResultadoCorridas : Controller
    {
        private readonly Contexto _context;
        private readonly PontuacaoService _pontuacaoService;

        // ✔ único construtor correto
        public ResultadoCorridas(Contexto context)
        {
            _context = context;
            _pontuacaoService = new PontuacaoService(context);
        }

        // GET: ResultadoCorridas
        public async Task<IActionResult> Index()
        {
            var contexto = _context.ResultadoCorridas
                .Include(r => r.Piloto)
                .Include(r => r.Pista)
                .Include(r => r.Temporada);

            return View(await contexto.ToListAsync());
        }

        // GET: ResultadoCorridas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var resultadoCorrida = await _context.ResultadoCorridas
                .Include(r => r.Piloto)
                .Include(r => r.Pista)
                .Include(r => r.Temporada)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (resultadoCorrida == null)
                return NotFound();

            return View(resultadoCorrida);
        }

        // GET: ResultadoCorridas/Create
        public IActionResult Create()
        {
            ViewData["PilotoId"] = new SelectList(_context.Pilotos, "Id", "Nome_piloto");
            ViewData["PistaId"] = new SelectList(_context.Pistas, "Id", "Nome");
            ViewData["TemporadaId"] = new SelectList(_context.Temporas, "Id", "Id");

            return View();
        }

        // POST: ResultadoCorridas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PilotoId,TemporadaId,PistaId,Posicao,PontosGanhos")] ResultadoCorrida resultadoCorrida)
        {
            if (ModelState.IsValid)
            {
                resultadoCorrida.PontosGanhos = _pontuacaoService.PontosPorPosicao(resultadoCorrida.Posicao);

                // 👍 registra no banco normalmente
                _context.Add(resultadoCorrida);

                // 👍 calcula e salva pontos automaticamente
                await _pontuacaoService.RegistrarResultadoCorridaAsync(
                    resultadoCorrida.PilotoId,
                    resultadoCorrida.TemporadaId,
                    resultadoCorrida.PistaId,
                    resultadoCorrida.Posicao
                );

                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // repopula selects
            ViewData["PilotoId"] = new SelectList(_context.Pilotos, "Id", "Nome_piloto", resultadoCorrida.PilotoId);
            ViewData["PistaId"] = new SelectList(_context.Pistas, "Id", "Nome", resultadoCorrida.PistaId);
            ViewData["TemporadaId"] = new SelectList(_context.Temporas, "Id", "Id", resultadoCorrida.TemporadaId);

            return View(resultadoCorrida);
        }
        // POST: ResultadoCorridas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,PilotoId,TemporadaId,PistaId,Posicao,PontosGanhos")] ResultadoCorrida resultadoCorrida)
        {
            if (id != resultadoCorrida.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(resultadoCorrida);
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
            ViewData["PilotoId"] = new SelectList(_context.Pilotos, "Id", "Nome_piloto", resultadoCorrida.PilotoId);
            ViewData["PistaId"] = new SelectList(_context.Pistas, "Id", "Nome", resultadoCorrida.PistaId);
            ViewData["TemporadaId"] = new SelectList(_context.Temporas, "Id", "Id", resultadoCorrida.TemporadaId);
            return View(resultadoCorrida);
        }

        // GET: ResultadoCorridas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var resultadoCorrida = await _context.ResultadoCorridas
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

        // POST: ResultadoCorridas/Delete/5
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
