//Robban
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BlockType : MonoBehaviour
{
    public enum categorys {Fluffy, Speedy, Heavy}
    public int playerteam = 1;
    public categorys category = categorys.Fluffy;


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(category == categorys.Fluffy)
        {
            if (collision.transform.GetComponent<BlockType>())
            {
                if(collision.transform.GetComponent<BlockType>().category == BlockType.categorys.Fluffy)
                {
                    FixedJoint2D fixedJoint = collision.gameObject.AddComponent<FixedJoint2D>();
                    fixedJoint.connectedBody = transform.GetComponent<Rigidbody2D>();
                }
            }
        }
        //FluffySpecial.updateLink = true;
    }
   

    // Start is called before the first frame update
    void Start()
    {
        //setColorByCategory();
        BlockManager.AddBlockToList(gameObject);
    }

    void Update()
    {
        //colUpt = false;
    }

    public void setCatagoryByNumber(int n)
    {
        switch (n)
        {
            case 1:
                {
                    category = categorys.Fluffy;
                }
                break;

            case 2:
                {
                    category = categorys.Speedy;
                }
                break;

            case 3:
                {
                    category = categorys.Heavy;
                }
                break;

            default:
                {
                    category = categorys.Speedy;
                    n = 2;
                }
                break;
        }
    }

    private void OnDestroy()
    {
        BlockManager.RemoveBlockFromList(gameObject);
    }
}
