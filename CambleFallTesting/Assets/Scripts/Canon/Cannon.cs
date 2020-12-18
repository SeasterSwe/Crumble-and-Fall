using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
[RequireComponent(typeof(CannonHealth))]
[RequireComponent(typeof(CannonHeightBonuses))]
[RequireComponent(typeof(CannonMovement))]
public class Cannon : MonoBehaviour
{

    //Test for aim //Robert
    public float projectileFinalCharge;
    public AimCannon aim;
    //End Test
    public float degToTheLeft;
    public float degToTheRight;
    public float fireRate;
    public float launchForce;
    public string shootButton;
    public float rotationSpeed;
    private float nextFire = 1;
    public float timeToFullCharge = 10;
    private float chargeSpeed;
    float chargePower = 1;
    float maxCharge = 20;
    public Transform shootPos;
    public GameObject shootEffekt;

    Vector3 point1;
    Vector3 point2;

    SpriteRenderer loadImage;
    public Inventory inventory;

    [HideInInspector] public bool chargeIsntStarted;
    [HideInInspector] public float bonunsRotationSpeed = 0;
    [HideInInspector] public float velBouns;

    public int numberOfPoints;
    private List<Vector3> points = new List<Vector3>();

    [HideInInspector] public BarBase loadBar;
    float time;
    private Vector3 normalScale;

    private Transform cannonPipe;

    float particleRotation = 90;
    private Animator animator;
    CannonHealth cannonHealth;

    void RobertsTestAim()
    {
        projectileFinalCharge = transform.localScale.x * (1 + 0.5f * (chargePower / maxCharge) + velBouns);
        aim.Aim();
    }


    void Start()
    {
        if (transform.position.x > 0)
            particleRotation = -90;

        cannonPipe = transform.Find("CannonPipe");
        normalScale = transform.localScale;

        loadImage = transform.Find("LoadImage").GetComponent<SpriteRenderer>();
        chargeSpeed = maxCharge / timeToFullCharge;

        SetAnglePoints();

        chargeIsntStarted = true;

        UpdateLoadImage(inventory.selectedBlock);

        animator = GetComponent<Animator>();

        cannonHealth = GetComponent<CannonHealth>();

        //Test : Roberts Test
        aim = GetComponent<AimCannon>();
    }
    void SetAnglePoints()
    {
        point1 = (degToTheLeft + cannonPipe.localEulerAngles.z) * cannonPipe.forward;
        point2 = (-degToTheRight + cannonPipe.localEulerAngles.z) * cannonPipe.forward;

        if (point1.magnitude > point2.magnitude) //så de åker åt samma håll
        {
            var tempPoint = point1;
            point1 = point2;
            point2 = tempPoint;
        }
    }

