//Robban
using UnityEngine;
using UnityEngine.UI;
public class BlockType : MonoBehaviour
{
    public int playerteam = 1;
    public Inventory inventory;

    public LayerMask projectileLayer;
    public LayerMask blockLayer;
    //public string category = "Red";
    public enum types { Fluffy, Speedy, Heavy }
    public types type;

    public enum states { Idle, Flying }
    public states state = states.Idle;

    private SpriteRenderer spRenderer;
    private Vector2 lowerLeftCorner;



    // Start is called before the first frame update
    void Start()
    {
        spRenderer = GetComponent<SpriteRenderer>();
        GetLowerLeftCorner();
        //BlockManager.AddBlockToList(gameObject);
        if (transform.position.x < 0)
        {
            playerteam = 1;
        }
        else
        {
            playerteam = 2;
        }

        Inventory[] inventorys = FindObjectsOfType<Inventory>();

        foreach (Inventory inv in inventorys)
        {
            if (inv.gameObject.GetComponent<RectTransform>().anchoredPosition.x < 0 && transform.position.x < 0)
            {
                inventory = inv;
            }
            else if (inv.gameObject.GetComponent<RectTransform>().anchoredPosition.x > 0 && transform.position.x > 0)
            {
                inventory = inv;
            }
        }

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
        if (GameState.currentState == GameState.gameStates.Build)
        {
            if (inventory != null)
                inventory.AddToInventory(type, 1);
        }
    }

    public void SetState(states toState)
    {
        state = toState;

        switch (state)
        {
            case states.Idle:
                {
                    gameObject.layer = layermaskToLayer(blockLayer);
                }
                break;

            case states.Flying:
                {
                    gameObject.layer = layermaskToLayer(projectileLayer);
                }
                break;

            default:
                {
                    gameObject.layer = layermaskToLayer(blockLayer);
                }
                break;
        }
    }

    //Thx. Reconnoiter - Unity forum
    public static int layermaskToLayer(LayerMask layerMask)
    {
        int layerNumber = 0;
        int layer = layerMask.value;
        while (layer > 0)
        {
            layer = layer >> 1;
            layerNumber++;
        }
        return layerNumber - 1;
    }
}
