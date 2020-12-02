using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTypeFlyffy : BlockType
{
    /*
    protected override void OnInsideTrigger(Collider2D collider)
    {
        base.OnInsideTrigger(collider);
        Vector2 dir = collider.transform.position - transform.position;
        float dist = dir.sqrMagnitude -1;

        GetComponent<Rigidbody2D>().AddForce(dir.normalized * 5 * dist);
    }
    */
    protected override void OnHitEnter(Collision2D collision)
    {
        base.OnHitEnter(collision);
        //base.HitEffekt(collision);
        if (BlockType.IsFluffy(collision.gameObject))
        {
            print("Hit another fluffy");
            if (gameObject.GetComponent<FixedJoint2D>())
            {
                FixedJoint2D joint = collision.gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = gameObject.GetComponent<Rigidbody2D>();
            }
            else
            {
                FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();
            }
        }
    }

}
