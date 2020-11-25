//Robban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockType : MonoBehaviour
{
    public string category = "Red";


    // Start is called before the first frame update
    void Start()
    {
        if(category == "Green")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if(category == "Blue")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if(category == "Yellow")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            category = "Red";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
