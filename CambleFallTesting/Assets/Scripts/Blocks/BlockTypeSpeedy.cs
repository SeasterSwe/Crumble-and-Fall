using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTypeSpeedy : BlockType
{
    [Header("Speedy Settings")]
    public GameObject fragment;
    public float minForceToBreak = 10;

    [Header("SpeedyProjectile Settings")]
    public float scatterForce = 5;

    private Vector2 normal;
    private Vector3 lastPos;
    public GameObject particle;
    public GameObject reflect;

    protected override void OnHitEnter(Collision2D collision)
    {

        if (collision.relativeVelocity.magnitude > minForceToBreak && state == states.Idle)
        {
            ReflectForce(collision);
            Scatter();

        }
        base.OnHitEnter(collision);
    }

    protected override void UpdateEachFrame()
    {
        base.UpdateEachFrame();

        if (state == states.Projectile)
        {
            if (transform.position.x > 0 && playerteam == 1)
            {
                print("Past 0");
                ScatterProjectile();
            }
            else if (transform.position.x < 0 && playerteam == 2)
            {
                ScatterProjectile();
            }
        }

        lastPos = transform.position;
    }

    void ReflectForce(Collision2D collision)
    {
        //Reflect the force of colliding object;
        if (collision.collider.gameObject.GetComponent<Rigidbody2D>())
        {
            Debug.DrawRay(collision.collider.transform.position, Vector2.up * 10, Color.white, 10);
            if (collision.collider.GetComponent<BlockType>())
            {
                if (!collision.collider.GetComponent<BlockType>().hitThisFrame)
                {
                    normal += collision.contacts[0].normal * -1;
                    Vector2 velo = collision.collider.gameObject.GetComponent<Rigidbody2D>().velocity;
                    velo = Vector3.Reflect(velo, normal.normalized);
                    collision.collider.gameObject.GetComponent<Rigidbody2D>().velocity = velo;


                    Debug.DrawRay(collision.collider.transform.position, collision.collider.GetComponent<Rigidbody2D>().velocity, Color.green, 1);
                    Debug.DrawRay(collision.collider.transform.position, normal * 5, Color.red, 10.5f);
                    Debug.DrawRay(collision.collider.transform.position, collision.collider.gameObject.GetComponent<Rigidbody2D>().velocity, Color.blue, 2);


                    collision.collider.GetComponent<BlockType>().hitThisFrame = true;
                }
            }
        }

        collision.otherCollider.GetComponent<Rigidbody2D>().velocity *= 0;
    }

    void Scatter()
    {
        //fx
        var rotation = Quaternion.Euler(0, 90, 0);
        if(transform.position.x > 0)
            rotation = Quaternion.Euler(0, -90, 0);
        GameObject particleClone = Instantiate(reflect, transform.position, rotation);

        //Split block into 4
        for (int i = 0; i < 4; i++)
        {
            Vector3 plusPos = Quaternion.Euler(0, 0, 90 * i) * Vector3.one;
            plusPos.z = 0;
            plusPos *= 0.25f;
            GameObject frag = Instantiate(fragment, lastPos + plusPos, transform.rotation);
        }
        Destroy(gameObject);
    }

    void ScatterProjectile()
    {
        // print(transform.name + " Fragmentet as projectile");
        Vector2 dir = GetComponent<Rigidbody2D>().velocity;

        for (int i = 0; i < 4; i++)
        {
            Vector3 plusPos = Quaternion.Euler(0, 0, 90 * i) * Vector3.one;
            plusPos.z = 0;
            plusPos *= 0.25f;

            //fx
            GameObject particleClone = Instantiate(particle, lastPos + plusPos, particle.transform.rotation);
            Vector2 scatter = Random.insideUnitSphere * scatterForce;
            scatter += dir;
            GameObject frag = Instantiate(fragment, transform.position + plusPos, transform.rotation);
            frag.GetComponent<Rigidbody2D>().velocity = scatter;
        }
        Destroy(gameObject);
    }
}
