using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Cannon))]
[RequireComponent(typeof(LineRenderer))]

public class AImCannon : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Cannon cannon;
    public float segmentStep = 0.1f;



    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        cannon = GetComponent<Cannon>();
    }


    public void Aim()
    {
        Rigidbody2D pRB = cannon.inventory.selectedBlock.GetComponent<Rigidbody2D>();
        Vector2 gravity = (Physics2D.gravity) * pRB.gravityScale;
        if (cannon.inventory.selectedBlock.GetComponent<BlockType>().type == BlockType.types.Fluffy)
        {
            gravity = Vector2.zero;
        }

        float speedMul = cannon.inventory.selectedBlock.GetComponent<BlockType>().velocityMultiplier;
        float velocity = cannon.projectileFinalCharge * speedMul;

        Vector2 fakeDir = cannon.shootPos.right * velocity;
        Vector2 fakePos = cannon.shootPos.position;// + cannon.shootPos.right * cannon.transform.localScale.x;



        lineRenderer.SetPosition(0, fakePos);
        for (int i = 1; i < lineRenderer.positionCount; i++)
        {
            float stepLength = i * segmentStep;
            lineRenderer.SetPosition(i, fakePos + fakeDir * stepLength + 0.5f * gravity * stepLength * stepLength);
        }
    }
}
