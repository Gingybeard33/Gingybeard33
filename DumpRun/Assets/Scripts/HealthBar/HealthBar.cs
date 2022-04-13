using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    private Transform bar;
    [SerializeField ] string healthBarName;
    private bool setLeft = false;
    // Start is called before the first frame update
    void Start()
    {
       bar = transform.Find("Bar");
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void changeDirection()
    {
        //float x = bar.localScale.x;
       // float y = bar.localScale.y;
       // this.bar.localScale = new Vector3(-x, y, 1f);
    }
    public void SetSize(float sizeNorm)
    {
        bar.localScale = new Vector3(sizeNorm, 1f);
    }
    public void SetSize(float sizeNorm, bool right)
    {
        if (right)
        {
            bar.localScale = new Vector3(sizeNorm, 1f);
            if (setLeft == true)
            {
                this.transform.localScale = new Vector3(-1 * this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                setLeft = false;
            }
           
        }
        else
        {

           bar.localScale = new Vector3(sizeNorm, 1f);
            if (setLeft == false)
            {
                this.transform.localScale = new Vector3(-1 * this.transform.localScale.x, this.transform.localScale.y, this.transform.localScale.z);
                setLeft = true;
            }

            
        }
       
    }

}
