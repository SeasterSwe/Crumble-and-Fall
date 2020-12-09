using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//TEST 
public class VelocityTest : MonoBehaviour
{
    public int collAmount = 0;
    public bool freeze = true;
    public bool isRotated = false;
    private void Update()
    {
        //RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 1f);
       // if (hit.collider != null)
        //{
            if (gameObject.transform.rotation.eulerAngles.z > 25 || gameObject.transform.rotation.eulerAngles.z < -25)
                isRotated = true;
            else
                isRotated = false;

            if (isRotated)
                GetComponent<Rigidbody2D>().freezeRotation = false;
            else
            {
                if (freeze)
                {
                    if (collAmount > 2)
                        GetComponent<Rigidbody2D>().freezeRotation = true;
                    else
                        GetComponent<Rigidbody2D>().freezeRotation = false;
                }
                else
                    GetComponent<Rigidbody2D>().freezeRotation = false;
            }
        //}
        //else
        //    GetComponent<Rigidbody2D>().freezeRotation = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
            collAmount += 1;
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Block"))
            collAmount -= 1;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (GetImpactForce(collision) > 350)
        {
            if (!coroutineIsActive)
                coroutine = StartCoroutine(StopFreeze());
            else
            {
                StopCoroutine(coroutine);
                coroutine = StartCoroutine(StopFreeze());
            }
        }
    }
    Coroutine coroutine;
    bool coroutineIsActive = false;
    IEnumerator StopFreeze()
    {
        coroutineIsActive = true;
        freeze = false;
        yield return new WaitForSeconds(5f);
        freeze = true;
        coroutineIsActive = false;
    }
    public static float GetImpactForce(Collision2D collision)
    {
        float impulse = 0F;

        foreach (ContactPoint2D point in collision.contacts)
        {
            impulse += point.normalImpulse;
        }

        return impulse / Time.fixedDeltaTime;
    }
}
