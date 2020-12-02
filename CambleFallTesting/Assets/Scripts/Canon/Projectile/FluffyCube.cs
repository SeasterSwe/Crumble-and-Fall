using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluffyCube : Projectile
{
    public override void PlayLaunchSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.NormalBlockShoot, transform.position);
    }

    protected override void HitEffekt(Collision2D collision)
    {
        //base.HitEffekt(collision);
        if (BlockType.IsFluffy(collision.gameObject))
        {
            print("Hit another fluffy");
            if (gameObject.GetComponent<FixedJoint2D>())
            {
                FixedJoint2D joint = collision.gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = gameObject.GetComponent<Rigidbody2D>();
            }
            else
            {
                FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
            }
        }
    }
}
