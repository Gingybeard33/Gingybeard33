using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;         // speed of the platform
    public int startingPoint;   // starting index (position of the platform)
    public Transform[] points;  // An array of transform points (positions where the platform needs to move)

    private int i;              //index of the array
    private GameObject emptyObject;

    // Start is called before the first frame update
    void Start()
    {
        transform.position = points[startingPoint].position;
        
    }

    // Update is called once per frame
    void Update()
    {
        //Update which point the platform is moving towards once we're close enough to the point
        if (Vector2.Distance(transform.position, points[i].position) < 0.02f )
        {
            i++;

            if (i == points.Length)
                i = 0;
        }

        transform.position = Vector2.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
    }

    // Set player to be child of a empty object, which is a child of the platform
    //
    // A child takes on the transforms of its immediate parent, so by using a empty
    // gameobject, which has no transformation changes, the player will not scale
    // with the tranforms of the platform (which causes visual bugs)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        emptyObject = new GameObject("emptyObject");

        emptyObject.transform.SetParent(transform);
        collision.transform.SetParent(emptyObject.transform);
    }

    // Remove player as child when they leave the platform & delete empty object
    private void OnCollisionExit2D(Collision2D collision)
    {
         collision.transform.SetParent(null);
         Object.Destroy(emptyObject);


    }
}
