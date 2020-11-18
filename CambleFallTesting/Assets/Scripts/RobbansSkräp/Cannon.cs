using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public Transform mousePointer;
    public GameObject projectile;

void Update()
    {
        mousePointer.position = GetMousePos();

        Vector2 dir = mousePointer.position - transform.position;
        float str = dir.sqrMagnitude;
        transform.right = dir;

        if(Input.GetButtonDown("Fire1"))
        {
            GameObject canonBall = Instantiate(projectile, transform.position, Quaternion.LookRotation(Vector3.forward, transform.up));
            canonBall.GetComponent<Rigidbody2D>().velocity = dir.normalized * str;
        }
    }


    Vector2 GetMousePos()
    {
        Vector3 mousePos = Input.mousePosition;
        return (Camera.main.ScreenToWorldPoint(mousePos));
    }
}
