using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeightChecking : MonoBehaviour
{
    public GameObject playerCannon;
    public Transform leftEdge;
    public Transform rightEdge;

    private Vector3[] raycastPos;
    public GameObject obj;
    public int amountOfPointsToCheck;

    private Vector3 activePos;
    public float checkRate = 0.3f;
    private float nextCheck = 0;
    public bool spawnVizualizeObj;
    public bool leftPlayer;

    private void Start()
    {
        raycastPos = new Vector3[amountOfPointsToCheck];
        float distBetwean = Vector3.Distance(leftEdge.position, rightEdge.position + Vector3.one);
        distBetwean = distBetwean / amountOfPointsToCheck;

        for (int i = 0; i < amountOfPointsToCheck; i++)
        {
            float x = leftEdge.position.x + (distBetwean * i);
            raycastPos[i] = new Vector3(x, leftEdge.position.y, 0);

            if (spawnVizualizeObj)
            {
                GameObject rayPosVizualize = Instantiate(obj, raycastPos[i], Quaternion.identity);
            }
        }
        activePos = playerCannon.transform.position;
    }
    void Update()
    {
        if (nextCheck < Time.time)
        {
            nextCheck = checkRate + Time.time;
            CheckForHighestPoint();
        }
    }

    void CheckForHighestPoint()
    {
        activePos = playerCannon.transform.position;
        Vector3 tempV3 = Vector3.one * -10;
        for (int i = 0; i < raycastPos.Length; i++)
        {
            RaycastHit2D hit = Physics2D.Raycast(raycastPos[i], -Vector2.up);
            if (hit.point.y > tempV3.y && hit.collider.CompareTag("Block")) //compareTag
            {
                if (!leftPlayer)
                    tempV3 = hit.collider.gameObject.transform.position + Vector3.up; //Fråga inte
                else
                    tempV3 = hit.collider.gameObject.transform.position;
            }
        }

        if (tempV3.y != activePos.y) //tempV3.y != activePos.y
        {
            activePos = tempV3;
            if (leftPlayer)
                playerCannon.transform.position = activePos + Vector3.up; //Fråga inte
            else
                playerCannon.transform.position = activePos;
            SwapEffekt();
        }

    }

    void SwapEffekt()
    {

    }
}
