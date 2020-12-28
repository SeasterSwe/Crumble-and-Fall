using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trailer_AnimFluffy : MonoBehaviour
{
    public Rigidbody2D rb;
    public Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WaitForStart());
        
        rb.gravityScale = 0;
        cam.transform.position = new Vector3(0, -1, -20);
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(4);
        rb.GetComponent<BlockTypeFlyffy>().SetState(BlockType.states.Projectile);
        rb.velocity = Vector2.right * 5 + Vector2.up * 1;
        rb.gravityScale = 0.0125f;
    }

    // Update is called once per frame
    void Update()
    {
        if(cam.transform.position.x < 13.25f)
        cam.transform.position = new Vector3((rb.transform.position.x + 2) * 0.75f,-1, -20);
    }
}
