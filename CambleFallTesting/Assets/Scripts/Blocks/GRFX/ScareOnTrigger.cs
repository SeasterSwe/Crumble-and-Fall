using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScareOnTrigger : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<BlockExpression>())
        {
            collision.GetComponent<BlockExpression>().SetMoodScared();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<BlockExpression>())
        {
            collision.GetComponent<BlockExpression>().SetMoodIdle();
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
