using System;

public enum TipoPeca { Nula, Peao, Cavaleiro, Bispo, Torre, Rainha, Rei }

public abstract class Peca
{
    public TipoPeca Tipo { get; set; }
    public Char Cor { get; set; }
}

public class PecaValida : Peca
{
    public PecaValida(Char cor) => this.Cor = (cor == 'B' || cor == 'P') ? cor : throw new Exceptions.CorInvalida();
}

public class Torre : PecaValida
{
    public Torre(Char cor) : base(cor) => this.Tipo = TipoPeca.Torre;
}

public class Cavaleiro : PecaValida
{
    public Cavaleiro(Char cor) : base(cor) => this.Tipo = TipoPeca.Cavaleiro;
}

public class Bispo : PecaValida
{
    public Bispo(Char cor) : base(cor) => this.Tipo = TipoPeca.Bispo;
}

public class Rei : PecaValida
{
    public Rei(Char cor) : base(cor) => this.Tipo = TipoPeca.Rei;
}

public class Rainha : PecaValida
{
    public Rainha(Char cor) : base(cor) => this.Tipo = TipoPeca.Rainha;
}

public class Peao : PecaValida
{
    public Peao(Char cor) : base(cor) => this.Tipo = TipoPeca.Peao;
}

public class PecaNula : Peca
{
    public PecaNula() => this.Tipo = TipoPeca.Nula;
}
