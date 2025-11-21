using UnityEngine;

public class FinalDaFase : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerMovement pm = other.GetComponent<PlayerMovement>();

            if (pm != null)
            {
                pm.IniciarDanca();
            }
        }
    }
}
