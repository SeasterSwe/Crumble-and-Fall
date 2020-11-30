using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FluffyCube : Projectile
{
    private void Start()
    {
        //GetComponent<Rigidbody2D>().AddTorque(1000, ForceMode2D.Impulse);
    }
    public override void PlayLaunchSound()
    {
        SoundManager.PlaySound(SoundManager.Sound.NormalBlockShoot, transform.position);
    }
}
