using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Transform groundCheckTransform;
    [SerializeField] private LayerMask playerMask;
    private bool jumpKeyPressed = false;
    private float horizontalInput;
    private Rigidbody2D rigidBodyComponent;
    private bool isGrounded;
    
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello From Start");
        rigidBodyComponent = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void Update()
    {
        //checks for space input and if the player is grounded
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
    
        }
        horizontalInput = Input.GetAxis("Horizontal");



    }

    //called once every physics update
    private void FixedUpdate()
    {
        /*
       if (Physics.OverlapSphere(groundCheckTransform.position, 0.1f, playerMask).Length == 0)
        {
            return;
        }
        */


       


     /*  if (jumpKeyPressed == true)
       {
            rigidBodyComponent.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
            jumpKeyPressed = false;
       }*/
      

        rigidBodyComponent.velocity = new Vector2(horizontalInput*5, rigidBodyComponent.velocity.y);


    }
    //checks to see if the charcter is on the ground
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
            isGrounded = true;
    }

    //jumps
    private void Jump()
    {
        rigidBodyComponent.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        isGrounded = false;
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
