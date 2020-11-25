using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickingBox : MonoBehaviour
{
    private Blockbuilder blockbuilder;
    private GameObject[] chooseBlocks;
    //private Inventory inventory;

    int ActiveBlock = 0;

    // Start is called before the first frame update
    void Start()
    {
        chooseBlocks = BlockList.buildList;
        blockbuilder = GetComponent<Blockbuilder>();
        //inventory = GetComponent<Inventory>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.W))
        {
            ChooseBetweenBlocks();
        }
    }

    public void ChooseBetweenBlocks()
    {
        int nextBlock = ActiveBlock + 1;
        nextBlock = nextBlock % chooseBlocks.Length;
        ActiveBlock = nextBlock;
        blockbuilder.blockPreFab = chooseBlocks[ActiveBlock];
    }
}
