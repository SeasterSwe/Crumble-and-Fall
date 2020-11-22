using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lancher : MonoBehaviour
{
    [Header("Coms")]
    public GameMaster gm;
    public IndicatorBar firePowerUI;
    public float firePowerUIscale = 0.25f;
    public int Player = 0;

    [Header("CanonSettings")]
    public int hp;
    public float maxAngle = 135;
    public float minAngle = -135;
    public float angularSpeed = 45;
    private bool ping;
    private Vector3 angle;

    public string fireButton = "FirePlayerRight";

    public BlockBuilder blockBuilder;

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
    private void OnCollisionEnter2D(Collision2D collision)
    {
        transform.parent = null;
        transform.position = blockBuilder.highestBlock.transform.position + Vector3.up;
        transform.parent = blockBuilder.highestBlock.transform;
    }
    // Update is called once per frame
    private void Update()
    {
        RotateLauncher();
        FirePower();

        if (Input.GetButtonDown(fireButton))
        {
            FireProjectile();
        }
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
        GameObject myProjectile = Instantiate(projectile, transform.position + transform.up, transform.rotation);
        myProjectile.GetComponent<Projectile>().setCatagoryByNumber(Random.Range(0,3));
        myProjectile.GetComponent<Rigidbody2D>().velocity = transform.up * firePower;
        firePower = 0;
    }

    public void TakeDamage()
    {
        hp--;
    }
    /*
    private void OnDestroy()
    {
    }
    */
}
