using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickingBox : MonoBehaviour
{
    private Blockbuilder blockbuilder;
    private GameObject[] chooseBlocks;
    //private Inventory inventory;
    public string pickButton = "VerticalPlayerOne";
    int activeBlock = 0;

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
        if (Input.GetButtonDown(pickButton))
        {
            ChooseBetweenBlocks();
        }
    }

    public void ChooseBetweenBlocks()
    {
        int nextBlock = activeBlock + 1;
        nextBlock = nextBlock % chooseBlocks.Length;
        activeBlock = nextBlock;
        blockbuilder.blockPreFab = chooseBlocks[activeBlock];
    }
}
