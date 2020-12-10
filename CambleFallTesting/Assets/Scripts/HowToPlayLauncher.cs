using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlayLauncher : MonoBehaviour
{
    public float restartTime;
    public float startVelocity;

    public GameObject wall;
    public GameObject fireBlock;

    public Transform wallSpawn;
    void Start()
    {
        StartCoroutine(Bruh());
    }

    IEnumerator Bruh()
    {
        GameObject wallClone = Instantiate(wall, wallSpawn.position, wall.transform.rotation);
        GameObject fireClone = Instantiate(fireBlock, transform.position, transform.rotation);
        Rigidbody2D rb = fireClone.GetComponent<Rigidbody2D>();

        fireClone.GetComponent<BlockType>().SetState(BlockType.states.Projectile);
        fireClone.GetComponent<BlockType>().SetProjectileSpeed(transform.right * -startVelocity);

        TransferBlockToProjectile(fireBlock);

        yield return new WaitForSeconds(restartTime);
        BruhAatweatesdasd();
        Destroy(wallClone);
        Destroy(fireClone);
        StartCoroutine(Bruh());
    }

    private void TransferBlockToProjectile(GameObject obj)
    {
        if (obj.GetComponent<Projectile>() != null)
            obj.GetComponent<Projectile>().enabled = true;

        if (obj.GetComponent<VelocityTest>() != null)
            obj.GetComponent<VelocityTest>().enabled = false;

        if (obj.GetComponent<TrailRenderer>() != null)
            obj.GetComponent<TrailRenderer>().enabled = true;

        obj.layer = 2; //ignoreRayCast
        obj.tag = "Untagged";
    }

    void BruhAatweatesdasd()
    {
        //byt till RB eller ngt 
        Collider2D[] vetEj = GameObject.FindObjectsOfType<Collider2D>();

        foreach(Collider2D collider2D in vetEj)
        {
            string name = collider2D.gameObject.name;
            if (name == "Fragment(Clone)")
                Destroy(collider2D.gameObject);
        }
    }
}
