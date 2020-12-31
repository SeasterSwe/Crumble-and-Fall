using UnityEngine;

public class PanPosition : MonoBehaviour
{
    public Vector3 bounds = new Vector3(25, 25, 25);
    public float speed = 2;
    public Vector3 motionDirection = Vector3.right;

    // Update is called once per frame
    void Update()
    {
        transform.position += motionDirection * speed * Time.deltaTime;
        LoopPos();    
    }

    void LoopPos()
    {
        if (transform.position.x > bounds.x)
        {
            transform.position -= new Vector3(bounds.x * 2, 0, 0);
        }
        else if (transform.position.x < -bounds.x)
        {
            transform.position += new Vector3(bounds.x * 2, 0, 0);
        }

        if (transform.position.y > bounds.y)
        {
            transform.position -= new Vector3(0, bounds.y * 2, 0);
        }
        else if (transform.position.y < -bounds.y)
        {
            transform.position += new Vector3(0, bounds.x * 2, 0);
        }
    }
}
