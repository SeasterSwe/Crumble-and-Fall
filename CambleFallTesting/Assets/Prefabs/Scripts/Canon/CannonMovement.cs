using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
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
        if (elevationCheck.highestBlock.gameObject != null)
            cannonObj.transform.position = elevationCheck.highestBlock.gameObject.transform.position + Vector3.up + (Vector3.up * cannon.extraYval());
    }
}
