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
    [SerializeField] public AudioSource Title;
    [SerializeField] public AudioSource hitSound;
    [SerializeField] public AudioSource HP;

    /*
    [SerializeField] public AudioSource lvl1;
    [SerializeField] public AudioSource lvl2;
    [SerializeField] public AudioSource lvl3;
    [SerializeField] public AudioSource lvl4;
    [SerializeField] public AudioSource lvl42;
    [SerializeField] public AudioSource lvl5;
    */

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

    public void PlayTitle()
    {
        Title.Play();
    }
    public void StopTitle()
    {
        Title.Stop();
    }
    public void PlayHitSound()
    {
        hitSound.Play();
    }

    public void PlayHP()
    {
        HP.Play();
    }


    /*

    public void Playlvl1()
    {
        lvl1.Play();
    }
    public void Stoplvl1()
    {
       lvl1.Stop();
    }


    public void Playlvl2()
    {
        lvl2.Play();
    }
    public void Stoplvl2()
    {
        lvl2.Stop();
    }


    public void Playlvl3()
    {
        lvl3.Play();
    }
    public void Stoplvl3()
    {
        lvl3.Stop();
    }



    public void Playlvl4()
    {
        lvl4.Play();
    }
    public void Stoplvl4()
    {
        lvl4.Stop();
    }



    public void Playlvl5()
    {
        lvl5.Play();
    }
    public void Stoplvl5()
    {
        lvl5.Stop();
    }



   */




}
