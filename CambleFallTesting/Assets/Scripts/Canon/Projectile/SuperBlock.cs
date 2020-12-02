using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuperBlock : MonoBehaviour
{
    public enum types { Fluffy, Speedy, Heavy};
    public types type;
    public bool projectile = false;
    public LayerMask projectileLayer;
    public LayerMask blockLayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (projectile)
        {
            ProjectileUpdate();
        }
        else
        {
            BlockUpdate();
        }
    }

    void ProjectileUpdate()
    {
        switch (type)
        {
            case types.Fluffy:
                {
                    //So Fluffy Stuff
                }
                break;
            case types.Speedy:
                {
                    //Do Speedy stuff
                }
                break;
            case types.Heavy:
                { //Do Heavy stuff
                }
                break;
            default:
                {
                    //do Nothing
                }
                break;
        }
    }

    void BlockUpdate()
    {

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        switch (type)
        {
            case types.Fluffy:
                {
                    FluffyCollision(collision.gameObject);
                }
                break;
            case types.Speedy:
                {
                    //Do Speedy stuff
                }
                break;
            case types.Heavy:
                { //Do Heavy stuff
                }
                break;
            default:
                {
                    //do Nothing
                }
                break;
        }
    }

    void FluffyCollision(GameObject objectCollidedWith)
    {
        if (objectCollidedWith.GetComponent<SuperBlock>())
        {
            if (objectCollidedWith.GetComponent<SuperBlock>().type == types.Fluffy)
            {
                if (!gameObject.GetComponent<FixedJoint2D>())
                {
                    FixedJoint2D joint = gameObject.AddComponent<FixedJoint2D>();
                    joint.connectedBody = objectCollidedWith.GetComponent<Rigidbody2D>();
                }
            }
        }
    }
}
