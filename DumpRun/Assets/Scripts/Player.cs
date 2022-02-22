using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    //Movement
    public Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private int jumpForce;
    [SerializeField] private int wallJumpXForce;
    [SerializeField] private int wallJumpYForce;
    [SerializeField] private int walkSpeed;
    private float horizontalInput;
    private Rigidbody2D rigidBodyComponent;
    

    //Game feeling
    public float hangTime = .1f;
    private float hangCounter;
    public float jumpBuffer = .1f;
    private float jumpBufferCount;
    //Wall jump
    private BoxCollider2D boxCollider;
    private float wallJumpTime = 0.2f;
    private float wallJump;
    private float wallSlideSpeed = 0.5f;
    private bool isWallSliding = false;
    private float wallDistance = .55f;
    private RaycastHit2D wallCheckHit;
  

    //First thing that happens when the game is insialized 
    private void Awake()
    {
        rigidBodyComponent = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello From Start");
        
    }

    // Update is called once per frame
    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
         //late reaction off side of cliff/jumping platform
            if (isGrounded())
            {
                hangCounter = hangTime;
            }
            else
            {
                hangCounter -= Time.deltaTime;
            }

            // jump Buffer
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jumpBufferCount = jumpBuffer;
            }
            else
            {
                jumpBufferCount -= Time.deltaTime;
            }

            //checks for space input and if the player is grounded
            if ((jumpBufferCount >= 0 && hangCounter > 0) || (isWallSliding && Input.GetKeyDown(KeyCode.Space)))
            {
                Jump();
                jumpBufferCount = 0;
            }
            //Small Tap jump
            if (Input.GetKeyUp(KeyCode.Space) && rigidBodyComponent.velocity.y > 0)
            {
                rigidBodyComponent.velocity = new Vector2(rigidBodyComponent.velocity.x, rigidBodyComponent.velocity.y * .5f);
            }

    }

    //called once every physics update
    private void FixedUpdate()
    {
        rigidBodyComponent.velocity = new Vector2(horizontalInput*walkSpeed, rigidBodyComponent.velocity.y);

        //walljump
        //checking right
        if(horizontalInput > 0)
        {
            wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, wallLayer);
        }
        else if(horizontalInput < 0)
        {
            wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, wallLayer);
        }
        
        if (wallCheckHit && !isGrounded())
        {
            isWallSliding = true;
            wallJump = Time.time + wallJumpTime;
        }
        else if (wallJump < Time.time )
        {
            isWallSliding = false;
 
        }

        if (isWallSliding)
        {
            rigidBodyComponent.velocity = new Vector2(rigidBodyComponent.velocity.x, 
                Mathf.Clamp(rigidBodyComponent.velocity.y, -wallSlideSpeed, float.MaxValue));
        }
    }

    //checks if you are on the ground
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, 
            Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }


    //jumps
    private void Jump()
    {



        //rigidBodyComponent.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        if (isGrounded())
        {
            rigidBodyComponent.velocity = new Vector2(rigidBodyComponent.velocity.x, jumpForce);
        }
        else if (isWallSliding && !isGrounded())
        {
            rigidBodyComponent.velocity = new Vector2(rigidBodyComponent.velocity.x, jumpForce);
            //rigidBodyComponent.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpXForce, wallJumpYForce), ForceMode2D.Impulse);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hello from OnTrigger");
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            //trash picked up by player
        }
    }
    
}
