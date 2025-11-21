public class SistemaDeVidaInimigo : SistemaDeVida
{
    Inimigo inimigo;
    BarraDeVidaInimigo barraDeVidaInimigo;

    new void Start()
    {
        base.Start();
        inimigo = GetComponent<Inimigo>();
        barraDeVidaInimigo = GetComponentInChildren<BarraDeVidaInimigo>();
    }

    public override void AplicarDano(float dano)
    {
        vidaAtual -= dano;

        if (inimigo != null)
        {
            inimigo.AnimacaoDeDano();
            inimigo.EfeitoDePiscar();
            inimigo.EfeitoDeRecuo();
        }

        AtualizarVida();

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    override protected void Morrer()
    {
        if (inimigo != null)
        {
            inimigo.AnimacaoDeMorte();
        }
    }

    void AtualizarVida()
    {
        if (barraDeVidaInimigo != null)
        {
            barraDeVidaInimigo.AtualizarUI(vidaAtual / vidaMaxima);
        }
    }
}
