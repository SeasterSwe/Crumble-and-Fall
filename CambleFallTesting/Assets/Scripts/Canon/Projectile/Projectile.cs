using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    int playerTeam = 0;
    protected Rigidbody2D rb;
    bool hasHit = false;
    bool hasDoneDmg = false;

    bool isProjectile = false;
    protected virtual void Start()
    {
        print("Start has run");
        rb = GetComponent<Rigidbody2D>();
        PlayLaunchSound();

        if (transform.position.x < 0)
            playerTeam = 1;
        else
            playerTeam = 2;
    }

    void Update()
    {
        if (!hasHit)
        {
           // RotateWithVelocity();
           // rb.freezeRotation = false;
        }

        if(playerTeam == 1)
        {
            if(transform.position.x > 0)
            {
                //past halfway
                PastHalfWay();
            }
        }
        else
        {
            if(transform.position.x < 0)
            {
                //past halfway1
                PastHalfWay();
            }
        }
    }

    protected virtual void PastHalfWay()
    {

    }

    void RotateWithVelocity()
    {
        float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg; //quickmath
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (this.enabled)
        {
            print("Has Collided " + transform.name);
            var obj = collision.gameObject;
            if (obj.CompareTag("Player") && !hasDoneDmg)
            {
                hasDoneDmg = true;
                obj.GetComponent<CannonHealth>().TakeDmg();
            }
            else if (!obj.CompareTag("Block"))
            {

            }
            else
            {
                gameObject.AddComponent<VelocityTest>();
                gameObject.layer = 8; //blocks
                gameObject.tag = "Block";
                Destroy(GetComponent<Projectile>());
            }
            hasHit = true;
            HitEffekt(collision);
        }
    }

    protected virtual void Stats()
    {

    }

    protected virtual void HitEffekt(Collision2D collision)
    {
        
    }

    public virtual void PlayLaunchSound()
    {

    }
}
