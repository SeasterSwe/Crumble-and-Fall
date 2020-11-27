using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FastCube : Projectile
{
    public float velMulti = 1.4f;
    protected override void Start()
    {
        base.Start();
        rb.velocity *= velMulti;
    }
    public override void PlayLaunchSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.FastBlockShoot, transform.position);
    }
}
