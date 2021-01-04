using UnityEngine;
using System.Collections;

public class WaterDetector : MonoBehaviour
{
    public int index;
    void OnTriggerEnter2D(Collider2D Hit)
    {
        var rb = Hit.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            if (rb.velocity.magnitude > 0.5f)
                transform.parent.GetComponent<Test2>().Splash(index, -rb.velocity.magnitude * (rb.mass) / 40f);
        }
    }
}