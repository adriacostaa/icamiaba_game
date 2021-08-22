using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed;
    public float jumpForce;
    public Transform ceilingCheck;
    public Transform groundCheck;
    public LayerMask groundObjects;
    public float checkRadius;
    public int maxJumpCount;


    private Rigidbody2D rb;
    private bool facingRight = true;
    private float moveDirection;
    private bool isJumping = false;
    private bool isGrounded;
    private int jumpCount;


    //Awake called after all objects are initialized. Called in a random order.
    private void Awake(){
        rb = GetComponent<Rigidbody2D>();

    }

    private void Start(){
        jumpCount = maxJumpCount;
    }

    // Update is called once per frame
    void Update()
    {
        //Get Inpus
        ProcessInputs();

        //Animate (ao movimentar o player altera-se a face do sprite)
        Animate();
    }

    private void FixedUpdate(){
        //check ground
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, groundObjects); 
        if(isGrounded){
            jumpCount = maxJumpCount;
        }

        Move();
    }

    private void Move()
    {
        rb.velocity = new Vector2(moveDirection * moveSpeed, rb.velocity.y);
        if(isJumping){
            rb.AddForce(new Vector2(0f, jumpForce));
            jumpCount--;
        }
        isJumping = false;
    }

    private void Animate()
    {
        if (moveDirection > 0 && !facingRight)
        {
            FlipCharacter();
        }
        else if (moveDirection < 0 && facingRight)
        {
            FlipCharacter();
        }
    }

    private void ProcessInputs()
    {
        moveDirection = Input.GetAxis("Horizontal");//Scale of -1 -> 1
        if(Input.GetButtonDown("Jump") && (jumpCount > 0)){
            isJumping = true;
        }
    }

    private void FlipCharacter(){
        facingRight = !facingRight;
        transform.Rotate(0f,180f,0f);
    }
}
