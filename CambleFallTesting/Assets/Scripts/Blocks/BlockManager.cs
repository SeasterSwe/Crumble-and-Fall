
//Robert S
//TODO: Singelton?
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static List<GameObject> blocks;
    public static List<Transform> blockParents;

    public static LinkBlocks linkBlocks;

    public static bool updateLinks = false;

    private int upLinks = 0;
    void Awake()
    {
        linkBlocks = FindObjectOfType<LinkBlocks>();
        blocks = new List<GameObject>();
        RefreshListFromScene();
    }

    private void Update()
    {
        if (updateLinks)
        {
            linkBlocks.CheckLinks();
            updateLinks = false;
            upLinks++;
            print("Linkupdated " + upLinks);
        }
    }

    public static void AddBlockToList(GameObject blockToAdd)
    {
        blocks.Add(blockToAdd);
    }
    public static void RemoveBlockFromList(GameObject blockToRemove)
    {
        blocks.Remove(blockToRemove);
    }

    public static void RefreshListFromScene()
    {
        blocks = new List<GameObject>();
        blockParents = new List<Transform>();
        BlockType[] blocksInScene = FindObjectsOfType<BlockType>();
        for(int i = 0; i < blocksInScene.Length; i++)
        {
            /*
            blocks.Add(blocksInScene[i].gameObject);
            if(blocksInScene[i].transform.parent == null)
            {
                blockParents.Add(blocksInScene[i].transform);
            }
            */
        }
    }

    public static void CheckAllLinks()
    {
        linkBlocks.CheckLinks();
    }
}
