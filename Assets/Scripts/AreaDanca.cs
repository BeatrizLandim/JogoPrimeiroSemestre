using UnityEngine;

public class AreaDanca : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Animator anim = other.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetBool("Dance", true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Animator anim = other.GetComponent<Animator>();
            if (anim != null)
            {
                anim.SetBool("Dance", false);
            }
        }
    }
}
