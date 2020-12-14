using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksInWater : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            rb.velocity *= 0.25f;
            rb.gravityScale = 0.75f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Rigidbody2D>())
        {
            collision.gameObject.GetComponent<Rigidbody2D>().gravityScale = 1.0f;
        }
    }
}
