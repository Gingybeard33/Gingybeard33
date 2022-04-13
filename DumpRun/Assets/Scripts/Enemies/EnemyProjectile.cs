using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float lifetime;

    [SerializeField] private Player player;
    [SerializeField] private float damage;
    private BoxCollider2D coll;

    private Vector3 direction;

    public float angle;

    private bool hit;

    private void Awake()
    {
       
        coll = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifetime = 0;
        gameObject.SetActive(true);
        coll.enabled = true;
        Vector3 heading = new Vector3(-1*(player.transform.position.x - transform.position.x), player.transform.position.y - transform.position.y, 0f);
        //int distance = heading.magnitude;
        direction = heading.normalized;

        Debug.Log(direction);
    }
    private void Update()
    {
        if (hit) return;
        float movementSpeed = speed * Time.deltaTime;

        //fix with angle
        transform.Translate(movementSpeed*direction.x, movementSpeed*direction.y, 0);

        lifetime += Time.deltaTime;
        if (lifetime > resetTime)
            gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if (collision.gameObject.layer == 11)
        {
            return;
        }
        if (collision.gameObject.layer == 8)
        {
            return;
        }
        if(collision.gameObject.layer == 15)
        {
            return;
        }
        if (collision.gameObject.layer == 6)
        {
            Player playerHit = collision.GetComponent<Player>();
            player.TakeDamage(damage);

         
        }
        if (collision.gameObject.layer == 7)
        {
            return;
        }
        hit = true;
        //Execute logic from parent script first
        coll.enabled = false;
        gameObject.SetActive(false); //When this hits any object deactivate arrow
        

    }
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
