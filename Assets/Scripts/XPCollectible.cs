using UnityEngine;

public class XPCollectible : MonoBehaviour
{
    [Header("Quantidade de XP que este item fornece")]
    public int xpValor = 10;

    void OnTriggerEnter2D(Collider2D other)
    {
        // Verifica se o jogador encostou
        if (other.CompareTag("Player"))
        {
            // Adiciona XP usando o SistemaXP
            SistemaXP.instance.AdicionarXP(xpValor);

            // Destrói o item de XP
            Destroy(gameObject);
        }
    }
}
