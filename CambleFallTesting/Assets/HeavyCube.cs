using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeavyCube : PlayerProjectile
{
    public override void PlayLaunchSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.HeavyBlockShoot, transform.position);
    }
}
