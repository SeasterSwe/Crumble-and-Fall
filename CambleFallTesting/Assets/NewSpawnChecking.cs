using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewSpawnChecking : MonoBehaviour
{
    [SerializeField] private GameObject cannonObj;
    [SerializeField] private ElevationCheck elevationCheck;
    private Cannon cannon;
    private void Start()
    {
        cannon = cannonObj.GetComponent<Cannon>();
    }
    void Update()
    {
        cannonObj.transform.position = elevationCheck.highestBlock.gameObject.transform.position + Vector3.up + (Vector3.up * cannon.extraYval());
    }
}
