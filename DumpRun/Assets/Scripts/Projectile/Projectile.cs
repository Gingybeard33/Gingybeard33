using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;

    private BoxCollider2D boxCollider;
    private Animator anim;

    [SerializeField] private float damage;
     private float lifeTime;

    private float direction;
    // Start is called before the first frame update
    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(hit)
            return;
        float movementSpeed = speed * Time.deltaTime * direction * 2;
        transform.Translate(movementSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if(lifeTime > .1)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
        //explode
        //do damage to enemy we contacted
        if (!(collision.gameObject.layer == 7) && !(collision.gameObject.layer == 11) && !(collision.gameObject.layer == 15))
        {
            if (collision.gameObject.layer == 8)
            {

                try
                {
                    MeleeEnemy enemy = collision.gameObject.GetComponent<MeleeEnemy>();
                    enemy.TakeDamage(damage);
                }
                catch
                {
                    RangedEnemy enemy = collision.gameObject.GetComponent<RangedEnemy>();
                    enemy.TakeDamage(damage);
                }
               
            }
            hit = true;
            boxCollider.enabled = false;
            deactivate();

        }


    }

    public void SetDirection(float _direction)
    {
        lifeTime = 0;
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localtScaleX = transform.localScale.x;
        if (Mathf.Sign(localtScaleX) != _direction)
        {
            localtScaleX = -localtScaleX;
        }

        transform.localScale = new Vector3(localtScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void deactivate()
    {
        gameObject.SetActive(false);
    }

}
