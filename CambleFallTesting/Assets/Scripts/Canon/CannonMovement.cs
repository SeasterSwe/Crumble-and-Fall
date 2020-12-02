using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonMovement : MonoBehaviour
{
    private GameObject cannonObj;
    [SerializeField] private ElevationCheck elevationCheck;
    private Cannon cannon;
    private void Start()
    {
        cannonObj = this.gameObject;
        cannon = cannonObj.GetComponent<Cannon>();
    }
    void Update()
    {
        cannonObj.transform.position = elevationCheck.highestBlock.gameObject.transform.position + Vector3.up + (Vector3.up * cannon.extraYval());
    }
}
