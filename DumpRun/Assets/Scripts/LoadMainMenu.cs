using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class LoadMainMenu : MonoBehaviour
{

    [SerializeField] Player player;
    [SerializeField] MeleeEnemy enemy;
    private SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }



    // Update is called once per frame
    void Update()
    {
        if (enemy.health <=0)
        {
            sr.color = new Color(0f, 128f, 0f);
        }

        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Collision");
        if (collision.gameObject.layer == 6)
        {
            if (enemy.health <= 0)
            {
                SceneManager.LoadScene("MainMenu");
            }
        }
    }
}
