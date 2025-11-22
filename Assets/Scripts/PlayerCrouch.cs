using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    [Header("Collider")]
    public BoxCollider2D boxCollider;

    [Header("Tamanhos")]
    public Vector2 standSize = new Vector2(1f, 1.8f);
    public Vector2 standOffset = new Vector2(0f, 0f);

    public Vector2 crouchSize = new Vector2(1f, 1f);
    public Vector2 crouchOffset = new Vector2(0f, -0.4f);

    [Header("Estado")]
    public bool isCrouching;

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.S))
        {
            if (!isCrouching)
                StartCrouch();
        }
        else
        {
            if (isCrouching)
                StopCrouch();
        }
    }

    void StartCrouch()
    {
        isCrouching = true;
        boxCollider.size = crouchSize;
        boxCollider.offset = crouchOffset;

        anim.SetBool("IsCrouching", true);
    }

    void StopCrouch()
    {
        isCrouching = false;
        boxCollider.size = standSize;
        boxCollider.offset = standOffset;

        anim.SetBool("IsCrouching", false);
    }
}
