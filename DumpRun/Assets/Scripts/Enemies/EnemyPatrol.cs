using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [SerializeField] private Transform enemy;



    [SerializeField] private float speed;
    [SerializeField] private float idleDuration;
    private float idleTimer;

    private Vector3 initScale;
  
    public bool movingLeft;

    private void Awake()
    {
        initScale = enemy.localScale;
         
    }
    // Start is called before the first frame update
    void Start()
    {
       // movingLeft = false;
    }

    // Update is called once per frame
    void Update()
    {
        try
        {
            if (movingLeft)
            {
                if (enemy.position.x >= leftEdge.position.x)
                {
                    MoveInDirection(-1);
                   
                }
                else
                {
                    DirectionChange();
                    
                }
            }
            else
            {
                if (enemy.position.x <= rightEdge.position.x)
                {
                    MoveInDirection(1);
                }
                else
                {
                    DirectionChange();
                   
                }
            }
        }
        catch
        {
            return;
        }
        
    }

    private void OnDisable()
    {
        
    }

    private void DirectionChange()
    {
        idleTimer += Time.deltaTime;

        if (idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
            enemy.localScale = new Vector3((enemy.localScale.x * -1), initScale.y, initScale.z);
        }

    }

    private void MoveInDirection(int direction)
    {
        idleTimer = 0;
        //enemy.localScale = new Vector3(Mathf.Abs(initScale.x * direction), initScale.y, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }
}
