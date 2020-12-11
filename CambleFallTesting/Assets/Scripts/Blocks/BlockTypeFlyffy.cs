using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockTypeFlyffy : BlockType
{
    public GameObject[] boundTo;

    protected override void OnHitEnter(Collision2D collision)
    {
        base.OnHitEnter(collision);
        if (BlockType.IsThisAFluffy(collision.gameObject))
        {
            if (!hitThisFrame)
            {
                FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = collision.gameObject.GetComponent<Rigidbody2D>();

                collision.gameObject.GetComponent<BlockType>().hitThisFrame = true;
                hitThisFrame = true;
            }
        }
    }

    /*
    protected override void StateChangedToIdle()
    {
        base.StateChangedToIdle();
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }

    protected override void StateChagedToProjectile()
    {
        base.StateChagedToProjectile();
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }
    */
}
