using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    protected Rigidbody2D rb;
    bool hasHit = false;
    bool hasDoneDmg = false;
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayLaunchSound();
    }

    void Update()
    {
        if (!hasHit)
        {
            RotateWithVelocity();
            rb.freezeRotation = false;
        }
    }

    void RotateWithVelocity()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg; //quickmath
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision.gameObject;
        if (obj.CompareTag("Player") && !hasDoneDmg)
        {
            hasDoneDmg = true;
            obj.GetComponent<CannonHealth>().TakeDmg();
        }
        hasHit = true;
        HitEffekt();
    }

    protected virtual void Stats()
    {

    }

    protected virtual void HitEffekt()
    {

    }

    public virtual void PlayLaunchSound()
    {

    }
}
