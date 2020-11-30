using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluffyCube : Projectile
{
    public override void PlayLaunchSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.NormalBlockShoot, transform.position);
    }
}
