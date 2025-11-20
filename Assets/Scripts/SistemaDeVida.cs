using UnityEngine;

public class SistemaDeVida : MonoBehaviour
{
    public BarraDeVida barraDeVida;

    [Range(0, 100)]
    public float vidaMaxima = 100f;

    [Range(0, 100)]
    public float vidaAtual;

    protected void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarVida();
    }

    public virtual void AplicarDano(float dano)
    {
        vidaAtual -= dano;

        if (vidaAtual <= 0)
        {
            Morrer();
        }

        AtualizarVida();
    }

    private void AtualizarVida()
    {
        barraDeVida.AtualizarUI(vidaAtual / vidaMaxima);
    }

    protected virtual void Morrer()
    {
        Debug.Log("Morreu");
    }
}
