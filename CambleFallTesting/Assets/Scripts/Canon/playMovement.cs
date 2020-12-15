using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playMovement : MonoBehaviour
{
    public Transform aim;
    public LayerMask mask;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    void CircelCasting()
    {
        RaycastHit2D hit = Physics2D.CircleCast(aim.position, 1.0f, Vector2.down, mask, 20);
        
        if(hit.collider != null)
        {
            Debug.DrawRay(hit.point, hit.normal, Color.cyan);
        }

    }
}
