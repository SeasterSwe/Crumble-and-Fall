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
    private float nextFire = 1;
    public float chargeSpeed = 10;
    float chargePower = 1;
    float maxCharge = 20;
    //public GameObject block;
    public Transform shootPos;
    public GameObject shootEffekt;
    Vector3 point1;
    Vector3 point2;
    void Start()
    {
        SetAnglePoints();
    }
    void SetAnglePoints()
    {
        point1 = (angle1 + transform.localEulerAngles.z) * transform.forward;
        point2 = (-angle2 + transform.localEulerAngles.z) * transform.forward;

        if (point1.magnitude > point2.magnitude) //så de åker åt samma håll
        {
            var tempPoint = point1;
            point1 = point2;
            point2 = tempPoint;
        }
    }
    float holdTimer = 0.2f;
    void Update()
    {
        if (Input.GetButton(shootButton) && nextFire < Time.time)
        {
            holdTimer += Time.deltaTime;
            if (holdTimer > 0.3f)
            {
                Charge();
            }
        }
        if (Input.GetButtonUp(shootButton) && nextFire < Time.time)
        {
            holdTimer = 0;
            nextFire = fireRate + Time.time;
            Shoot(BlockList.GetARandomPlayerShoot(), chargePower);
            GameObject particleEffekt = Instantiate(shootEffekt, shootPos.position - (shootPos.right * 0.5f), shootPos.rotation * Quaternion.Euler(0, 90, 0));
            chargePower = 1;
            transform.localScale = Vector3.one;
        }
        Rotatation(rotationSpeed);
    }
    private void Charge()
    {
        chargePower += Time.deltaTime * chargeSpeed;
                if (chargePower > maxCharge)
                    chargePower = maxCharge;

                transform.localScale = Vector3.one + (Vector3.one * (chargePower / maxCharge) * 0.6f);
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
    void Shoot(GameObject block, float extraForce = 0)
    {
        GameObject clone = Instantiate(block, shootPos.position, shootPos.rotation);
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        float mass = rb.mass;
        rb.AddForce(shootPos.right * ((launchForce * mass) + extraForce), ForceMode2D.Impulse);
    }
}
