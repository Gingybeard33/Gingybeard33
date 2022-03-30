using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public Animator animator;
    
    //Movement
    public Transform groundCheckTransform;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask playerMask;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private int jumpForce;
    [SerializeField] private int wallJumpXForce;
    [SerializeField] private int wallJumpYForce;
    [SerializeField] private int walkSpeed;
    [SerializeField] private int wallJumpImpulse;
    private bool isMovingLeft = false;
    private bool wallOnLeft = false;
    private bool wallOnRight = false;
    private float horizontalInput;
    private Rigidbody2D rigidBodyComponent;


    private bool HasDoubleJump = true;
    

    //Game feeling
    public float hangTime = .1f;
    private float hangCounter;
    public float jumpBuffer = .1f;
    private float jumpBufferCount;
    //Wall jump
    private CapsuleCollider2D boxCollider;
    //private BoxCollider2D boxCollider;
    private float wallJumpTime = 0.2f;
    private float wallJump;
    private float wallSlideSpeed = 0.5f;
    private bool isWallSliding = false;
    private float wallDistance = .55f;
    private RaycastHit2D wallCheckHit;

    //Health
    [SerializeField] private float health;
    [SerializeField] private HealthBar healthBar;

    public static int trashCollected = 0;

    //First thing that happens when the game is insialized 
    private void Awake()
    {
        rigidBodyComponent = GetComponent<Rigidbody2D>();
       //boxCollider = GetComponent<BoxCollider2D>();
        boxCollider = GetComponent<CapsuleCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
        healthBar.SetSize(1f);
        Debug.Log("Hello From Start");
        
    }

    // Update is called once per frame
    private void Update()
    {
        if (health <= 0)
        {

            int scene = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadScene(scene, LoadSceneMode.Single);
        }
        healthBar.SetSize(health);
        horizontalInput = Input.GetAxis("Horizontal");
         //late reaction off side of cliff/jumping platform

        //flip the player based on direction
         if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        }
        else if (horizontalInput < -0.0f)
        {
            transform.localScale = new Vector3(-0.5f, 0.5f, 0.5f);
        }
            if (isGrounded())
            {
                hangCounter = hangTime;
                HasDoubleJump = true;
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
            /*
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
            */

            if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

          
        if (rigidBodyComponent.velocity.x > 0)
        {
            isMovingLeft = false;
           // Debug.Log("Moving right");
        }
        else
        {
              isMovingLeft = true;
            // Debug.Log("Moving left");
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
            //wall on right
            if (wallCheckHit == true)
            {
                wallOnRight = true;
                wallOnLeft = false;
                Debug.Log("WallOnRight");
            }
            else
            {
                wallOnRight = false;
                wallOnLeft = false;
            }

        }
        else if(horizontalInput < 0)
        {
            wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, wallLayer);
            //wall on left
            if (wallCheckHit == true)
            {
                wallOnRight = false;
                wallOnLeft = true;
                Debug.Log("WallOnLeft");
            }
            else
            {
                wallOnRight = false;
                wallOnLeft = false;
            }
        }
        else //horizontal input == 0
        {
            //check for wall on right
            wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(-wallDistance, 0), wallDistance, wallLayer);
            if (wallCheckHit)
            {
                wallOnLeft = true;
                wallOnRight = false;
                HasDoubleJump = true;
            }
            else if(!wallCheckHit)
            {
                //check for wall on left now
               wallCheckHit = Physics2D.Raycast(transform.position, new Vector2(wallDistance, 0), wallDistance, wallLayer);
                 //wall on left
               if (wallCheckHit)
                {
                    wallOnRight = true;
                    wallOnLeft = false;
                    HasDoubleJump = true;   
                }
               //wall not on right or left
                else
                {
                    wallOnRight = false;
                    wallOnLeft = false;
                }

            } 

           
        }
        
        if (wallCheckHit && !isGrounded())
        {
            isWallSliding = true;
            wallJump = Time.time + wallJumpTime;
            Debug.Log("WallSLiding");
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
           // rigidBodyComponent.velocity = new Vector2(rigidBodyComponent.velocity.x, jumpForce);


            if (wallOnLeft)
            {
                rigidBodyComponent.velocity = new Vector2( wallJumpImpulse, jumpForce);
                Debug.Log("WallJumpToTheRIght");
               // rigidBodyComponent.AddForce(new Vector2(wallJumpImpulse, 0), ForceMode2D.Impulse);
            }
            else if(wallOnRight)
            {
                rigidBodyComponent.velocity = new Vector2(-wallJumpImpulse, jumpForce);
                Debug.Log("WallJumpToTheLeft");
                //rigidBodyComponent.AddForce(new Vector2(-wallJumpImpulse, 0), ForceMode2D.Impulse);
            }
            //rigidBodyComponent.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpXForce, wallJumpYForce), ForceMode2D.Impulse);
        }
        else if (!isGrounded())
        {
            if (HasDoubleJump)
            {
                Debug.Log("DoubleJumping");
                rigidBodyComponent.velocity = new Vector2(rigidBodyComponent.velocity.x, jumpForce);
                if (!isWallSliding)
                {
                    HasDoubleJump = false;
                }

            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Hello from OnTrigger");
        if (other.gameObject.layer == 7)
        {
            Destroy(other.gameObject);
            //trash picked up by player
            // health = health - 0.1f;
            trashCollected++;
        }
        if (other.gameObject.layer == 13)
        {
            health = 1.0f;
            Destroy(other.gameObject);
        }
        //if object is enemy projectile, take damage
    }


    public void TakeDamage(float damage)
    {
        health = health - damage;
    }

    public void SetHealth(float hp)
    {

        health = hp;
    }

     /*
     // From Tutorial Video - Working movement and jumping
     // 
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;

    private bool jumpKeyPressed;
    private float horizontalInput;
    private Rigidbody2D rigidBodyComp;

    private void Start()
    {
        rigidBodyComp = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            jumpKeyPressed = false;
            Debug.Log("Space Released");
        }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jumpKeyPressed = true;
            Debug.Log("Space Pressed");
        }

        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        rigidBodyComp.velocity = new Vector2(horizontalInput, rigidBodyComp.velocity.y);

        if (Physics2D.OverlapCircleAll(groundCheckTransform.position, 0.1f).Length == 2)
        {
            return;
        }

        if (jumpKeyPressed)
        {
            rigidBodyComp.AddForce(Vector3.up * 3, ForceMode2D.Impulse);
        }
    } 
     */
}
