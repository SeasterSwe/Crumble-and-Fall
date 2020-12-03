using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTypeSpeedy : BlockType
{
    public GameObject fragment;
    public float minForceToScatter = 10;
    public float reboundForce = 2;
    protected override void OnHitEnter(Collision2D collision)
    {
        print("Hit Force : " + collision.relativeVelocity.magnitude);
        /*
        if (collision.gameObject.GetComponent<BlockType>())
        {
            if(collision.gameObject.GetComponent<BlockType>().playerteam != GetComponent<BlockType>().playerteam)
            {
                //Do your thing.
            }
        }
        */

        if (collision.relativeVelocity.magnitude > minForceToScatter)
        {
            //Reflect and absorb
            if (!GetComponent<Projectile>().isActiveAndEnabled)
            {
                GetComponent<Rigidbody2D>().velocity *= 0;
           
                if (collision.gameObject.GetComponent<Rigidbody2D>())
                {
                    Vector2 velo = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                    velo.x *= -reboundForce;
                    velo.y = Mathf.Abs(velo.y) * reboundForce;
                    collision.gameObject.GetComponent<Rigidbody2D>().velocity = velo;
                }
            }


            print(transform.name + " Is running disabled function");
            Vector2 dir = GetComponent<Rigidbody2D>().velocity;

            for (int i = 0; i < 4; i++)
            {
                Vector3 plusPos = Quaternion.Euler(0, 0, 90 * i) * Vector3.one;
                plusPos.z = 0;
                plusPos *= 0.25f;

                Vector2 scatter = Random.insideUnitSphere;
                scatter += dir;
                GameObject frag = Instantiate(fragment, transform.position + plusPos, transform.rotation);
            }
            Destroy(gameObject);
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
