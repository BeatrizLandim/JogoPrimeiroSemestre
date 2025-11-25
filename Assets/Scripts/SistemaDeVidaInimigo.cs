public class SistemaDeVidaInimigo : SistemaDeVida
{
    private Inimigo inimigo;
    private BarraDeVidaInimigo barraDeVidaInimigo;

    protected override void Start()
    {
        base.Start(); // chama a inicialização da classe pai

        inimigo = GetComponent<Inimigo>();
        barraDeVidaInimigo = GetComponentInChildren<BarraDeVidaInimigo>();
    }

    public override void AplicarDano(float dano)
    {
        if (estaMorto) return;

        vidaAtual -= dano;

        // efeitos específicos do inimigo
        inimigo.AnimacaoDeDano();
        inimigo.EfeitoDePiscar();

        AtualizarVidaInimigo();

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    private void AtualizarVidaInimigo()
    {
        if (barraDeVidaInimigo != null)
            barraDeVidaInimigo.AtualizarUI(vidaAtual / vidaMaxima);
    }

    protected override void Morrer()
    {
        if (estaMorto) return;
        estaMorto = true;

        // animação do inimigo
        inimigo.AnimacaoDeMorte();

        // impedimos o pai de reiniciar a cena
        if (animator != null)
            animator.SetBool("Vivo", false);

        // destruir o inimigo após a animação
        Destroy(gameObject, 2f);
    }
}
