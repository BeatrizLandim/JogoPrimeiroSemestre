using UnityEngine;
using UnityEngine.SceneManagement;

public class SistemaDeVida : MonoBehaviour
{
    [Header("Barra de Vida")]
    public BarraDeVida barraDeVida;

    [Header("Configuração de Vida")]
    [Range(0, 100)] public float vidaMaxima = 100f;
    [Range(0, 100)] public float vidaAtual;

    [Header("Animações")]
    public Animator anim;

    [Header("Tempo para Reiniciar")]
    public float tempoAntesDeReiniciar = 5f;

    protected bool morreu = false;

    protected void Start()
    {
        vidaAtual = vidaMaxima;
        AtualizarVida();

        if (anim == null)
            anim = GetComponent<Animator>();

        anim.SetBool("Vivo", true);
        anim.SetBool("Machucado", false);
    }

    public virtual void AplicarDano(float dano)
    {
        if (morreu) return;

        vidaAtual -= dano;
        AtualizarVida();

        anim.SetBool("Machucado", true);
        Invoke(nameof(PararAnimMachucado), 0.25f);

        if (vidaAtual <= 0)
        {
            Morrer();
        }
    }

    private void PararAnimMachucado()
    {
        anim.SetBool("Machucado", false);
    }

    protected virtual void Morrer()
    {
        if (morreu) return;
        morreu = true;

        anim.SetBool("Vivo", false);
        anim.SetBool("Machucado", false);

        Invoke(nameof(ReiniciarCena), tempoAntesDeReiniciar);
    }

    private void ReiniciarCena()
    {
        Scene cenaAtual = SceneManager.GetActiveScene();
        SceneManager.LoadScene(cenaAtual.name);
    }

    protected void AtualizarVida()
    {
        barraDeVida.AtualizarUI(vidaAtual / vidaMaxima);
    }
}
