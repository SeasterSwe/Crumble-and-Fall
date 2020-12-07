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
        if (collision.relativeVelocity.magnitude > minForceToScatter)
        {
            ReflectForce(collision);
            Fragmentize();
        }
    }

    void OLDVelocityChange(Collision2D collision)
    {
        print("Hit Force : " + collision.relativeVelocity.magnitude);
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
    }

    void ReflectForce(Collision2D collision)
    {
        //Reflect the force of colliding object;
        if (!GetComponent<Projectile>().isActiveAndEnabled)
        {
            GetComponent<Rigidbody2D>().velocity *= 0;

            if (collision.gameObject.GetComponent<Rigidbody2D>())
            {
                //Reflect force of speedy
                Vector2 velo = collision.gameObject.GetComponent<Rigidbody2D>().velocity;
                Vector2 normal = collision.gameObject.GetComponent<Collider2D>().ClosestPoint(collision.otherCollider.transform.position);
                normal -= new Vector2(collision.transform.position.x, collision.transform.position.y);
                normal = normal.normalized;

                Vector2.Reflect(velo, normal);
                collision.gameObject.GetComponent<Rigidbody2D>().velocity = velo;
                Debug.DrawRay(collision.transform.position, normal, Color.red, 0.5f);
            }
        }

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }
    void Fragmentize()
    {
        //Split block into 4
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
