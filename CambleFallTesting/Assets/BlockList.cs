using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockList : MonoBehaviour
{
    public GameObject[] blocks;
    private static GameObject[] list;
    private void Awake()
    {
        list = blocks;
    }
    public static GameObject GetARandomBlock()
    {
        int r = Random.Range(0, list.Length);
        return list[r];
    }
}
