//Robert S
//TODO: Replace this with more efficent. This script loops trough all blocks.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkBlocks : MonoBehaviour
{
    public float linkAtDist = 1.01f;

    public void CheckLinks()
    {
        RemoveAllLinks();
        SetNewLinks();
    }

    void RemoveAllLinks()
    {
        for (int a = 0; a < BlockManager.blocks.Count; a++)
        {
            BlockManager.blocks[a].transform.parent = null;
        }
    }

    void SetNewLinks()
    {
        for (int a = 0; a < BlockManager.blocks.Count; a++)
        {
            for (int b = 0; b < BlockManager.blocks.Count; b++)
            {
                Transform myBlock = BlockManager.blocks[a].transform;
                Transform otherBlock = BlockManager.blocks[b].transform;

                string myCategory = myBlock.GetComponent<BlockType>().category;
                string otherCatagory = otherBlock.GetComponent<BlockType>().category;
                if (myCategory == otherCatagory)
                {
                    if ((myBlock.position - otherBlock.position).sqrMagnitude < linkAtDist)
                    {
                        if (myBlock.transform.parent == null)
                        {
                            otherBlock.transform.parent = myBlock;
                        }
                        else
                        {
                            otherBlock.transform.parent = myBlock.transform.parent;
                        }
                    }
                }
            }
        }

      //  yield return null;
        
        for(int c = 0; c < BlockManager.blocks.Count; c++)
        {
            Transform t = BlockManager.blocks[c].transform;
            if (t.parent == null)
            {
                if (t.GetComponent<Rigidbody2D>())
                {
                    t.GetComponent<Rigidbody2D>().mass = t.childCount+1;
                }
                else
                {
                    Rigidbody2D rbT = t.gameObject.AddComponent<Rigidbody2D>();
                    rbT.mass = t.childCount+1;
                }
            }
            else
            {
                if (t.GetComponent<Rigidbody2D>())
                {
                    //t.rotation = t.parent.rotation;
                    Vector3 alignPos = t.parent.InverseTransformPoint(t.transform.position);
                    alignPos *= 2;
                    alignPos = new Vector3(Mathf.Round(alignPos.x), Mathf.Round(alignPos.y), alignPos.z);
                    alignPos *= 0.5f;

                    t.position = t.parent.TransformPoint(alignPos);
                    
                    Destroy(t.GetComponent<Rigidbody2D>());
                }
            }
        }
    }
    /* TODO: Remove
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("l"))
        {
            CheckLinks();
        }
    }
    */
}
