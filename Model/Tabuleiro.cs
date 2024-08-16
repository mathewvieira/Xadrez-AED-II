using System;

public class Casa
{
    public int Linha { get; set; }
    public int Coluna { get; set; }
    public Peca Peca { get; set; }
    public char CorFundo { get; set; }
    public string Posicao { get; set; }

    public Casa(int linha, int coluna)
    {
        this.Linha = linha;
        this.Coluna = coluna;
    }

    public int ObterIndice() => this.Linha * Constants.LarguraTabuleiro + this.Coluna;

    public static void ConverterIndiceParaCoordenadas(int indice, out int linha, out int coluna)
    {
        linha = indice / Constants.LarguraTabuleiro;
        coluna = indice % Constants.LarguraTabuleiro;
    }

    public String ObterInfo()
    {
        return $"Linha: {this.Linha}" +
            $" / Coluna: {this.Coluna}" +
            $" / Peça: {(this.Peca.Tipo == TipoPeca.Nula ? "Nula" : $"{this.Peca.Tipo}({this.Peca.Cor})")}" +
            $" / CorFundo: {this.CorFundo}" +
            $" / Posição: {this.Posicao}";
    }
}

public class Tabuleiro
{
    private Casa[,] _casas;

    public Tabuleiro()
    {
        _casas = new Casa[Constants.AlturaTabuleiro, Constants.LarguraTabuleiro];

        CriarCasas();
        PreencherCasas();
    }

    public void CriarCasas()
    {
        for (int i = 0; i < Constants.AlturaTabuleiro; i++)
        {
            for (int j = 0; j < Constants.LarguraTabuleiro; j++)
            {
                _casas[i, j] = new Casa(i, j);
                _casas[i, j].Linha = i;
                _casas[i, j].Coluna = j;
                _casas[i, j].Peca = null;
                _casas[i, j].CorFundo = (i + j) % 2 == 0 ? 'P' : 'B';
                _casas[i, j].Posicao = (i + 1).ToString() + Constants.IndicadorColuna[j];
            }
        }
    }

    public void PreencherCasas()
    {
        Peca[,] pecasIniciais = new Peca[Constants.AlturaTabuleiro, Constants.LarguraTabuleiro]
        {
            { new Torre('P'), new Cavaleiro('P'), new Bispo('P'), new Rei('P'), new Rainha('P'), new Bispo('P'), new Cavaleiro('P'), new Torre('P') },
            { new Peao('P'), new Peao('P'), new Peao('P'), new Peao('P'), new Peao('P'), new Peao('P'), new Peao('P'), new Peao('P') },
            { new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula() },
            { new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula() },
            { new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula() },
            { new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula(), new PecaNula() },
            { new Peao('B'), new Peao('B'), new Peao('B'), new Peao('B'), new Peao('B'), new Peao('B'), new Peao('B'), new Peao('B') },
            { new Torre('B'), new Cavaleiro('B'), new Bispo('B'), new Rei('B'), new Rainha('B'), new Bispo('B'), new Cavaleiro('B'), new Torre('B') },
        };

        for (int i = 0; i < Constants.AlturaTabuleiro; i++)
        {
            for (int j = 0; j < Constants.LarguraTabuleiro; j++)
            {
                _casas[i, j].Peca = pecasIniciais[Constants.MapeamentoLinha[i], j];
            }
        }
    }

    public Casa ObterCasa(int linha, char coluna)
    {
        return ObterCasa(linha - 1, Constants.MapeamentoColuna[Char.ToLower(coluna)]);
    }

    public Casa ObterCasa(int linha, int coluna)
    {
        if (DentroDosLimites(linha, coluna)) return _casas[linha, coluna];

        throw new Exceptions.UltrapassaLimiteTabuleiro();
    }

    public bool DentroDosLimites(int linha, int coluna)
    {
        return (linha >= 0) && (linha < Constants.AlturaTabuleiro) && (coluna >= 0) && (coluna < Constants.LarguraTabuleiro);
    }
}
