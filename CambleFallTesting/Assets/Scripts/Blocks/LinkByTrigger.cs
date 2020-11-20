using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkByTrigger : MonoBehaviour
{
    private bool addForce;
    private Vector2 addForceDir;
    public float Forcemul = 2;
    public float maxRangeOfTrigger;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<BlockType>())
        {
            if(collision.GetComponent<BlockType>().category == gameObject.GetComponent<BlockType>().category)
            {
                Vector2 dir = collision.transform.position - transform.position;
                addForceDir += dir;
                addForce = true;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        maxRangeOfTrigger *= maxRangeOfTrigger;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (addForce)
        {
            if (gameObject.GetComponent<Rigidbody2D>())
            {
                gameObject.GetComponent<Rigidbody2D>().AddForce(addForceDir.normalized * Forcemul);
                Debug.DrawRay(transform.position - Vector3.forward, addForceDir.normalized , Color.green);
                addForceDir = Vector2.zero;
                addForce = false;
            }
        }
            
    }
}
