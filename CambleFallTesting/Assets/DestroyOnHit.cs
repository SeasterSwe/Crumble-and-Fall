using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnHit : MonoBehaviour
{
    public bool hasDestroyed = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Block"))
        {
            if(!hasDestroyed)
            {
                hasDestroyed = true;
                Destroy(collision.gameObject);       
            }
        }
    }
}
