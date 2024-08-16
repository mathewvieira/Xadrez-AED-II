using System;
using System.Collections.Generic;

public class Constants
{
    public const short AlturaTabuleiro = 8, LarguraTabuleiro = 8;

    public static readonly String LinhaVazia = "                                                                                         ";
    public static readonly String LinhaHorizontalCima = "        ┌────────┬────────┬────────┬────────┬────────┬────────┬────────┬────────┐        ";
    public static readonly String LinhaHorizontal = "        ├────────┼────────┼────────┼────────┼────────┼────────┼────────┼────────┤        ";
    public static readonly String LinhaVertical = "        │        │        │        │        │        │        │        │        │        ";
    public static readonly String LinhaHorizontalBaixo = "        └────────┴────────┴────────┴────────┴────────┴────────┴────────┴────────┘        ";
    public static readonly String LinhaIndicadorColuna = "             a        b        c        d        e        f        g        h            ";

    public static readonly Char[] IndicadorColuna = { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h' };

    public static readonly Dictionary<int, int> MapeamentoLinha = new Dictionary<int, int>
    {
        { 0, 7 },
        { 1, 6 },
        { 2, 5 },
        { 3, 4 },
        { 4, 3 },
        { 5, 2 },
        { 6, 1 },
        { 7, 0 }
    };

    public static readonly Dictionary<char, int> MapeamentoColuna = new Dictionary<char, int>
    {
        { 'a', 0 },
        { 'b', 1 },
        { 'c', 2 },
        { 'd', 3 },
        { 'e', 4 },
        { 'f', 5 },
        { 'g', 6 },
        { 'h', 7 }
    };

    public static readonly Dictionary<TipoPeca, string> MapeamentoPecaBranca = new Dictionary<TipoPeca, string>
    {
        { TipoPeca.Nula, " " },
        { TipoPeca.Torre, "♖" },
        { TipoPeca.Cavaleiro, "♘" },
        { TipoPeca.Bispo, "♗" },
        { TipoPeca.Rei, "♔" },
        { TipoPeca.Rainha, "♕" },
        { TipoPeca.Peao, "♙" }
    };

    public static readonly Dictionary<TipoPeca, string> MapeamentoPecaPreta = new Dictionary<TipoPeca, string>
    {
        { TipoPeca.Nula, " " },
        { TipoPeca.Torre, "♜" },
        { TipoPeca.Cavaleiro, "♞" },
        { TipoPeca.Bispo, "♝" },
        { TipoPeca.Rei, "♚" },
        { TipoPeca.Rainha, "♛" },
        { TipoPeca.Peao, "♟︎" }
    };
}
