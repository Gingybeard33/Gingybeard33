using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    public Animator animator;
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float rangeHeight;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float damage;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;
    
    private float cooldownTimer = Mathf.Infinity;
    [SerializeField] private Player player;
    
    [SerializeField] private EnemyPatrol enemyPatrol;
    //Health
    [SerializeField] public float health;
    [SerializeField] private HealthBar healthBar;

    private bool movingRight = true;

    [SerializeField] bool isBoss = false;

    public bool isNewspaper = false;
    public bool isSlime = false;
    public bool isPlastic = false;
    public bool isCloud = false;

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

        if (movingRight)
        { healthBar.SetSize(health, true); }
        else
        {
            healthBar.SetSize(health, false);
        }
        cooldownTimer += Time.deltaTime;
        if ((cooldownTimer >= attackCooldown))
        {
            //Debug.Log("Can idle animation");
           
        }
        if (PlayerInSightMele())
        {
            if (isNewspaper)
            {
                if (cooldownTimer >= attackCooldown)
                {
                    //attack
                    Debug.Log("Can Attacking");
                    animator.SetBool("Attacking", false);
                    animator.SetBool("Attacking", true);
                    cooldownTimer = 0;
                    // DamagePlayer();

                }
                else if (cooldownTimer >= 6)
                {
                    animator.SetBool("Attacking", false);
                }
            }
            else if (isSlime)
            {
                if (cooldownTimer >= attackCooldown)
                {
                    //attack
                    Debug.Log("Can Attacking");
                    animator.SetBool("Attacking", false);
                    animator.SetBool("Attacking", true);
                    cooldownTimer = 0;
                    // DamagePlayer();

                }
                else if (cooldownTimer >= 1)
                {
                    animator.SetBool("Attacking", false);
                }
            }
            else if (isPlastic)
            {
                if (cooldownTimer >= attackCooldown)
                {
                    //attack
                    Debug.Log("Plastic Attacking");
                    animator.SetBool("Attacking", false);
                    animator.SetBool("Attacking", true);
                    cooldownTimer = 0;
                    // DamagePlayer();

                }
                else if (cooldownTimer >= 1)
                {
                    animator.SetBool("Attacking", false);
                }
            }
            else if (isCloud)
            {
                if (cooldownTimer >= attackCooldown)
                {
                    //attack
                    Debug.Log("Plastic Attacking");
                    animator.SetBool("Attacking", false);
                    animator.SetBool("Attacking", true);
                    cooldownTimer = 0;
                    // DamagePlayer();

                }
                else if (cooldownTimer >= 1)
                {
                    animator.SetBool("Attacking", false);
                }
            }
            else
            {
                if (cooldownTimer >= attackCooldown)
                {
                    //attack
                    Debug.Log("Can Attacking");
                    animator.SetBool("Attacking", false);
                    animator.SetBool("Attacking", true);
                    cooldownTimer = 0;
                    // DamagePlayer();

                }
                else if (cooldownTimer >= 0.5)
                {
                    animator.SetBool("Attacking", false);
                }
            }
            
            
        }
        else
        {
            animator.SetBool("Attacking", false);
        }
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSightMele();
        }
        if ((enemyPatrol.movingLeft == true) && (movingRight == true))
        {
           //healthBar.changeDirection();
            movingRight = false;
        }
        if ((enemyPatrol.movingLeft == false) && (movingRight == false))
        {

           // healthBar.changeDirection();
            movingRight = true;
        }
    }

    private void Awake()
    {
       //enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private bool PlayerInSightMele()
    {
        //if (enemyPatrol.movingLeft)
        {
            RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance * -1,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * rangeHeight, boxCollider.bounds.size.z),
                0, Vector2.left, 0, playerLayer);
            return hit.collider != null;
        }
        /*
        else if (!enemyPatrol.movingLeft)
        {
            RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance * 1,
                new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * rangeHeight, boxCollider.bounds.size.z),
                0, Vector2.left, 0, playerLayer);
            return hit.collider != null;
        }
        */
        return false;





    }

    public void FrameAttackPlayer()
    {
        if (PlayerInSightMele())
        {
            DamagePlayer();
        }


    }

    public void SlashSound()
    {
        player.allSounds.PlayEnemyMelee();
        
    }

    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.red;
       // if (enemyPatrol.movingLeft)
        {
            Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance * -1,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * rangeHeight, boxCollider.bounds.size.z));
        }
        /*
        else
        {

            Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance *1,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y * rangeHeight, boxCollider.bounds.size.z));
        }
        */
        
        
    }

    private void DamagePlayer()
    {
        if (PlayerInSightMele())
        {
            //give damage to player
            player.TakeDamage(damage);
        }

    }

    public void TakeDamage(float damage)
    {
        if (!isBoss)
        {
            health = health - damage;
        }
        else
        {
            health = health - damage/4;
        }
        
    }
}
