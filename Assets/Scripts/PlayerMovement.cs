using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Variáveis públicas
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    // Crouch
    public Vector2 crouchSize = new Vector2(1f, 0.7f);
    public Vector2 crouchOffset = new Vector2(0f, -0.15f);

    public float inputX;       // direção horizont

    private Vector2 originalSize;
    private Vector2 originalOffset;
    private bool isCrouching = false;

    // Componentes
    public Rigidbody2D rb;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D box;

    private PlayerCrouch crouch;

    public bool isGrounded;

    bool estaDancando = false;

   

    void Start()
    {
        crouch = GetComponent<PlayerCrouch>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        box = GetComponent<BoxCollider2D>();

        // Salva tamanho original do collider
        originalSize = box.size;
        originalOffset = box.offset;
    }

    void Update()
    {
        Crouch();
        UpdateAnimator();
        Movement();
        Jump();
        Attack();
        if (estaDancando)
            return; 
    }

    private void UpdateAnimator()
    {
        animator.SetFloat("Speed", Mathf.Abs(rb.velocity.x));
        animator.SetBool("IsJumping", !isGrounded);
        animator.SetBool("IsCrouching", isCrouching);
    }

    private void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            animator.SetTrigger("Attack");
        }
    }

    private void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && !isCrouching)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    private void Movement()
    {   
    float moveInput = Input.GetAxis("Horizontal");
    float speed = moveSpeed;

    if (crouch != null && crouch.isCrouching)
        speed *= 0.6f;

    rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);
    MirrorSprite(moveInput);
    }

    private void Crouch()
    {
        if (Input.GetKey(KeyCode.S) && isGrounded)
        {
            isCrouching = true;

            // reduzir collider
            box.size = crouchSize;
            box.offset = crouchOffset;
        }
        else
        {
            isCrouching = false;

            // restaurar collider
            box.size = originalSize;
            box.offset = originalOffset;
        }
    }

    private void MirrorSprite(float moveInput)
    {
        if (moveInput > 0) spriteRenderer.flipX = false;
        else if (moveInput < 0) spriteRenderer.flipX = true;
    }

    // Detecta chão
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = true;
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            isGrounded = false;
    }

    public void IniciarDanca()
    {
        estaDancando = true;
        rb.velocity = Vector2.zero;
        animator.SetBool("Dance", true);
    }

    public void PararDanca()
    {
        estaDancando = false;
        animator.SetBool("Dance", false);
    }
}
