using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedEnemy : MonoBehaviour
{

    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float damage;
    [SerializeField] private float rangeHeight;

    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] fireball;

    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [SerializeField] private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private Player player;
    [SerializeField] private EnemyPatrol enemyPatrol;

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
        if (health <= 0)
        {
            Destroy(gameObject);
            return;
        }


        healthBar.SetSize(health);
        cooldownTimer += Time.deltaTime;

        if (PlayerInSightShooting())
        {
            if (cooldownTimer >= attackCooldown)
            {
                //attack
                cooldownTimer = 0;
                rangedAttack();

            }
        }
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSightShooting();
        }
    }

    private void rangedAttack()
    {
        cooldownTimer = 0;
        fireball[FindFireball()].transform.position = firepoint.position;
        fireball[FindFireball()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindFireball()
    {
        for (int i = 0; i < fireball.Length; i++)
        {
            if(!fireball[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private void Awake()
    {
       // enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private bool PlayerInSightShooting()
    {

        
        if (enemyPatrol.movingLeft)
        {
            RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance * -1,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * rangeHeight, boxCollider.bounds.size.z),
                0, Vector2.left, 0, playerLayer);
            return hit.collider != null;
        }
        else if (!enemyPatrol.movingLeft)
        {
            RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance * 1,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * rangeHeight, boxCollider.bounds.size.z),
                0, Vector2.left, 0, playerLayer);
            return hit.collider != null;
        }
        return false;

        



    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
        if (enemyPatrol.movingLeft)
        {
            Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance * -1,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * rangeHeight, boxCollider.bounds.size.z));
        }
        else
        {

            Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance * 1,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * rangeHeight, boxCollider.bounds.size.z));
        }
        

    }


    public void TakeDamage(float damage)
    {
        health = health - damage;
    }
}
