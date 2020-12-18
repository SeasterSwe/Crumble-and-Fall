//Robban
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//using UnityEngine.UI;
public class BlockType : MonoBehaviour
{
    [Header("BlockType Settings")]

    public float velocityMultiplier = 1.4f;
    public float linearDrag = 1;

    public float BlockMass = 5;
    public float projectileMass = 1;

    public LayerMask projectileLayer;
    public LayerMask blockLayer;

    public enum types { Fluffy, Speedy, Heavy }
    public types type;

    public enum states { Idle, Projectile, Worried, Brace }
    public states state = states.Idle;

    public Inventory inventory;

    private SpriteRenderer spRenderer;
    private Vector2 lowerLeftCorner;

    public int playerteam = 1;

    Rigidbody2D rb;
    public GameObject particle;
    public bool hitThisFrame;

    private CircleCollider2D circleCol;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(state == states.Idle)
        {
            SetState(states.Brace);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(state == states.Brace)
        {
            SetState(states.Idle);
        }
    }

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
        //print(collision.relativeVelocity.magnitude);
    }
    protected virtual void OnHitEnter(Collision2D collision)
    {
         //fx
        if (state == states.Projectile || state == states.Worried)
        {
            var contactPoint = collision.GetContact(0).point;
            GameObject particleClone = Instantiate(particle, contactPoint, particle.transform.rotation);
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<CannonHealth>().TakeDmg();
        }
        // SetState(states.Idle);
        StartCoroutine(ChangeWhenVelIs(1));
    }

    private void Update()
    {
        spRenderer.sortingOrder = (int)(transform.position.x - lowerLeftCorner.x + transform.position.y - lowerLeftCorner.y);
        UpdateEachFrame();

        
        if (state == states.Projectile)
        {
            if (rb.velocity.x > 0)
            {
                transform.right = rb.velocity;
            }
            else
            {
                transform.right = rb.velocity * -1;
                gameObject.GetComponent<SpriteRenderer>().flipX = true;
            }
        }

    }

    private void FixedUpdate()
    {
        if (state == states.Idle || state == states.Worried)
        {
            if (rb.velocity.sqrMagnitude > 0.5f)
            {
                SetState(states.Worried);
            }
            else
            {
                SetState(states.Idle);
            }
        }

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
        if(circleCol != null)
        {
            Destroy(circleCol);
        }

        state = toState;

        switch (state)
        {
            case states.Idle:
                {
                    //print(transform.name + " State set to Idle");
                    StateChangedToIdle();
                }
                break;

            case states.Projectile:
                {
                    //print(transform.name + " State set to Projectile");
                    StateChagedToProjectile();
                }
                break;

            case states.Worried:
                {
                    StateChagedToWorried();
                }
                break;

            case states.Brace:
                {
                    StateChagedToBrace();
                }
                break;

            default:
                {
                    gameObject.layer = layermaskToLayer(blockLayer);
                    GetComponent<Animator>().SetInteger("State", 2);
                }
                break;
        }
    }

    protected virtual void StateChangedToIdle()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.drag = linearDrag;
        rb.mass = BlockMass;
        gameObject.layer = layermaskToLayer(blockLayer);
        //StartCoroutine(ChangeWhenVelIs(0.7f));
        StartCoroutine(Anim(0));
    }

    IEnumerator ChangeWhenVelIs(float mag)
    {
        rb = GetComponent<Rigidbody2D>();
        bool exitToIdle = false;
        while (!exitToIdle)
        {
            if (rb.velocity.sqrMagnitude < mag * mag)
            {
                SetState(states.Idle);
                exitToIdle = true;
            }
            yield return null;
        }
        yield return new WaitForEndOfFrame();
    }

    protected virtual void StateChagedToProjectile()
    {
        //circleCol = gameObject.AddComponent<CircleCollider2D>();
        //circleCol.isTrigger = true;
        //circleCol.radius = 10;

        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.drag = 0;
        rb.mass = projectileMass;

        gameObject.layer = layermaskToLayer(projectileLayer);
        StartCoroutine(Anim(3));
    }

    protected virtual void StateChagedToWorried()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.drag = linearDrag;
        rb.mass = BlockMass;

        gameObject.layer = layermaskToLayer(blockLayer);
        StartCoroutine(Anim(2));
    }

    protected virtual void StateChagedToBrace()
    {
        Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
        rb.drag = linearDrag;
        rb.mass = BlockMass;
        gameObject.layer = layermaskToLayer(blockLayer);
        //StartCoroutine(ChangeWhenVelIs(0.7f));
        StartCoroutine(Anim(0));
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

    private IEnumerator Anim(int i)
    {
        yield return null;
        gameObject.GetComponent<Animator>().SetInteger("State", i);
    }
}
