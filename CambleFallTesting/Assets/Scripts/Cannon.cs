using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public float angle1;
    public float angle2;
    public float fireRate;
    public float launchForce;
    public string shootButton;
    public float rotationSpeed;

    public GameObject block;
    public Transform shootPos;

    Vector3 point1;
    Vector3 point2;
    void Start()
    {
        SetAnglePoints();
    }

    void Update()
    {
        if (Input.GetButtonDown(shootButton))
        {
            Shoot();
        }

        Rotatation(rotationSpeed);
    }

    void SetAnglePoints()
    {
        var localDirection = transform.rotation * transform.forward;
        point1 = angle1 * localDirection;
        point2 = -angle2 * localDirection;
    }
    float lerpVal = 0;
    void Rotatation(float rotationSpeed)
    {
        lerpVal += Time.deltaTime * rotationSpeed;
        transform.localEulerAngles = Vector3.Lerp(point1, point2, lerpVal);

        //lerp tar in värde mellan 0 - 1.
        if (lerpVal >= 1) 
        {
            var tempPoint = point1;
            point1 = point2;
            point2 = tempPoint;
            lerpVal = 0;
        }
    }
    void Shoot()
    {
        GameObject clone = Instantiate(block, shootPos.position, shootPos.rotation);
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        float mass = rb.mass;
        rb.AddForce(shootPos.right * launchForce * mass, ForceMode2D.Impulse);
    }
}
