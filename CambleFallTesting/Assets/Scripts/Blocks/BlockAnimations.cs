using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BlockType))]

public class BlockAnimations : MonoBehaviour
{
    public Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        //bt = GetComponent<BlockType>();
        anim.SetInteger("State", 0);
    }

    // Update is called once per frame
    public void SetAnimation(int i)
    {
        print("Called");
        anim.SetInteger("State", i);
    }
}
