using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBuilding : MonoBehaviour
{
    public Transform leftSide;
    public Transform rightSide;
    public float moveSpeed;
    public float dropSpeed = -9;

    float debugTime;
    private GameObject nextBlock;
    private SpriteRenderer renderer;
    private void Start()
    {
        nextBlock = BlockList.GetARandomBlock();
        renderer = GetComponent<SpriteRenderer>();
        renderer.color = nextBlock.GetComponent<SpriteRenderer>().color;
    }
    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
        if (leftSide.position.x > transform.position.x && debugTime < Time.time)
        {
            debugTime = Time.time + 0.1f;
            moveSpeed *= -1;
        }
        if (rightSide.position.x < transform.position.x && debugTime < Time.time)
        {
            debugTime = Time.time + 0.1f;
            moveSpeed *= -1;
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            NormalDrop();
            //RayCastDrop();

            nextBlock = BlockList.GetARandomBlock();
            renderer.color = nextBlock.GetComponent<SpriteRenderer>().color;
            moveSpeed *= -1;
        }
    }
    void NormalDrop()
    {
        GameObject clone = Instantiate(nextBlock, transform.position, transform.rotation);
        clone.GetComponent<Rigidbody2D>().velocity = Vector2.up * dropSpeed;
    }
    void RayCastDrop()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up);
        if(hit.collider != null)
        {
            Instantiate(nextBlock, hit.point + (Vector2.up * 0.5f), Quaternion.identity);
            nextBlock = BlockList.GetARandomBlock();
        }
    }
}
