using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 5f;
    public float jumpForce = 7f;
    public static bool facingRight = true;

    private Rigidbody2D rb;
    private Animator animator;

    public Transform groundCheck;
    public float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public float timeSpentJumping = 0f;
    public float doubleJumpStartTime = 1f;
    private JumpStatus jumpStatus;
    private enum JumpStatus
    {
        READY,
        JUMPED,
        DOUBLE_JUMPED
    }



    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        float horiz = Input.GetAxis("Horizontal");

        rb.velocity = new Vector2(horiz * speed, rb.velocity.y);

        if (facingRight && horiz < 0 || !facingRight && horiz > 0)
        {
            flip();
        }

        animator.SetFloat("Speed", Mathf.Abs(this.rb.velocity.x));

        bool grounded = Physics2D.OverlapCircle(groundCheck.position,
                                                groundRadius,
                                                whatIsGround);

        if (grounded)
        {
            this.jumpStatus = JumpStatus.READY;
            timeSpentJumping = 0;
            Debug.Log("Grounded");
        }

        if (Input.GetButtonDown("Jump") && (jumpStatus == JumpStatus.READY || jumpStatus == JumpStatus.JUMPED))
        {
            if (jumpStatus == JumpStatus.JUMPED)
            {
                if (facingRight)
                {

                    animator.SetTrigger("RollRight");
                }
                else
                {

                    animator.SetTrigger("RollLeft");
                }
                jumpStatus = JumpStatus.DOUBLE_JUMPED;
                Debug.Log("DoubleJumped: " + timeSpentJumping);
            }
            else
            {
                jumpStatus = JumpStatus.JUMPED;
                Debug.Log("Jumped: " + timeSpentJumping);
            }
            timeSpentJumping += Time.deltaTime;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
        }
        animator.SetBool("Grounded", grounded);
    }

    private void flip()
    {
        facingRight = !facingRight;

        Vector3 newLocalScale = transform.localScale;
        newLocalScale.x *= -1;
        transform.localScale = newLocalScale;
    }
}
