using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public string category;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCatagoryByNumber(int n)
    {
        if (n == 1)
        {
            category = "Green";
        }
        else if (n == 2)
        {
            category = "Blue";
        }
        else
        {
            category = "Red";
        }
        setColorByCategory();
    }
    public void setColorByCategory()
    {
        if (category == "Green")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if (category == "Blue")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if (category == "Yellow")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            category = "Red";
        }
    }
}
