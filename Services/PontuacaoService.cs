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
    int temporadaId,
    int pistaId,
    int posicao)
    {
        int pontosGanhos = PontosPorPosicao(posicao);

        // Buscar ou criar temporada
        var registroTemporada = await _context.PontuacaoPilotoTemporada
            .FirstOrDefaultAsync(x => x.PilotoId == pilotoId && x.TemporadaId == temporadaId);

        if (registroTemporada == null)
        {
            registroTemporada = new PontuacaoPilotoTemporada
            {
                PilotoId = pilotoId,
                TemporadaId = temporadaId,
                Pontos = 0
            };

            _context.PontuacaoPilotoTemporada.Add(registroTemporada);
        }

        // Apenas soma os pontos
        registroTemporada.Pontos += pontosGanhos;

        // ❌ Removido: NÃO cria resultado da corrida aqui!

        await _context.SaveChangesAsync();
    }
}