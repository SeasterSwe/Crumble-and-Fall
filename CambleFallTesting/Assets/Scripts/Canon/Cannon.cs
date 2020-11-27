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
    public float timeToFullCharge = 10;
    private float chargeSpeed;
    float chargePower = 1;
    float maxCharge = 20;
    public Transform shootPos;
    public GameObject shootEffekt;
    Vector3 point1;
    Vector3 point2;
    SpriteRenderer loadImage;
    GameObject nextBlock;

    [HideInInspector] public bool chargeIsntStarted;
    [HideInInspector] public float bonunsRotationSpeed = 0;
    [HideInInspector] public float velBouns;
    void Start()
    {
        loadImage = transform.Find("LoadImage").GetComponent<SpriteRenderer>();
        chargeSpeed = maxCharge / timeToFullCharge;
        SetAnglePoints();

        nextBlock = BlockList.GetARandomPlayerShoot();
        loadImage.sprite = nextBlock.GetComponent<SpriteRenderer>().sprite;
        loadImage.color = nextBlock.GetComponent<SpriteRenderer>().color;
        chargeIsntStarted = true;
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
    Vector3 startPos = new Vector3();
    void Update()
    {
        if (Input.GetButton(shootButton) && nextFire < Time.time)
        {
            holdTimer += Time.deltaTime;
            if (chargeIsntStarted)
                startPos = transform.position;

            if (holdTimer > 0.3f)
            {
                Charge();
                chargeIsntStarted = false;
            }
        }

        if (Input.GetButtonUp(shootButton) && nextFire < Time.time)
        {
            holdTimer = 0;
            nextFire = fireRate + Time.time;
            Shoot(nextBlock, chargePower);
            GameObject particleEffekt = Instantiate(shootEffekt, shootPos.position - (shootPos.right * 0.5f), shootPos.rotation * Quaternion.Euler(0, 90, 0));
            chargePower = 1;
            
            nextBlock = BlockList.GetARandomPlayerShoot();
            loadImage.sprite = nextBlock.GetComponent<SpriteRenderer>().sprite;
            loadImage.color = nextBlock.GetComponent<SpriteRenderer>().color;
            transform.localScale = Vector3.one;
            chargeIsntStarted = true;
        }

        Rotatation(rotationSpeed + bonunsRotationSpeed);
    }
    private void Charge()
    {
        chargePower += Time.deltaTime * chargeSpeed;
                if (chargePower > maxCharge)
                    chargePower = maxCharge;

                transform.localScale = Vector3.one + (Vector3.one * (chargePower / maxCharge) * 0.6f);
    }
    public float extraYval()
    {
        float val = (chargePower / maxCharge) * 0.6f;
        return val;
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
        float mass = rb.mass/2;
        float totaltForce = (launchForce * mass) + extraForce + velBouns;
        rb.AddForce(shootPos.right * totaltForce, ForceMode2D.Impulse);
        if (totaltForce > 150)
            rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }
    public void IncreasMaxCharge(float amount)
    {
        maxCharge += amount;
        chargeSpeed = maxCharge / timeToFullCharge;
    }
}
