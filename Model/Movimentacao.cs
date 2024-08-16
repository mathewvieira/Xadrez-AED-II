using System;
using System.Collections.Generic;

public class Movimentacao
{
    private Tabuleiro _tabuleiro;
    private Grafo _grafo;
    private Partida _partida;

    public Movimentacao(Tabuleiro tabuleiro, Grafo grafo, Partida partida)
    {
        _tabuleiro = tabuleiro;
        _grafo = grafo;
        _partida = partida;
    }

    public void MoverPeca(Casa casaAntiga, Casa casaNova)
    {
        casaNova.Peca = casaAntiga.Peca;
        casaAntiga.Peca = new PecaNula();

        _grafo.LimparGrafo();
    }

    public List<int[]> ObterMovimentosPossiveisPeca(Casa casa)
    {
        if ((casa.Peca.Tipo != TipoPeca.Nula) && _partida.EhPecaDoJogadorAtual(casa))
        {
            Dictionary<TipoPeca, Func<List<int[]>>> movimentos = new Dictionary<TipoPeca, Func<List<int[]>>>
            {
                { TipoPeca.Peao, () => ObterMovimentosPossiveisPeao(casa) },
                { TipoPeca.Torre, () => ObterMovimentosPossiveisTorre(casa) },
                { TipoPeca.Cavaleiro, () => ObterMovimentosPossiveisCavaleiro(casa) },
                { TipoPeca.Bispo, () => ObterMovimentosPossiveisBispo(casa) },
                { TipoPeca.Rei, () => ObterMovimentosPossiveisRei(casa) },
                { TipoPeca.Rainha, () => ObterMovimentosPossiveisRainha(casa) }
            };

            if (movimentos.ContainsKey(casa.Peca.Tipo))
            {
                return movimentos[casa.Peca.Tipo]();
            }
        }

        return new List<int[]>();
    }

    private List<int[]> ObterMovimentosPossiveisPeao(Casa casa)
    {
        if (casa.Peca is Peao && _partida.EhPecaDoJogadorAtual(casa))
        {
            List<int[]> movimentos = new List<int[]>();

            int direcao = casa.Peca.Cor == 'B' ? 1 : -1;
            int linhaInicial = casa.Peca.Cor == 'B' ? 1 : 6;

            // Movimento para frente (uma casa)
            int linhaDestino = casa.Linha + direcao;
            if (_tabuleiro.DentroDosLimites(linhaDestino, casa.Coluna) &&
                _tabuleiro.ObterCasa(linhaDestino, casa.Coluna).Peca.Tipo == TipoPeca.Nula)
            {
                movimentos.Add(new int[] { linhaDestino, casa.Coluna });

                // Movimento para frente (duas casas, se for o primeiro movimento)
                if (casa.Linha == linhaInicial)
                {
                    int linhaDoisPassos = casa.Linha + 2 * direcao;
                    if (_tabuleiro.DentroDosLimites(linhaDoisPassos, casa.Coluna) &&
                        _tabuleiro.ObterCasa(linhaDoisPassos, casa.Coluna).Peca.Tipo == TipoPeca.Nula)
                    {
                        movimentos.Add(new int[] { linhaDoisPassos, casa.Coluna });
                    }
                }
            }

            // Captura diagonal
            foreach (int diagonal in new int[] { -1, 1 })
            {
                int linhaDiagonal = casa.Linha + direcao;
                int colunaDiagonal = casa.Coluna + diagonal;

                if (_tabuleiro.DentroDosLimites(linhaDiagonal, colunaDiagonal) &&
                    _tabuleiro.ObterCasa(linhaDiagonal, colunaDiagonal).Peca.Tipo != TipoPeca.Nula &&
                    _tabuleiro.ObterCasa(linhaDiagonal, colunaDiagonal).Peca.Cor != _partida.ObterJogadorAtual())
                {
                    movimentos.Add(new int[] { linhaDiagonal, colunaDiagonal });
                }
            }

            foreach (var movimento in movimentos)
            {
                _grafo.AdicionarAresta(casa.ObterIndice(), _tabuleiro.ObterCasa(movimento[0], movimento[1]).ObterIndice());
            }

            /*

            foreach (int[] movimento in movimentos) Console.WriteLine("{ " + movimento[0] + ", " + movimento[1] + " }");

            Console.WriteLine("\n");

            foreach (int vertice in _grafo.BuscaEmLargura(_grafo.ObterPrimeiroVertice()))
            {
                Casa.ConverterIndiceParaCoordenadas(vertice, out int linha, out int coluna);
                Console.WriteLine($"\n\n{{ Vértice: {vertice} => Linha: {linha + 1} Coluna: {coluna + 1} }}");
            }

            */

            return movimentos;
        }

        return new List<int[]>();
    }

