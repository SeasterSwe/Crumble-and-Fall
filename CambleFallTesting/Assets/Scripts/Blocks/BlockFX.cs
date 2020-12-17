using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockFX : MonoBehaviour
{

    public GameObject FXpreFab;

    public void SpawnFX(Vector2 pos , Vector2 dir)
    {
        GameObject fx = Instantiate(FXpreFab, pos, Quaternion.LookRotation( dir));

        ParticleSystem ps = fx.GetComponentInChildren<ParticleSystem>();
        Destroy(fx, 3);// ps.startLifetime + ps.duration);
    }
}
