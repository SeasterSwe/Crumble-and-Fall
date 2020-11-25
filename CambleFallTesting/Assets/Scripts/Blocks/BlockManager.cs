
//Robert S
//TODO: Singelton?
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour
{
    public static List<GameObject> blocks;
    //public static LinkBlocks linkBlocks;
    public static bool updateLinks = false;
    void Awake()
    {
        //linkBlocks = FindObjectOfType<LinkBlocks>();
        blocks = new List<GameObject>();
        RefreshListFromScene();
    }

    private void Update()
    {
        if (updateLinks)
        {
            //linkBlocks.CheckLinks();
            updateLinks = false;
            //print("Linkupdated to " + blocks.Count);
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
        BlockType[] blocksInScene = FindObjectsOfType<BlockType>();
    }

    public static void CheckAllLinks()
    {
        //linkBlocks.CheckLinks();
    }
}
