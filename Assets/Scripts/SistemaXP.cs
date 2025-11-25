using UnityEngine;
using UnityEngine.UI;

public class SistemaXP : MonoBehaviour
{
    public static SistemaXP instance;   // ← Singleton aqui

    [Header("Configurações de XP")]
    public int xpAtual = 0;
    public int xpMaximo = 100;

    [Header("UI")]
    public Image barraXP;

    [Header("Nível Atual")]
    public int nivelAtual = 1; // bronze = 1, prata = 2, ouro = 3

    public static event System.Action<int> OnNivelMudou;

    void Awake()
    {
        // Garantindo singleton
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        AtualizarBarra();
    }

    public void AdicionarXP(int quantidade)
    {
        xpAtual += quantidade;

        if (xpAtual > xpMaximo)
            xpAtual = xpMaximo;

        AtualizarNivel();
        AtualizarBarra();
    }

    void AtualizarBarra()
    {
        float fill = (float)xpAtual / xpMaximo;
        barraXP.fillAmount = fill;
    }

    void AtualizarNivel()
    {
        int novoNivel = nivelAtual;

        float porcentagem = (float)xpAtual / xpMaximo;

        if (porcentagem < 0.33f)
            novoNivel = 1;
        else if (porcentagem < 0.66f)
            novoNivel = 2;
        else
            novoNivel = 3;

        if (novoNivel != nivelAtual)
        {
            nivelAtual = novoNivel;
            OnNivelMudou?.Invoke(nivelAtual);
        }
    }
}
