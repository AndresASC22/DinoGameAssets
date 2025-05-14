using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CavemanController : MonoBehaviour
{

    // Initial declarations for the Caveman movement and animations
    public float speed;
    public float xMotion;
    private Rigidbody2D body;
    public float jumpForce;
    public float jumpVelocity;
    bool isJumping;
    private Animator anim;
    public bool isGrounded;
    public static bool facingRight;
    private SpriteRenderer rend;
 
    //I NEED THE FOLLOWING FOR GROUND CHECK!!!
    public Transform groundCheck;
    public LayerMask groundLayers;
    private float groundCheckRadius;

    //EWEWEWEWWEWE
    public AudioSource music;
    public AudioSource sfx;
    public AudioClip[] soundEffects;

    //Instantiate or Grab things from the GameObject
    void Start()
    {
        speed = 5.0f;
        jumpForce = 2f;
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        rend = GetComponent<SpriteRenderer>();
        isJumping = false;
        groundCheckRadius = .1f;
        jumpVelocity = 10f;
        facingRight = true;        
    }

    // Update is called once per frame
    // Makes it CPU dependent
    // It is called "60" times per second
    // Controls.
    void Update()
    {

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayers);
        //anim.SetBool("isGrounded", isGrounded);

        //Is the player moving left or right?
        xMotion = Input.GetAxis("Horizontal");  //-1, 0, or 1        

        if (xMotion > 0f || xMotion < 0f)
        {
            //anim.SetBool("isIdle", false);
            //We are moving - but which way should we be facing??
            if ((facingRight && xMotion < 0f) || (!facingRight && xMotion > 0f))
            {
                //rend.flipX = !rend.flipX;
                //facingRight = !facingRight;
                Flip();
            }
        } else
        {
            //anim.SetBool("isIdle", true);
        }
    }

    void FixedUpdate()
    {
        //Time.deltaTime is super important and super neecessary
        //It smoothes out the motion regardless of what type of machine you are using
        Vector3 motion = new Vector3(speed * xMotion * Time.deltaTime, 0f, 0f);
        //anim.SetFloat("velocity", motion.x);  //nice idea
        Vector3 position = this.transform.position;
        position += motion;
        this.transform.position = position;

        if (Input.GetAxis("Jump") > 0 && isGrounded)
        {
            body.velocity += new Vector2(0f, jumpVelocity);
        }
        

    }
    public void Flip()
    {
        Vector3 localScale = this.transform.localScale;
        localScale.x *= -1f;
        this.transform.localScale = localScale;
        facingRight = !facingRight;
    }
}
