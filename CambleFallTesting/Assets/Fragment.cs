using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fragment : MonoBehaviour
{
    bool collided = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        var obj = collision.gameObject;
        var type = obj.GetComponent<BlockType>();
        if (type != null && type.state != BlockType.states.Projectile)
        {
            collided = true;
        }
        else if (obj.gameObject.CompareTag("Player") && !collided)
        {
            collided = true;
            obj.GetComponent<CannonHealth>().TakeDmg(0.3f);
        }
    }
}
