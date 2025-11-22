using Formula1.Models;
using Microsoft.EntityFrameworkCore;

public class PontuacaoService
{
    private readonly Contexto _context;

    public PontuacaoService(Contexto context)
    {
        _context = context;
    }

    public int PontosPorPosicao(int posicao)
    {
        return posicao switch
        {
            1 => 25,
            2 => 18,
            3 => 15,
            4 => 12,
            5 => 10,
            6 => 8,
            7 => 6,
            8 => 4,
            9 => 2,
            10 => 1,
            _ => 0
        };
    }

    public async Task RegistrarResultadoCorridaAsync(
        int pilotoId,
        int equipeId,
        int temporadaId,
        int pistaId,
        int posicao)
    {
        int pontosGanhos = PontosPorPosicao(posicao);

        var registroPiloto = await _context.PontuacaoPilotoTemporada
            .FirstOrDefaultAsync(x => x.PilotoId == pilotoId && x.TemporadaId == temporadaId);

        if (registroPiloto == null)
        {
            registroPiloto = new PontuacaoPilotoTemporada { PilotoId = pilotoId, TemporadaId = temporadaId, Pontos = 0 };
            _context.PontuacaoPilotoTemporada.Add(registroPiloto);
        }
        registroPiloto.Pontos += pontosGanhos;


        var registroEquipe = await _context.PontuacaoEquipeTemporada 
            .FirstOrDefaultAsync(x => x.EquipeId == equipeId && x.TemporadaId == temporadaId);

        if (registroEquipe == null)
        {
            registroEquipe = new PontuacaoEquipeTemporada { EquipeId = equipeId, TemporadaId = temporadaId, Pontos = 0 };
            _context.PontuacaoEquipeTemporada.Add(registroEquipe);
        }
        registroEquipe.Pontos += pontosGanhos; 

        
    }

 
    public async Task RemoverResultadoCorridaAsync(int pilotoId, int equipeId, int temporadaId)
    {
 
        var resultadosRestantesPiloto = await _context.ResultadoCorridas
            .Where(r => r.PilotoId == pilotoId && r.TemporadaId == temporadaId)
            .ToListAsync();

        int novaPontuacaoPiloto = resultadosRestantesPiloto.Sum(r => PontosPorPosicao(r.Posicao));

        var registroPiloto = await _context.PontuacaoPilotoTemporada
            .FirstOrDefaultAsync(x => x.PilotoId == pilotoId && x.TemporadaId == temporadaId);

        if (registroPiloto != null)
        {
            registroPiloto.Pontos = novaPontuacaoPiloto;
            _context.Update(registroPiloto);
        }

  
        var resultadosRestantesEquipe = await _context.ResultadoCorridas
            .Where(r => r.EquipeId == equipeId && r.TemporadaId == temporadaId)
            .ToListAsync();

        int novaPontuacaoEquipe = resultadosRestantesEquipe.Sum(r => PontosPorPosicao(r.Posicao));

        var registroEquipe = await _context.PontuacaoEquipeTemporada
            .FirstOrDefaultAsync(x => x.EquipeId == equipeId && x.TemporadaId == temporadaId);

        if (registroEquipe != null)
        {
            registroEquipe.Pontos = novaPontuacaoEquipe;
            _context.Update(registroEquipe);
        }

    }
}