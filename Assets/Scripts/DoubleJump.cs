using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [Header("Configurações de Pulo")]
    public float jumpForce = 10f;
    public int maxJumps = 1; // pulo normal + pulo duplo

    private int jumpsRemaining;
    private Rigidbody2D rb;

    [Header("Detecção de Chão")]
    public Transform groundCheck;
    public float checkRadius = 0.15f;     // menor para evitar detectar paredes
    public LayerMask groundLayer;         // somente chão real

    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        // CHECAGEM DE CHÃO REAL
        isGrounded = Physics2D.OverlapCircle(
            groundCheck.position,
            checkRadius,
            groundLayer
        );

        // RESET SE ESTIVER NO CHÃO
        if (isGrounded)
        {
            jumpsRemaining = maxJumps;
        }

        // INPUT DO PULO
        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
        {
            Jump();
        }
    }

    void Jump()
    {
        // garantir pulo consistente
        rb.velocity = new Vector2(rb.velocity.x, 0f);

        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpsRemaining--;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        if (groundCheck != null)
            Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
