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
    private float wallJumpCooldown;

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
        //wall jumping
        /*if (wallJumpCooldown > 0.2f)
        {*/


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
            if (jumpBufferCount >= 0 && hangCounter > 0)
            {
                Jump();
                jumpBufferCount = 0;
            }
            //Small Tap jump
            if (Input.GetKeyUp(KeyCode.Space) && rigidBodyComponent.velocity.y > 0)
            {
                rigidBodyComponent.velocity = new Vector2(rigidBodyComponent.velocity.x, rigidBodyComponent.velocity.y * .5f);
            }

    /*}
        else
            wallJumpCooldown += Time.deltaTime;*/

    }

    //called once every physics update
    private void FixedUpdate()
    {
        rigidBodyComponent.velocity = new Vector2(horizontalInput*walkSpeed, rigidBodyComponent.velocity.y);
    }

    //checks if you are on the ground
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, 
            Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

   /* //checks if you are on a wall
    private bool onWall()
    {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0,
            new Vector2(transform.localScale.x, 0), 0.5f, wallLayer);
        return raycastHit.collider != null;
    }*/

    //jumps
    private void Jump()
    {
        if (isGrounded())
        {
            Debug.Log("Jumping");

            rigidBodyComponent.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
        /*else if (onWall() && !isGrounded())
        {
            Debug.Log("Wall Jump");

            wallJumpCooldown = 0;
            rigidBodyComponent.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpXForce, wallJumpYForce), ForceMode2D.Impulse);
        }*/
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
