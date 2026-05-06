using UnityEngine;

public class WallMovement : MonoBehaviour
{
    [Header("Checagem de Paredes")]
    public Transform wallCheckLeft;
    public Transform wallCheckRight;
    public float wallCheckRadius = 0.2f;
    public LayerMask groundLayer;

    [Header("Configurações do Wall Slide")]
    public float slideDelay = 0.8f;
    public float slideSpeed = 2f;
    private float wallTimer;

    [Header("Configurações do Wall Jump")]
    public float wallJumpForceX = 8f;
    public float wallJumpForceY = 12f;
    public float wallJumpControlLock = 0.15f;

    private bool isOnLeftWall;
    private bool isOnRightWall;

    private float horizontalInput;
    private float controlLockTimer;

    [Header("Ground Check")]
    public Transform groundCheck;
    public float groundRadius = 0.2f;
    private bool isGrounded;

    [Header("Referências")]
    private Rigidbody2D rb;
    private Animator anim;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, groundLayer);

        isOnLeftWall = Physics2D.OverlapCircle(wallCheckLeft.position, wallCheckRadius, groundLayer);
        isOnRightWall = Physics2D.OverlapCircle(wallCheckRight.position, wallCheckRadius, groundLayer);

        HandleWallSlide();
        HandleWallJump();

        // Atualiza animação
        anim.SetBool("encostarParede", IsTouchingWall());
    }

    bool IsTouchingWall()
    {
        return !isGrounded && (isOnLeftWall || isOnRightWall);
    }

    void HandleWallSlide()
    {
        bool touchingWall = isOnLeftWall || isOnRightWall;

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
                WallJump(1);
            }
            else if (!isGrounded && isOnRightWall)
            {
                WallJump(-1);
            }
        }
    }

    void WallJump(int direction)
    {
        wallTimer = 0;

        // 🔥 FORÇA SAÍDA IMEDIATA DA PAREDE
        isOnLeftWall = false;
        isOnRightWall = false;

        // 🔥 ZERA VELOCIDADE PRA NÃO HERDAR SLIDE
        rb.velocity = Vector2.zero;

        // 🔥 APLICA FORÇA
        rb.AddForce(new Vector2(wallJumpForceX * direction, wallJumpForceY), ForceMode2D.Impulse);

        // 🔥 FLIP EXATO NO MOMENTO DO PULO
        transform.localScale = new Vector3(direction, 1, 1);

        // 🔥 FORÇA ANIMAÇÃO A SAIR DA PAREDE
        anim.SetBool("encostarParede", false);
        anim.SetTrigger("pulo"); // cria esse trigger no Animator

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