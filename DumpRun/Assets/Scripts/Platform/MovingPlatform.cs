using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed;         // speed of the platform
    public int startingPoint;   // starting index (position of the platform)
    public Transform[] points;  // An array of transform points (positions where the platform needs to move)

    private int i;              //index of the array

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

    // Set player to be child of platform, so it moves with it
    private void OnCollisionEnter2D(Collision2D collision)
    {
        collision.transform.SetParent(transform);
    }

    // Remove player as child when they leave the platform
    private void OnCollisionExit2D(Collision2D collision)
    {
        collision.transform.SetParent(null);
    }
}
