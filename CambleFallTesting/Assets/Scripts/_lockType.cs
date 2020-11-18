using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockType : MonoBehaviour
{
    public string catagory = "Red";


    // Start is called before the first frame update
    void Start()
    {
        if(catagory == "Green")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        }
        else if(catagory == "Blue")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.blue;
        }
        else if(catagory == "Yellow")
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.yellow;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            catagory = "Red";
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
