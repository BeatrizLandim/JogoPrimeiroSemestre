using UnityEngine;

public class WallJump : MonoBehaviour
{
    [Header("Wall Detection")]
    public Transform wallCheck;
    public float wallCheckDistance = 0.2f;
    public LayerMask groundLayer;

    [Header("Wall Slide")]
    public float wallSlideSpeed = 2f;
    private bool isWallSliding;

    [Header("Wall Jump")]
    public float wallJumpForce = 12f;
    public Vector2 wallJumpDirection = new Vector2(1, 1);
    public float wallJumpTime = 0.2f;
    private bool canWallJump;

    [Header("References")]
    private Rigidbody2D rb;
    private Animator anim;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();

        wallJumpDirection.Normalize();
    }

    private void Update()
    {
        HandleWallSlide();
        HandleWallJumpInput();
    }

    void HandleWallSlide()
    {
        bool touchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, groundLayer);
        bool movingDown = rb.velocity.y < 0;

        if (touchingWall && movingDown)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
        }
        else
        {
            isWallSliding = false;
        }

        // ANIMAÇÃO
        anim.SetBool("IsWallSliding", isWallSliding);
    }

    void HandleWallJumpInput()
    {
        if (isWallSliding)
            canWallJump = true;

        if (Input.GetKeyDown(KeyCode.Space) && canWallJump)
        {
            canWallJump = false;

            // força do pulo
            rb.velocity = new Vector2(
                wallJumpDirection.x * -transform.localScale.x * wallJumpForce,
                wallJumpDirection.y * wallJumpForce
            );

            // animação
            anim.SetTrigger("WallJump");
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (wallCheck != null)
            Gizmos.DrawLine(wallCheck.position, wallCheck.position + transform.right * wallCheckDistance);
    }

}
