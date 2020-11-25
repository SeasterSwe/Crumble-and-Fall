using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockList : MonoBehaviour
{
    public GameObject[] buildingBlocks;
    public GameObject[] playerShoots;
    public static GameObject[] buildList;
    private static GameObject[] shootList;
    private void Awake()
    {
        buildList = buildingBlocks;
        shootList = playerShoots;
    }
    public static GameObject GetARandomBlock()
    {
        int r = Random.Range(0, buildList.Length);
        return buildList[r];
    }
    public static GameObject GetARandomPlayerShoot()
    {
        int r = Random.Range(0, shootList.Length);
        return shootList[r];
    }
}
