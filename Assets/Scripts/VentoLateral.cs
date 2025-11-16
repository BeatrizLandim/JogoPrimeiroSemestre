using UnityEngine;

public class VentoLateral : MonoBehaviour
{
    public float forcaVento = 10f;   // Intensidade do vento
    public bool ventoParaDireita = true; // Direção do vento

    private Rigidbody2D rbJogador;
    private bool jogadorNoVento = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            rbJogador = other.GetComponent<Rigidbody2D>();
            jogadorNoVento = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            jogadorNoVento = false;
            rbJogador = null;
        }
    }

    void FixedUpdate()
    {
        if (jogadorNoVento && rbJogador != null)
        {
            // Define a direção: direita (+1) ou esquerda (-1)
            float direcao = ventoParaDireita ? 1f : -1f;

            // Aplica a força lateral
            rbJogador.AddForce(Vector2.right * direcao * forcaVento);
        }
    }
}