    private List<int[]> ObterMovimentosPossiveisTorre(Casa casa)
    {
        List<int[]> movimentos = new List<int[]>();
        VerificarDirecao(movimentos, casa, 1, 0);
        VerificarDirecao(movimentos, casa, -1, 0);
        VerificarDirecao(movimentos, casa, 0, 1);
        VerificarDirecao(movimentos, casa, 0, -1);
        return movimentos;
    }

    private List<int[]> ObterMovimentosPossiveisCavaleiro(Casa casa)
    {
        List<int[]> movimentos = new List<int[]>();
        int[][] movimentosPossiveis = {
            new int[] { -2, -1 }, new int[] { -2, 1 },
            new int[] { -1, -2 }, new int[] { -1, 2 },
            new int[] { 1, -2 },  new int[] { 1, 2 },
            new int[] { 2, -1 },  new int[] { 2, 1 }
        };

        foreach (int[] movimento in movimentosPossiveis)
            AdicionarMovimentoValido(movimentos, casa, casa.Linha + movimento[0], casa.Coluna + movimento[1]);
        return movimentos;
    }

    private List<int[]> ObterMovimentosPossiveisBispo(Casa casa)
    {
        List<int[]> movimentos = new List<int[]>();
        VerificarDirecao(movimentos, casa, 1, 1);
        VerificarDirecao(movimentos, casa, 1, -1);
        VerificarDirecao(movimentos, casa, -1, 1);
        VerificarDirecao(movimentos, casa, -1, -1);
        return movimentos;
    }

    private List<int[]> ObterMovimentosPossiveisRei(Casa casa)
    {
        List<int[]> movimentos = new List<int[]>();
        for (int i = -1; i <= 1; i++)
            for (int j = -1; j <= 1; j++)
                if (i != 0 || j != 0)
                    AdicionarMovimentoValido(movimentos, casa, casa.Linha + i, casa.Coluna + j);
        return movimentos;
    }

    private List<int[]> ObterMovimentosPossiveisRainha(Casa casa)
    {
        List<int[]> movimentos = ObterMovimentosPossiveisTorre(casa);
        movimentos.AddRange(ObterMovimentosPossiveisBispo(casa));
        return movimentos;
    }

    // Funções auxiliares

    private void VerificarDirecao(List<int[]> movimentos, Casa casa, int incrementoLinha, int incrementoColuna)
    {
        int linhaAtual = casa.Linha + incrementoLinha;
        int colunaAtual = casa.Coluna + incrementoColuna;

        while (_tabuleiro.DentroDosLimites(linhaAtual, colunaAtual))
        {
            Casa casaDestino = _tabuleiro.ObterCasa(linhaAtual, colunaAtual);
            if (casaDestino.Peca.Tipo == TipoPeca.Nula)
                movimentos.Add(new int[] { linhaAtual, colunaAtual });
            else
            {
                if (casaDestino.Peca.Cor != _partida.ObterJogadorAtual())
                    movimentos.Add(new int[] { linhaAtual, colunaAtual });
                break;
            }

            linhaAtual += incrementoLinha;
            colunaAtual += incrementoColuna;
        }
    }

    private void AdicionarMovimentoValido(List<int[]> movimentos, Casa casaOrigem, int linhaDestino, int colunaDestino)
    {
        if (_tabuleiro.DentroDosLimites(linhaDestino, colunaDestino) &&
            (_tabuleiro.ObterCasa(linhaDestino, colunaDestino).Peca.Tipo == TipoPeca.Nula ||
             _tabuleiro.ObterCasa(linhaDestino, colunaDestino).Peca.Cor != _partida.ObterJogadorAtual()))
        {
            movimentos.Add(new int[] { linhaDestino, colunaDestino });
        }
    }
}
