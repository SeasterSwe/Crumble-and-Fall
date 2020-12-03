using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedyCube : Projectile
{
    public GameObject fragment;
    public float exForce = 5;

    public float velMulti = 1.4f;
    protected override void Start()
    {
        base.Start();
        rb.velocity *= velMulti;
    }

    protected override void HitEffekt(Collision2D collision)
    {
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
            frag.GetComponent<Rigidbody2D>().velocity = scatter * exForce;
        }
        Destroy(gameObject);
    }

    public override void PlayLaunchSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.FastBlockShoot, transform.position);
    }
}
