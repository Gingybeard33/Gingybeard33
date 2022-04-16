using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{

    [SerializeField] public sounds allSounds;

    public void PlayGame()
    {
        //allSounds.StopTitle();
        SceneManager.LoadScene("Level1");
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    // Start is called before the first frame update
    void Start()
    {
        Player.trashCollected = 0;
        //allSounds.PlayTitle();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
