using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluffyProjectile : MonoBehaviour
{
    public GameObject buildBlock;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        print("hit");
        if (collision.transform.GetComponent<BlockType>())
        {
                print("k");
            if (collision.transform.GetComponent<BlockType>().category == BlockType.categorys.Fluffy)
            {
//                Instantiate(buildBlock, transform.position, transform.rotation, collision.transform);
               // Destroy(gameObject.GetComponent<Rigidbody2D>());
             //   transform.parent = collision.transform;
            }
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
