using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sludge : MonoBehaviour
{

    [SerializeField] Player player;

    private bool isInside;
    private float damageDelayCount = 0.5f;
    [SerializeField] private float currTime;
    [SerializeField] private float damage;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (isInside)
        {
            if (currTime > damageDelayCount)
            {
                player.TakeDamage(damage);
                currTime = 0;
            }
            else
            {
                currTime += Time.deltaTime;
            }
        }
        else
        {
            currTime += Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //player.TakeDamage(0.1f);
        if ((collision.gameObject.layer == 6) && (isInside == false))
        {
            isInside = true;
            player.TakeDamage(damage);
            currTime = 0;
        }
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {

            isInside = false;
        }
    }
}
