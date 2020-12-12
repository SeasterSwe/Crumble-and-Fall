using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class CannonMovement : MonoBehaviour
{
    private GameObject cannonObj;
    private ElevationCheck elevationCheck;
    private Cannon cannon;
    public GameObject smoke;

    Transform target;
    //public bool targetYeeted = false;

    private void Start()
    {
        cannonObj = this.gameObject;
        cannon = cannonObj.GetComponent<Cannon>();
        StartCoroutine(MinHjärnaDog());
        elevationCheck = FindClosetElevationCheck.GetClosets(gameObject);
    }
    void Update()
    {
        if (elevationCheck.highestBlock.gameObject != null)
        {
            if (target == null)
                target = elevationCheck.highestBlock.gameObject.transform;


            if (target.gameObject.GetComponent<Rigidbody2D>() != null)
            {
                if (!(target.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude > 1f))
                {
                    float totalDist = Vector2.Distance(target.position, elevationCheck.highestBlock.gameObject.transform.position);
                    float distX = target.position.x - elevationCheck.highestBlock.gameObject.transform.position.x;
                    if (totalDist > 1.8 || Mathf.Abs(distX) < 0.6f)
                        target = elevationCheck.highestBlock.gameObject.transform;
                }

                //kan bli buggad med fluffy, fix later
                else if (target.gameObject.GetComponent<Renderer>().isVisible == false)
                {
                    //om canonen flyger men landar ej i vatten
                    if (cannonObj.transform.position.y > -8f)
                        cannonObj.GetComponent<CannonHealth>().TakeDmg();

                    Swap();
                }

            }
            else
                target = elevationCheck.highestBlock.gameObject.transform;

            cannonObj.transform.position = target.position + Vector3.up + (Vector3.up * cannon.extraYval());
        }
    }

    void Swap()
    {
        target = elevationCheck.highestBlock.gameObject.transform;
        GameObject smokeClone = Instantiate(smoke, target.position + Vector3.up, smoke.transform.rotation);
    }
    IEnumerator MinHjärnaDog()
    {
        yield return new WaitForSeconds(0.08f);
        GameObject smokeClone = Instantiate(smoke, transform.position, smoke.transform.rotation);
    }
}
