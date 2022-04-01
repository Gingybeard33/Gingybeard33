using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sounds : MonoBehaviour
{

    [SerializeField] public  AudioSource playerShoot;
    [SerializeField] public  AudioSource playerMelee;
    [SerializeField] public  AudioSource EnemyMelee;
    [SerializeField] public  AudioSource EnemyShoot;
    [SerializeField] public  AudioSource TrashCollected;
    //[SerializeField] public static AudioSource playerMelee;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayPlayerShoot()
    {
        playerShoot.Play();
    }
    public void PlayPlayerMelee()
    {
        playerMelee.Play();
    }
    public void PlayEnemyShoot()
    {
        EnemyShoot.Play();
    }
    public void PlayEnemyMelee()
    {
        EnemyMelee.Play();
    }
    public void PlayTrashCollected()
    {
        TrashCollected.Play();
    }


}
