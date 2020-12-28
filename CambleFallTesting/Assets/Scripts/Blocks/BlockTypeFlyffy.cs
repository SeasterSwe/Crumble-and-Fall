using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Fluffys Special ability 
public class BlockTypeFlyffy : BlockType
{
    [Header("Joint")]
    public float breakForce = 15.0f;

    private bool alreadyJointed = false;
    private bool doNotJoint = false;

    protected override void OnHitEnter(Collision2D collision)
    {
        base.OnHitEnter(collision);
        GetComponent<Rigidbody2D>().velocity += Vector2.right * -2;
        GetComponent<Rigidbody2D>().AddTorque(10);

        if (collision.relativeVelocity.magnitude > breakForce && state != states.Projectile)
        {
            BreakJoint();
        }

        if (!doNotJoint)
        {
            CheckSurroundingBlocks();
            StartCoroutine(ResetJointCheckUp());
        }
    }

    private void BreakJoint()
    {
        FixedJoint2D[] joints = gameObject.GetComponents<FixedJoint2D>();
        if (joints.Length > 0)
        {
            foreach (FixedJoint2D joint in joints)
            {
                Destroy(joint);
            }
        }
        StartCoroutine(FreezeJointing());
        
    }

    void CheckSurroundingBlocks()
    {
        Collider2D[] surroundingBlocks = Physics2D.OverlapCircleAll(transform.position, transform.GetComponent<Collider2D>().bounds.extents.magnitude);

        if (surroundingBlocks.Length > 0)
        {
            foreach (Collider2D checkObject in surroundingBlocks)
            {
                CheckBlock(checkObject);
            }
        }
    }

    void CheckBlock(Collider2D otherBlock)
    {
        if (BlockType.IsThisAFluffy(otherBlock.gameObject) && otherBlock.gameObject != gameObject)
        {

            Rigidbody2D thisRB = gameObject.GetComponent<Rigidbody2D>();

            //Check if Otherblock has a connection to this block
            FixedJoint2D[] otherJoints = otherBlock.GetComponents<FixedJoint2D>();
            if (otherJoints.Length > 0)
            {
                foreach (FixedJoint2D otherJoint in otherJoints)
                {
                    if (otherJoint.connectedBody == thisRB)
                    {
                        alreadyJointed = true;
                    }
                }
            }

            //Check if this block has a connection to other block
            FixedJoint2D[] thisJoints = gameObject.GetComponents<FixedJoint2D>();
            if (thisJoints.Length > 0)
            {
                foreach (FixedJoint2D thisJoint in thisJoints)
                {
                    if (thisJoint.connectedBody == otherBlock.GetComponent<Rigidbody2D>())
                    {
                        alreadyJointed = true;
                    }
                }
            }

            //If not already connected, connect
            if (!alreadyJointed)
            {
                FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                joint.connectedBody = otherBlock.gameObject.GetComponent<Rigidbody2D>();
            }
        }
    }

    IEnumerator ResetJointCheckUp()
    {
        yield return new WaitForFixedUpdate();
        alreadyJointed = false;
    }
    IEnumerator FreezeJointing()
    {
        doNotJoint = true;
        yield return new WaitForSeconds(2);
        doNotJoint = false;
        alreadyJointed = false;
    }
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
}
