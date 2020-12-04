//Robban
using UnityEngine;

public class BlockType : MonoBehaviour
{
    public int playerteam = 1;
    //public string category = "Red";
    public enum types {Fluffy, Speedy, Heavy}
    public types type;

    public enum states { Idle, Flying}
    public states state = states.Idle;

    private SpriteRenderer spRenderer;
    private Vector2 lowerLeftCorner;



    // Start is called before the first frame update
    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        GetLowerLeftCorner();
        //BlockManager.AddBlockToList(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnHitEnter(collision);
    }

    void GetLowerLeftCorner()
    {
        float yHight = Camera.main.orthographicSize;
        float aspect = Camera.main.aspect;
        Vector2 pos = Camera.main.transform.position;

        lowerLeftCorner = pos;
        lowerLeftCorner.y -= yHight;
        lowerLeftCorner.x -= yHight * aspect;
    }

    private void Update()
    {
        spRenderer.sortingOrder = (int)(transform.position.x - lowerLeftCorner.x + transform.position.y - lowerLeftCorner.y);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnInsideTrigger(collision);
    }

    protected virtual void OnInsideTrigger(Collider2D collider)
    {
        //Is insideTrigger
    }

    protected virtual void OnHitEnter(Collision2D collision)
    {

    }


    // Start is called before the first frame update
    void Start()
    {
        //BlockManager.AddBlockToList(gameObject);
    }

    public static bool IsFluffy(GameObject checkObject)
    {
        if (checkObject.GetComponent<BlockType>())
        {
            if (checkObject.GetComponent<BlockType>().type == types.Fluffy)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDestroy()
    {
        //BlockManager.RemoveBlockFromList(gameObject);
    }
}
