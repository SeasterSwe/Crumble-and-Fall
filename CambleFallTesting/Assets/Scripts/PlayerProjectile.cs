using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    Rigidbody2D rb;
    bool hasHit;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!hasHit)
        {
            RotateWithVelocity();
        }
    }

    void RotateWithVelocity()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg; //quickmath
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hasHit = true;
    }
}
