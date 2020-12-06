using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LancherTest : MonoBehaviour
{
    [Header("Settings")]
    public string FireButton = "FireButton";

    public float rotationSpeeed  = 30;
    public GameObject angleMarkerOne;
    public GameObject angleMarkerTwo;

    private Quaternion angleOne;
    private Quaternion angleTwo;
    private Quaternion targetAngle;

    public float firerate = 1;
    private float coolDown;

    public float maxCharge = 20;
    public float minCharge = 10;
    public float chargePerSec = 2;
    private float charge;


    [Header("OtherScripts")]
    public Inventory inventory;
    public ElevationCheck evCheck;

    // Start is called before the first frame update
    void Start()
    {
        coolDown = firerate;
        charge = minCharge;

        angleOne = angleMarkerOne.transform.rotation;
        angleTwo = angleMarkerTwo.transform.rotation;
        targetAngle = angleOne;
        transform.rotation = angleOne;
        Destroy(angleMarkerOne);
        Destroy(angleMarkerTwo);
    }

    // Update is called once per frame
    void Update()
    {
        PingPongRotation();
        FireOperation();
    }

    void PingPongRotation()
    {
        if (transform.rotation == angleOne)
            targetAngle = angleTwo;

        if (transform.rotation == angleTwo)
            targetAngle = angleOne;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, targetAngle, rotationSpeeed * Time.deltaTime);
    }


    void FireOperation()
    {
        coolDown -= Time.deltaTime;
        if (Input.GetButton(FireButton) && coolDown < 0)
        {
            charge += chargePerSec * Time.deltaTime;
            if (charge > maxCharge)
                charge = maxCharge;

            FXScaleCannon( (charge - minCharge) / maxCharge);

        }
        if (Input.GetButtonUp(FireButton) && coolDown < 0)
        {
            Fire();
            
            //Reset
            charge = minCharge;
            coolDown = firerate;
        }
    }

    void FXScaleCannon(float lerpScale)
    {
        transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 2, lerpScale);
    }

    void Fire()
    {
        GameObject projectile = Instantiate(inventory.TakeActiveBlockFromInventory(), transform.position + transform.up * 2, transform.rotation);
        projectile.GetComponent<Rigidbody2D>().velocity = transform.up * charge;
    }

    void MoveCannon()
    {
        transform.position = evCheck.highestBlock.ClosestPoint(evCheck.highestBlock.transform.position + Vector3.up) + Vector2.up * 0.5f;
        transform.parent = evCheck.transform;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        MoveCannon();
    }
}
