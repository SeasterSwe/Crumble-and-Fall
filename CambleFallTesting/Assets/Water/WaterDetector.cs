using UnityEngine;
using System.Collections;

public class WaterDetector : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D Hit)
    {
        var rb = Hit.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            transform.parent.GetComponent<DynamicWater>().Splash(transform.position.x, rb.velocity.y * (rb.mass) / 40f);
        }
    }
}