using UnityEngine;

public class VentoZona : MonoBehaviour
{
    public float forcaVento = 10f; // você ajusta no inspector
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
            // empurra com força constante para cima
            rbJogador.AddForce(Vector2.up * forcaVento);
        }
    }
}
