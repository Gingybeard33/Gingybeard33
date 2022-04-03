using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinalCutSceneText : MonoBehaviour
{
    private float timeCoeff = 2;
    private float timePassed = 0;
    private float text1 = 1 * 2;
    private float text2 = 3 * 2;
    private float text3 = 5 * 2;
    private float text4 = 6 * 2;
    private float text5 = 8 * 2;
    private float text6 = 9 * 2;
    private float textTrashCollected = 2 * 2;
    private float textPoundsCollected = 4 * 2;
    private float textYears = 7 * 2;

    private float loadMM = 15* 2;
    [SerializeField] public TMPro.TextMeshProUGUI t1;
    [SerializeField] public TMPro.TextMeshProUGUI t2;
    [SerializeField] public TMPro.TextMeshProUGUI t3;
    [SerializeField] public TMPro.TextMeshProUGUI t4;
    [SerializeField] public TMPro.TextMeshProUGUI t5;
    [SerializeField] public TMPro.TextMeshProUGUI t6;
    [SerializeField] public TMPro.TextMeshProUGUI TotalTrashCollected;
    [SerializeField] public TMPro.TextMeshProUGUI TotalPounds;
    [SerializeField] public TMPro.TextMeshProUGUI Years;
    private int trashCollected;
    // Start is called before the first frame update
    void Start()
    {
        trashCollected = Player.trashCollected;
        if (trashCollected == 0)
        {
            trashCollected = 1;
        }

        t1.gameObject.SetActive(false);
        t2.gameObject.SetActive(false);
        t3.gameObject.SetActive(false);
        t4.gameObject.SetActive(false);
        t5.gameObject.SetActive(false);
        t6.gameObject.SetActive(false);
        TotalTrashCollected.gameObject.SetActive(false);
        TotalPounds.gameObject.SetActive(false);
        Years.gameObject.SetActive(false);


        setTotalTrashText();
        setTotalPoundTrashText();
        setYears();
    }

    // Update is called once per frame
    void Update()
    {
        if ((timePassed > textTrashCollected) )
        {
            //TotalTrashCollected.enabled = true;
            TotalTrashCollected.gameObject.SetActive(true);
        }
        if ((timePassed > textPoundsCollected))
        {
            //TotalPounds.enabled = true;
           TotalPounds.gameObject.SetActive(true);
        }
        if ((timePassed > textYears))
        {
            //Years.enabled = true;
            Years.gameObject.SetActive(true);
        }
        if ((timePassed > text1))
        {
            // t1.enabled = true;
            t1.gameObject.SetActive(true);
        }

        if ((timePassed > text2) )
        {
            // t2.enabled = true;
            t2.gameObject.SetActive(true);
        }

        if ((timePassed > text3) )
        {
            // t1.enabled = true;
            t3.gameObject.SetActive(true);
        }

        if ((timePassed > text4) )
        {
            //  t4.enabled = true;
            t4.gameObject.SetActive(true);
        }
        if ((timePassed > text5) )
        {
            // t5.enabled = true;
            t5.gameObject.SetActive(true);
        }

        if ((timePassed > text6) )
        {
            // t6.enabled = true;
            t6.gameObject.SetActive(true);
        }

        if ((timePassed > loadMM))
        {
            SceneManager.LoadScene("MainMenu");
        }

        timePassed += Time.deltaTime;
    }

    private void setTotalTrashText()
    {
        TotalTrashCollected.text = trashCollected.ToString();
    }

    private float lbs = 0;

    private float collected = 1;
    private void setTotalPoundTrashText()
    {
        collected = Player.trashCollected;
        if (collected == 0)
        {
            collected = 1;
        }

        lbs = collected / 12;
        TotalPounds.text = lbs.ToString();
    }

    private void setYears()
    {
        float totalGamesPlayed = 2700000000 / lbs;

        float totalYearsPlaying = totalGamesPlayed / 35040;

        Years.text = totalYearsPlaying.ToString() + "  Years";

    }


}
