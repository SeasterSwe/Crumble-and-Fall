using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootCube : MonoBehaviour
{
    public float moveSpeed = 4;
    public float launchForce;
    public GameObject cube;
    void Update()
    {

        Vector2 movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        transform.Translate(movement.normalized * Time.deltaTime * moveSpeed);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject clone = Instantiate(cube, transform.position, transform.rotation);
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();
        rb.AddForce(new Vector2(-1,0.2f) * launchForce, ForceMode2D.Impulse);
    }
}
