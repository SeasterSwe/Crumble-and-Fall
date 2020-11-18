using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBall : MonoBehaviour
{
    Rigidbody2D rb;

    [Header("Egenskaper")]
    public bool destroyHitBlock;
    public bool explode;
    public float blastRadius = 5;
    public float blastPower = 10;
    public GameObject fx;

    public bool selfDestruct;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (explode)
        {
            //Explode();
            Collider2D[] blastObjects = Physics2D.OverlapCircleAll(transform.position, blastRadius);
            foreach (Collider2D col in blastObjects)
            {
                Rigidbody2D otherRb = col.GetComponent<Rigidbody2D>();
                if (otherRb != null)
                {
                    Vector2 dir = otherRb.position - rb.position;
                    float dist = blastRadius - dir.magnitude;
                    dist = Mathf.Clamp(dist, 0, blastRadius);
                    otherRb.AddForce(dir.normalized * dist * blastPower, ForceMode2D.Impulse);
                }
            }

            GameObject vfx = Instantiate(fx, transform.position + Vector3.forward * blastRadius, transform.rotation);
            vfx.transform.localScale = Vector3.one * blastRadius;
            Destroy(vfx, 0.2f);
        }
        if (destroyHitBlock)
        {
            if (collision.transform.tag != "Player")
            {
                Transform p = collision.transform.parent;
                if (collision.transform.childCount > 0)
                {
                    if (p != null)
                    {
                        for (int c = 0; c < collision.transform.childCount; c++)
                        {
                            collision.transform.GetChild(c).parent = p;
                        }
                    }
                    else
                    {
                        for (int c = 1; c < collision.transform.childCount; c++)
                        {
                            collision.transform.GetChild(c).parent = collision.transform.GetChild(0);
                        }

                        Transform newP = collision.transform.GetChild(0);
                        newP.parent = null;
                        newP.gameObject.AddComponent<Rigidbody2D>();
                        newP.gameObject.GetComponent<Rigidbody2D>().mass = 1 + newP.childCount;
                        Debug.DrawRay(newP.position, Vector3.up * 0.25f, Color.red, 1);
                    }
                }
                Destroy(collision.gameObject);
            }
        }
        if (selfDestruct)
        {
            Destroy(gameObject);
        }
    }

    IEnumerator Explode()
    {
        yield return new WaitForFixedUpdate();

     
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent< Rigidbody2D>();
     //   rb.velocity = transform.right * speed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (rb.IsSleeping())
        {
            Destroy(GetComponent<CircleCollider2D>());
        }
    }
}
