using Formula1.Data;
using Formula1.Models;
using Formula1.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Formula1.Controllers
{
    public class RelatorioController : Controller
    {
        private readonly Contexto _context;

        public RelatorioController(Contexto context)
        {
            _context = context;
        }

        public async Task<IActionResult> PontuacaoPiloto(int? temporadaId)
        {
            ViewBag.Temporadas = new SelectList(_context.Temporas, "Id", "Ano", temporadaId);

            if (!temporadaId.HasValue)
                return View(new List<PontuacaoPilotoViewModel>());

            var dados = await _context.ResultadoCorridas
                .Include(r => r.Piloto)
                .Include(r => r.Temporada)
                .Where(r => r.TemporadaId == temporadaId)
                .GroupBy(r => new
                {
                    r.PilotoId,
                    NomePiloto = r.Piloto.Nome_piloto
                })
                .Select(g => new PontuacaoPilotoViewModel
                {
                    NomePiloto = g.Key.NomePiloto,
                    TotalPontos = g.Sum(x => x.PontosGanhos)
                })
                .OrderByDescending(x => x.TotalPontos)
                .ToListAsync();

            return View(dados);
        }

        public async Task<IActionResult> PontuacaoEquipe(int? temporadaId)
        {
            ViewBag.Temporadas = new SelectList(_context.Temporas, "Id", "Ano", temporadaId);

            if (!temporadaId.HasValue)
                return View(new List<PontuacaoEquipeViewModel>());

            var dados = await _context.ResultadoCorridas
                .Include(r => r.Equipe)
                .Include(r => r.Temporada)
                .Where(r => r.TemporadaId == temporadaId)
                .GroupBy(r => new
                {
                    r.EquipeId,
                    NomeEquipe = r.Equipe.Nome_equipe
                })
                .Select(g => new PontuacaoEquipeViewModel
                {
                    NomeEquipe = g.Key.NomeEquipe,
                    TotalPontos = g.Sum(x => x.PontosGanhos)
                })
                .OrderByDescending(x => x.TotalPontos)
                .ToListAsync();

            return View(dados);
        }
    }
}
