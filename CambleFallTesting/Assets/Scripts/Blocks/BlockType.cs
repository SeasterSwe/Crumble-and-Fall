//Robban
using UnityEngine;
//using UnityEngine.UI;
public class BlockType : MonoBehaviour
{
    [Header("BlockType Settings")]

    public float velocityMultiplier = 1.4f;
    public float linearDrag = 1;

    public LayerMask projectileLayer;
    public LayerMask blockLayer;
    //public string category = "Red";
    public enum types { Fluffy, Speedy, Heavy }
    public types type;

    public enum states { Idle, Projectile }
    public states state = states.Idle;

    public Inventory inventory;
    
    private SpriteRenderer spRenderer;
    private Vector2 lowerLeftCorner;

    public int playerteam = 1;
    public bool hitThisFrame;

    Rigidbody2D rb;

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spRenderer = GetComponent<SpriteRenderer>();
        GetLowerLeftCorner();
        GetPlayerTeam();
        GetInvetory();

        //BlockManager.AddBlockToList(gameObject);
    }

    public void SetProjectileSpeed(Vector3 dir)
    {
        gameObject.GetComponent<Rigidbody2D>().velocity = dir * velocityMultiplier;
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
    void GetPlayerTeam()
    {
        if (transform.position.x < 0)
        {
            playerteam = 1;
        }
        else
        {
            playerteam = 2;
        }
    }

    void GetInvetory()
    {
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
    protected virtual void OnHitEnter(Collision2D collision)
    {
        SetState(states.Idle);
    }

    private void Update()
    {
        spRenderer.sortingOrder = (int)(transform.position.x - lowerLeftCorner.x + transform.position.y - lowerLeftCorner.y);
        UpdateEachFrame();
        
        if(state == states.Projectile)
        {
            if (rb.velocity.x > 0)
                transform.right = rb.velocity;
            else
                transform.right = rb.velocity *-1;

            //float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg; //quickmath
            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void FixedUpdate()
    {
        hitThisFrame = false;
    }

    protected virtual void UpdateEachFrame()
    {

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        OnInsideTrigger(collision);
    }

    protected virtual void OnInsideTrigger(Collider2D collider)
    {
        //Is insideTrigger
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
                    StateChangedToIdle();
                }
                break;

            case states.Projectile:
                {
                    StateChagedToProjectile();
                    
                }
                break;

            default:
                {
                    gameObject.layer = layermaskToLayer(blockLayer);
                }
                break;
        }
    }

    protected virtual void StateChangedToIdle()
    {
        gameObject.GetComponent<Rigidbody2D>().drag = linearDrag;
        gameObject.layer = layermaskToLayer(blockLayer);
    }

    protected virtual void StateChagedToProjectile()
    {
        gameObject.GetComponent<Rigidbody2D>().drag = 0;
        gameObject.layer = layermaskToLayer(projectileLayer);
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

    public static bool IsThisAFluffy(GameObject checkObject)
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
}
