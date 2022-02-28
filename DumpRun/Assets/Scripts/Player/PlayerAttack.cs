using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] projectiles;
    private Animator anim;
    private Player player;


    [SerializeField] private float cooldownTimer = Mathf.Infinity;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0) && cooldownTimer > attackCooldown)
        {
            Attack();
        }
        cooldownTimer += Time.deltaTime;
    }

    private void Attack()
    {
        //play animation for attack
        cooldownTimer = 0;
        //int bullet = FindFireball();
        projectiles[FindFireball()].transform.position = firePoint.position;
        projectiles[FindFireball()].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireball()
    {
        for (int i =0; i < projectiles.Length; i ++)
        {
            if (!projectiles[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }
}