    float holdTimer = 0.2f;
    Vector3 startPos = new Vector3();
    void Update()
    {
        if (Input.GetButtonUp(shootButton))
        {
            aim.Disable();
        }


        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            UpdateLoadImage(inventory.selectedBlock);

        RotateCannon(rotationSpeed + bonunsRotationSpeed);

        //GameObject block = inventory.selectedBlock;// blockBuilder.blockPreFab.GetComponent<BlockType>().type;
        nextFire += Time.deltaTime;
        if (!inventory.SelectedBlockIsInInventory())
        {
            if (Input.GetButtonDown(shootButton))
                SoundManager.PlaySound(SoundManager.Sound.CannonOutOfAmmo);

            loadBar.UpdateFillAmount(0);
            OutOfBlocks();
            return;
        }

        loadBar.UpdateFillAmount(nextFire / time);
        //holdCharge
        if (Input.GetButton(shootButton) && nextFire > time)
        {
            //Test Robban
            aim.Enable();

            animator.SetBool("Shooting", true);
            holdTimer += Time.deltaTime;
            if (chargeIsntStarted)
                startPos = transform.position;


            if (holdTimer > 0.3f)
            {
                ChargeCannon();
                chargeIsntStarted = false;
            }


            //TEST : Roberts test aim
            RobertsTestAim();
        }

        //Shoot      
        if (Input.GetButtonUp(shootButton) && nextFire > time)
        {
            holdTimer = 0;
            time = fireRate;
            nextFire = 0;
            if (Time.timeScale != 0)
            {
                ShootBlock(chargePower);
                GameObject particleEffekt = Instantiate(shootEffekt, shootPos.position - (shootPos.right * 0.5f), shootPos.rotation * Quaternion.Euler(0, particleRotation, 0));
            }
            chargePower = 1;

            transform.localScale = normalScale;
            chargeIsntStarted = true;
            animator.SetBool("Shooting", false);
        }
    }
    private void ChargeCannon()
    {
        chargePower += Time.deltaTime * chargeSpeed;
        if (chargePower > maxCharge)
            chargePower = maxCharge;

        transform.localScale = normalScale + (normalScale * (chargePower / maxCharge) * 0.6f);
    }
    public float extraYval()
    {
        float val = (chargePower / maxCharge) * 0.6f;
        return val;
    }
    float lerpVal = 0;
    void RotateCannon(float rotationSpeed)
    {
        if (GameState.currentState != GameState.gameStates.Fight && GameState.currentState != GameState.gameStates.SuddenDeath)
            return;

        lerpVal += Time.deltaTime * rotationSpeed;
        cannonPipe.localEulerAngles = Vector3.Lerp(point1, point2, lerpVal);

        //lerp tar in värde mellan 0 - 1.
        if (lerpVal >= 1)
        {
            var tempPoint = point1;
            point1 = point2;
            point2 = tempPoint;
            lerpVal = 0;
        }
    }
    void ShootBlock(float extraForce = 0)
    {
        RobertsTestAim();
        GameObject clone = Instantiate(inventory.TakeActiveBlockFromInventory(), shootPos.position, shootPos.rotation);
        Rigidbody2D rb = clone.GetComponent<Rigidbody2D>();

        clone.GetComponent<BlockType>().SetProjectileSpeed(shootPos.right * transform.localScale.x * (1 + 0.5f * (chargePower / maxCharge) + velBouns));
        //float mass = rb.mass/2;
        //float totaltForce = (launchForce * mass) + extraForce + velBouns;
        //rb.AddForce(shootPos.right * totaltForce, ForceMode2D.Impulse);

        TransferBlockToProjectile(clone);


        //        if (totaltForce > 15)
        //          rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
    }
    private void TransferBlockToProjectile(GameObject obj)
    {
        UpdateLoadImage(obj);

        obj.GetComponent<BlockType>().SetState(BlockType.states.Projectile);

        /*
        if (obj.GetComponent<Projectile>() != null)
            obj.GetComponent<Projectile>().enabled = true;
        /*
        if(obj.GetComponent<VelocityTest>() != null)

            obj.GetComponent<VelocityTest>().enabled = false;
        */
        if (obj.GetComponent<TrailRenderer>() != null)
            obj.GetComponent<TrailRenderer>().enabled = true;

        // obj.layer = 2; //ignoreRayCast
        // obj.tag = "Untagged";
    }
    private void UpdateLoadImage(GameObject newBlock)
    {
        loadImage.sprite = inventory.GetIconFromSelection();//.GetComponent<SpriteRenderer>().sprite;
        loadImage.color = loadImage.GetComponent<SpriteRenderer>().color;
    }

    public void IncreasMaxCharge(float amount)
    {
        maxCharge += amount;
        chargeSpeed = maxCharge / timeToFullCharge;
    }

    //void DrawPoints(int amountOfPoints, float force, float mass)
    //{
    //    points.Clear();
    //    for (int i = 0; i < amountOfPoints; i++)
    //    {
    //        points.Add(PointPosition(i * 0.1f, force, mass));
    //        line.SetPosition(i, points[i]);
    //    }
    //}
    //Vector3 PointPosition(float t, float force, float mass)
    //{
    //    Vector3 position = shootPos.position + (shootPos.right * force * t) + 0.5f * ((Vector3)Physics2D.gravity * (t * t) * mass); //formelSak
    //    return position;
    //}

    private void OutOfBlocks()
    {

    }
}
