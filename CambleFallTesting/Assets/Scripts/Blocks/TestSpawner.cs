using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSpawner : MonoBehaviour
{
    public Transform spawnOne;
    public Transform spawnTwo;
    private int stepOne = 0;
    private int stepTwo = 0;
    public int maxStep = 5;

    public int layerOder = 100;
    [Header("SpawnThis")]
    public GameObject baseblock;
 

    // Start is called before the first frame update
    void Start()
    {
             
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("1"))
        {
            SpawnBlock(1, spawnOne.position + Vector3.right * stepOne);
            stepOne = ToggleStep(stepOne);

        }

        if (Input.GetKeyDown("2"))
        {
            SpawnBlock(2, spawnTwo.position + Vector3.right *-stepTwo);
            stepTwo = ToggleStep(stepTwo);
        }
    }

    int ToggleStep(int step)
    {
        int s = step;
        s++;
        if(s > maxStep)
        {
            s = 0;
        }
        return s;
    }

    void SpawnBlock(int team, Vector3 pos)
    {
        GameObject spawn = Instantiate(baseblock, pos, Quaternion.identity);
        spawn.GetComponent<BlockType>().playerteam = team;
        spawn.GetComponent<BlockType>().setCatagoryByNumber((int)Random.Range(0, 3));
        spawn.GetComponent<SpriteRenderer>().sortingOrder = layerOder;
        layerOder--;
    }
}
