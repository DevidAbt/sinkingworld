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

    private bool doubleJumped = false;
    public float timeSpentJumping = 0f;
    public float doubleJumpStartTime = 1f;



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

        bool grounded = Physics2D.OverlapCircle(groundCheck.position,
                                                groundRadius,
                                                whatIsGround);

        if (Input.GetButtonDown("Jump") && (grounded || !doubleJumped))
        {
            timeSpentJumping += Time.deltaTime;
            rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
            Debug.Log(timeSpentJumping);
            if (timeSpentJumping > doubleJumpStartTime && !grounded)
            {
                doubleJumped = true;
                animator.SetTrigger("DoubleJump");
            }
        }

        if (grounded)
        {
            doubleJumped = false;
            timeSpentJumping = 0;
        }

        animator.SetFloat("Speed", Mathf.Abs(this.rb.velocity.x));
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
