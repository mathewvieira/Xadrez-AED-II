using System;
using System.Threading;
using System.Collections.Generic;

public class Jogador
{
    public char CorPeca { get; set; }

    public Jogador(char corPeca)
    {
        CorPeca = corPeca;
    }
}

public class Partida
{
    private Jogador _jogadorUm { get; set; }
    private Jogador _jogadorDois { get; set; }
    public Jogador JogadorAtual { get; set; }

    public Partida()
    {
        _jogadorUm = new Jogador('B');
        _jogadorDois = new Jogador('P');
        JogadorAtual = _jogadorUm;
    }

    public bool EhPecaDoJogadorAtual(Casa casa)
    {
        return casa.Peca.Cor == JogadorAtual.CorPeca;
    }

    public char ObterJogadorAtual()
    {
        return JogadorAtual.CorPeca;
    }

    public string ObterJogadorAtualNome()
    {
        return JogadorAtual.CorPeca == 'B' ? "Branco" : "Preto";
    }

    public void AlternarJogador()
    {
        JogadorAtual = JogadorAtual.CorPeca == 'B' ? _jogadorDois : _jogadorUm;
    }
}

public sealed class Mecanica
{
    private Casa _pecaSelecionada { get; set; }
    private String _respostaLida { get; set; }

    private Tabuleiro _tabuleiro { get; }
    private Grafo _grafo { get; }
    private Partida _partida { get; }
    private Movimentacao _movimentacao { get; }

    private Mecanica()
    {
        _tabuleiro = new Tabuleiro();
        _grafo = new Grafo(Constants.AlturaTabuleiro * Constants.LarguraTabuleiro);
        _partida = new Partida();
        _movimentacao = new Movimentacao(_tabuleiro, _grafo, _partida);
    }

    private static Mecanica _instance;

    public static Mecanica ObterInstancia()
    {
        if (_instance == null)
        {
            _instance = new Mecanica();
        }
        return _instance;
    }

    public void Iniciar()
    {
        while (true)
        {
            AtualizarConsole();
            SelecionarPeca();
            MoverPeca();
        }
    }

    public void AtualizarConsole()
    {
        /*

        Console.WriteLine("\n\n" +
            @"             $$\   $$\  $$$$$$\  $$$$$$$\  $$$$$$$\  $$$$$$$$\ $$$$$$$$\       " + "\n" +
            @"             $$ |  $$ |$$  __$$\ $$  __$$\ $$  __$$\ $$  _____|\____$$  |      " + "\n" +
            @"             \$$\ $$  |$$ /  $$ |$$ |  $$ |$$ |  $$ |$$ |          $$  /       " + "\n" +
            @"              \$$$$  / $$$$$$$$ |$$ |  $$ |$$$$$$$  |$$$$$\       $$  /        " + "\n" +
            @"              $$  $$<  $$  __$$ |$$ |  $$ |$$  __$$< $$  __|     $$  /         " + "\n" +
            @"             $$  /\$$\ $$ |  $$ |$$ |  $$ |$$ |  $$ |$$ |       $$  /          " + "\n" +
            @"             $$ /  $$ |$$ |  $$ |$$$$$$$  |$$ |  $$ |$$$$$$$$\ $$$$$$$$\       " + "\n" +
            @"             \__|  \__|\__|  \__|\_______/ \__|  \__|\________|\________|      \n\n\n");

        */

        Console.Clear();
        Console.WriteLine($"\x1b[3J\x1b[7m{Constants.LinhaVazia}\n{Constants.LinhaHorizontalCima}");

        for (int i = (Constants.AlturaTabuleiro - 1); i >= 0; i--)
        {
            String linha = "";

            for (int j = 0; j < Constants.LarguraTabuleiro; j++)
            {
                Casa casa = _tabuleiro.ObterCasa(i, j);

                linha += $" │   {(casa.Peca.Cor == 'B' ? Constants.MapeamentoPecaBranca[casa.Peca.Tipo] : Constants.MapeamentoPecaPreta[casa.Peca.Tipo])}   ";
            }

            Console.WriteLine($"{Constants.LinhaVertical}\n   {i + 1}   {linha} │        \n{Constants.LinhaVertical}\n{(((i - 1) >= 0) ? Constants.LinhaHorizontal : Constants.LinhaHorizontalBaixo)}");
        }

        Console.WriteLine($"{Constants.LinhaVazia}\n{Constants.LinhaIndicadorColuna}\n{Constants.LinhaVazia}\n\x1b[0m");
    }

    private void SelecionarPeca()
    {
        Console.Write($"                                JogadorAtual:  {_partida.ObterJogadorAtualNome()}");
        Console.Write($"\n\n           (Ex: 4e, 1a, 8h)  Selecionar peça:  ");
        LerPeca();
    }

    private void MoverPeca()
    {
        if (_pecaSelecionada != null)
        {
            List<int[]> movimentos = _movimentacao.ObterMovimentosPossiveisPeca(_pecaSelecionada);

            string movimentosFormatados = "";

            foreach (int[] movimento in movimentos)
                movimentosFormatados += $"{movimento[0] + 1}{Constants.IndicadorColuna[movimento[1]]}  ";

            Console.Write($"\n                        Movimentos possíveis:  {(movimentos.Count > 0 ? movimentosFormatados : "Nenhum")}");

            if (movimentos.Count > 0)
            {
                Console.Write($"\n\n        (Digite 0 para cancelar)  Mover para:  ");

                Casa casaAntiga = _pecaSelecionada, casaNova = LerPeca();

                if (_pecaSelecionada != null)
                {
                    _movimentacao.MoverPeca(casaAntiga, casaNova);
                    _partida.AlternarJogador();
                }
            }
            else
            {
                for (int i = 3; i > 0; i--)
                {
                    Console.Write($"\r    (Voltando em {i}...)  Movimentos possíveis:  Nenhum");
                    Thread.Sleep(1000);
                }
            }
        }
    }

    private Casa LerPeca()
    {
        string respostaLida = Console.ReadLine();

        if (respostaLida == "0" || respostaLida.Length != 2) return _pecaSelecionada = null;

        _respostaLida = respostaLida;
        _pecaSelecionada = _tabuleiro.ObterCasa((int)char.GetNumericValue(_respostaLida[0]), _respostaLida[1]);
        return _pecaSelecionada;
    }
}
