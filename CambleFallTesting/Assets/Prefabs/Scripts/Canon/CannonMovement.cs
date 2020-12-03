using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class CannonMovement : MonoBehaviour
{
    private GameObject cannonObj;
    [SerializeField] private ElevationCheck elevationCheck;
    private Cannon cannon;
    public GameObject smoke;
    private void Start()
    {
        cannonObj = this.gameObject;
        cannon = cannonObj.GetComponent<Cannon>();
        StartCoroutine(MinHjärnaDog());
    }
    void Update()
    {
        if (elevationCheck.highestBlock.gameObject != null)
        {
            //if (Mathf.Abs(cannonObj.transform.position.y - elevationCheck.highestBlock.gameObject.transform.position.y) > 0.2f)
            //{
                cannonObj.transform.position =
                    elevationCheck.highestBlock.gameObject.transform.position + Vector3.up + (Vector3.up * cannon.extraYval());
            //}
            //else
            //    print("Bruh");
        }
    }

    IEnumerator MinHjärnaDog()
    {
        yield return new WaitForSeconds(0.08f);
        GameObject smokeClone = Instantiate(smoke, transform.position, smoke.transform.rotation);
    }
}
