using UnityEngine;

public class DoubleJump : MonoBehaviour
{
    [Header("Configurações de Pulo")]
    public float jumpForce = 10f;
    public int maxJumps = 1; // 2 = pulo duplo

    private int jumpsRemaining;
    private Rigidbody2D rb;

    [Header("Detecção de Chão")]
    public Transform groundCheck;
    public float checkRadius = 0.2f;
    public LayerMask whatIsGround;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        jumpsRemaining = maxJumps;
    }

    void Update()
    {
        bool isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        // se tocar o chão, resetar pulo
        if (isGrounded)
            jumpsRemaining = maxJumps;

        // botão de pulo (Space)
        if (Input.GetButtonDown("Jump") && jumpsRemaining > 0)
        {
            Jump();
        }
    }

    void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 0f); // limpa velocidade vertical
        rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        jumpsRemaining--;
    }

    void OnDrawGizmosSelected()
    {
        // desenha círculo para verificação do chão
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheck.position, checkRadius);
    }
}
