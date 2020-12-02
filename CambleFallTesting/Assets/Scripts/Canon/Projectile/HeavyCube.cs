using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyCube : Projectile
{
    public override void PlayLaunchSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.HeavyBlockShoot, transform.position);
    }
}
