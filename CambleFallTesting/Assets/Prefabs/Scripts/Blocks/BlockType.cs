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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        OnHitEnter(collision);
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
