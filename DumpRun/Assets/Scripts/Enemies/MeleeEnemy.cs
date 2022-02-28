using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float rangeHeight;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private EnemyPatrol enemyPatrol;

    //Health
    [SerializeField] private float health;
    [SerializeField] private HealthBar healthBar;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        healthBar.SetSize(health);
        cooldownTimer += Time.deltaTime;

        if (PlayerInSightMele())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;

            }
        }
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSightMele();
        }
    }

    private void Awake()
    {
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private bool PlayerInSightMele()
    {
        if (!enemyPatrol.movingLeft)
        {
            RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * rangeHeight, boxCollider.bounds.size.z),
                0, Vector2.left, 0, playerLayer);
            return hit.collider != null;
        }
        else
        {
            RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance*-1,
              new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * rangeHeight, boxCollider.bounds.size.z),
              0, Vector2.left, 0, playerLayer);
            return hit.collider != null;
        }

        
       

    }

    private void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.red;
        if (!enemyPatrol.movingLeft)
        {
            Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * rangeHeight, boxCollider.bounds.size.z));
        }
        else
        {
            Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance*-1,
           new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * rangeHeight, boxCollider.bounds.size.z));
        }
        */
    }

    private void DamagePlayer()
    {
        if (PlayerInSightMele())
        {
            //give damage to player
        }

    }

    private void TakeDamage(float damage)
    {
        health = health - damage;
    }
}
