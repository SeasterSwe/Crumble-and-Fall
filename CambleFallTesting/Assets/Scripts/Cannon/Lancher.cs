using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lancher : MonoBehaviour
{

    public GameMaster gm;
    public IndicatorBar firePowerUI;
    public float firePowerUIscale = 0.25f;
    public bool init = false;

    public int Player = 0;
    [Header("CanonSettings")]
    public int lancherHP = 5;
    public float maxAngle = 135;
    public float minAngle = -135;
    public float angularSpeed = 45;
    private bool ping;
    private Vector3 angle;

    public string fireButton = "FirePlayerRight";

    [Header("ProjectileSettings")]
    public GameObject projectile;
    public float fireSpeed = 10;
    public float firePower = 0;
    public float maxFirePower = 40;

    // Start is called before the first frame update
    void Start()
    {
        angle = Vector3.up;
    }

    // Update is called once per frame
    private void Update()
    {
        if (init)
        {
            RotateLauncher();
            FirePower();

            if (Input.GetButtonDown(fireButton))
            {
                FireProjectile();
            }
        }

        init = true;
    }

    void FirePower()
    {
        firePower += fireSpeed * Time.deltaTime;
        if (firePower > maxFirePower)
            firePower = maxFirePower;


        firePowerUI.UpdateValue(firePower, firePowerUIscale);
    }
    void RotateLauncher()
    {
        if (ping)
        {
            angle.z += angularSpeed * Time.deltaTime;
            if(angle.z > maxAngle)
                ping = !ping;
        }
        else
        {
            angle.z -= angularSpeed * Time.deltaTime;
            if (angle.z < minAngle)
                ping = !ping;
        }
        transform.rotation = Quaternion.Euler(angle);
    }

    void FireProjectile()
    {
        GameObject myProjectile = Instantiate(projectile, transform.position, transform.rotation);
        myProjectile.GetComponent<Projectile>().setCatagoryByNumber(Random.Range(0,3));
        myProjectile.GetComponent<Rigidbody2D>().velocity = transform.up * firePower;
        firePower = 0;
    }

    private void OnDestroy()
    {
        lancherHP--;
        
    }
}
