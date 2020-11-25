using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VelocityTest : MonoBehaviour
{
    public int collAmount = 0;
    public bool freeze = true;
    public bool isRotated = false;
    private void Update()
    {
        if (gameObject.transform.rotation.eulerAngles.z > 25 || gameObject.transform.rotation.eulerAngles.z < -25)
            isRotated = true;
        else
            isRotated = false;

        if (!isRotated)
        {
            if (freeze)
            {
                GetComponent<Rigidbody2D>().freezeRotation = true;
            }
            else
                GetComponent<Rigidbody2D>().freezeRotation = false;
        }
        else
        {
            GetComponent<Rigidbody2D>().freezeRotation = false;
        }

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
            if (!isRunning)
                coroutine = StartCoroutine(UnFreezeAndReFreeze());
            else
            {
                StopCoroutine(coroutine);
                coroutine = StartCoroutine(UnFreezeAndReFreeze());
            }
        }
    }
    Coroutine coroutine;
    bool isRunning = false;
    IEnumerator UnFreezeAndReFreeze()
    {
        isRunning = true;
        freeze = false;
        yield return new WaitForSeconds(5f);
        freeze = true;
        isRunning = false;
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
