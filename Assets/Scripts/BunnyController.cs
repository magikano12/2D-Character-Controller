using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyController : MonoBehaviour
{
    
    public float maxSpeed = 10f;
    bool facingRight = true;

    Animator anim;

    bool grounded = false;
    public Transform groundCheck;
    float groundRadius = 0.2f;
    public LayerMask whatIsGround;

    public float jumpForce = 700;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {

        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        anim.SetBool("Ground", grounded);

        anim.SetFloat("vSpeed", rb2d.velocity.y);

        float move = Input.GetAxis("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(move));     
        rb2d.velocity= new Vector2(move * maxSpeed, rb2d.velocity.y);

        if(move>0&&!facingRight)
        {
            Flip();
        }

        else if(move<0&&facingRight)
        {
            Flip();
        }
	}
    private void Update()
    {
        Rigidbody2D rb2d = GetComponent<Rigidbody2D>();
        if (grounded&&Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("Ground", false);
            rb2d.AddForce(new Vector2(0, jumpForce));
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
