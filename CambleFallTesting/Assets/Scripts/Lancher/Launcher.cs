using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Launcher : MonoBehaviour
{
    public string fireInput;
    public Transform aim;

    public float baseCharge = 5;
    public float chargePerSec = 5;
    public float maxCharge = 20;
    public float charge = 0;


    public GameObject currentProjectile;

    public GameObject fluffy;
    public GameObject speedy;
    public GameObject heavy;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void Update()
    {
        CheckInput();
        Scale();
    }
    void CheckInput()
    {
        if (Input.GetButtonDown(fireInput))
        {
            charge = baseCharge;
        }
        if (Input.GetButton(fireInput))
        {
            charge += chargePerSec * Time.deltaTime;

            if (charge > maxCharge)
                charge = maxCharge;
        }
        if (Input.GetButtonUp(fireInput))
        {
            GameObject projectile = Instantiate(currentProjectile, aim.position - aim.right * 2, aim.rotation);
            projectile.GetComponent<Rigidbody2D>().velocity = -aim.right * charge;

        }
    }

    void Scale()
    {
        transform.localScale = Vector3.one * (1 + (charge * 0.01f)); 
    }
}
