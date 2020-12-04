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

    //Transform target;
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
            //if (target == null)
            //    target = elevationCheck.highestBlock.gameObject.transform;

            if (elevationCheck.highestBlock.gameObject.transform.position.y > cannonObj.transform.position.y - 1)
            {
                Swap();
            }
            else
                cannonObj.transform.position = elevationCheck.highestBlock.gameObject.transform.position + Vector3.up + (Vector3.up * cannon.extraYval());

            //else if (Mathf.Abs(cannonObj.transform.position.y - elevationCheck.highestBlock.gameObject.transform.position.y) > 1.5f)
            //{
            //    Swap();
            //}

            //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 2f);
            //if (hit.collider == null)
            //    cannonObj.transform.position = elevationCheck.highestBlock.gameObject.transform.position + Vector3.up + (Vector3.up * cannon.extraYval());

        }
    }

    void Swap()
    {
        cannonObj.transform.position =
            elevationCheck.highestBlock.gameObject.transform.position + Vector3.up + (Vector3.up * cannon.extraYval());
        GameObject smokeClone = Instantiate(smoke, transform.position, smoke.transform.rotation);
    }
    IEnumerator MinHjärnaDog()
    {
        yield return new WaitForSeconds(0.08f);
        GameObject smokeClone = Instantiate(smoke, transform.position, smoke.transform.rotation);
    }
}
