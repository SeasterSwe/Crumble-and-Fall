using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTest : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(GetImpactForce(collision) > 200)
        {
            var a = GetComponent<Rigidbody2D>();
            a.freezeRotation = false;
        }
    }
    public static float GetImpactForce(Collision2D collision)
    {
        float impulse = 0F;

        foreach (ContactPoint2D point in collision.contacts)
        {
            impulse += point.normalImpulse;
        }

        return impulse / Time.fixedDeltaTime;
    }
}
