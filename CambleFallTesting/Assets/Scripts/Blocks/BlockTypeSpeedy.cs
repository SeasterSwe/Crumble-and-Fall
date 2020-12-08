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
        Vector3 pos = transform.position;
        if (collision.relativeVelocity.magnitude > minForceToScatter)
        {
            ReflectForce(collision);
            Fragmentize();
        }
        transform.position = pos;

    }

    void ReflectForce(Collision2D collision)
    {
        //Reflect the force of colliding object;
        if (!GetComponent<Projectile>().isActiveAndEnabled)
        {
            if (collision.gameObject.GetComponent<Rigidbody2D>())
            {
                collision.otherCollider.gameObject.GetComponent<Rigidbody2D>().velocity *= -1;

                print("HIT " + collision.otherCollider.GetComponent<Rigidbody2D>().velocity);
                
                //Reflect force of speedy
                Vector2 velo = collision.otherCollider.GetComponent<Rigidbody2D>().velocity;
                Vector2 normal = collision.otherCollider.GetComponent<Collider2D>().ClosestPoint(collision.otherCollider.transform.position);
                normal -= new Vector2(collision.otherCollider.transform.position.x, collision.otherCollider.transform.position.y);
                normal = normal.normalized;

                Vector2.Reflect(velo, normal);
                collision.otherCollider.gameObject.GetComponent<Rigidbody2D>().velocity = velo;
                Debug.DrawRay(collision.transform.position, normal*50, Color.red, 10.5f);
            }
        }
        GetComponent<Rigidbody2D>().velocity *= 0;
        print("Hit " + GetComponent<Rigidbody2D>().velocity);

    }
    void Fragmentize()
    {
        //Split block into 4
        for (int i = 0; i < 4; i++)
        {
            Vector3 plusPos = Quaternion.Euler(0, 0, 90 * i) * Vector3.one;
            plusPos.z = 0;
            plusPos *= 0.25f;

            GameObject frag = Instantiate(fragment, transform.position + plusPos, transform.rotation);
        }
        Destroy(gameObject);
    }

}
