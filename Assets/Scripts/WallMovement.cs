using UnityEngine;

public class WallMovement : MonoBehaviour
{

    [Header("Checagem de Paredes")]
    public Transform wallCheckLeft;
    public Transform wallCheckRight;
    public float wallCheckRadius = 0.2f;
    public LayerMask groundLayer; // agora é GROUND

    [Header("Configurações do Wall Slide")]
    public float slideDelay = 0.8f;          // tempo antes de escorregar
    public float slideSpeed = 2f;            // velocidade de escorregada
    private float wallTimer;

    [Header("Configurações do Wall Jump")]
    public float wallJumpForceX = 8f;        // força horizontal
    public float wallJumpForceY = 12f;       // força vertical
    public float wallJumpControlLock = 0.15f; // bloqueia controle por alguns ms

    private bool isOnLeftWall;
    private bool isOnRightWall;
 
    private float horizontalInput;
    private float controlLockTimer;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    // usa a mesma Layer de chão (Ground)
    private bool isGrounded;

    [Header("Referências")]
    private Rigidbody2D rb;
    private Animator anim;   // ← ADICIONAR

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>(); // ← ADICIONAR
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        // Detecta chão
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        // Detecta paredes na MESMA layer de chão (GROUND)
        isOnLeftWall = Physics2D.OverlapCircle(wallCheckLeft.position, wallCheckRadius, groundLayer);
        isOnRightWall = Physics2D.OverlapCircle(wallCheckRight.position, wallCheckRadius, groundLayer);

        HandleWallSlide();
        HandleWallJump();
        anim.SetBool("encostarParede", IsTouchingWall());

    }

    bool IsTouchingWall()
    {
        return !isGrounded && (isOnLeftWall || isOnRightWall);
    }

    void HandleWallSlide()
    {
        bool touchingWall = isOnLeftWall || isOnRightWall;

        // Condição para começar a escorregar
        if (!isGrounded && touchingWall && IsPressingTowardsWall())
        {
            wallTimer += Time.deltaTime;

            if (wallTimer >= slideDelay)
            {
                rb.velocity = new Vector2(rb.velocity.x, -slideSpeed);
            }
        }
        else
        {
            wallTimer = 0;
        }
    }

    bool IsPressingTowardsWall()
    {
        return (isOnLeftWall && horizontalInput < 0) ||
               (isOnRightWall && horizontalInput > 0);
    }

    void HandleWallJump()
    {
        if (controlLockTimer > 0)
        {
            controlLockTimer -= Time.deltaTime;
            return;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (!isGrounded && isOnLeftWall)
            {
                WallJump(1);  // pulo para a direita
            }
            else if (!isGrounded && isOnRightWall)
            {
                WallJump(-1); // pulo para a esquerda
            }
        }
    }

    void WallJump(int direction)
    {
        wallTimer = 0;

        rb.velocity = Vector2.zero;

        rb.AddForce(new Vector2(wallJumpForceX * direction, wallJumpForceY), ForceMode2D.Impulse);

        controlLockTimer = wallJumpControlLock;
    }

    void OnDrawGizmosSelected()
    {
        if (wallCheckLeft != null)
            Gizmos.DrawWireSphere(wallCheckLeft.position, wallCheckRadius);

        if (wallCheckRight != null)
            Gizmos.DrawWireSphere(wallCheckRight.position, wallCheckRadius);

        if (groundCheck != null)
            Gizmos.DrawWireSphere(groundCheck.position, groundRadius);
    }
}
