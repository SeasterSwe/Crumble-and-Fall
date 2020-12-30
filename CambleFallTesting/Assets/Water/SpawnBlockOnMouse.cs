using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBlockOnMouse : MonoBehaviour
{
    public GameObject[] blocks;
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            Vector2 screenPosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            Vector2 worldPosition = Camera.main.ScreenToWorldPoint(screenPosition);
            int i = Random.Range(0, blocks.Length);
            GameObject cube = Instantiate(blocks[i], worldPosition, blocks[i].transform.rotation);
        }
    }
}
