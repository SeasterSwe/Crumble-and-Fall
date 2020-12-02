using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerBlockBuilder : MonoBehaviour
{
    [Header("Spawners")] // Rubik för publik variablar.


    [Header("Controls")]
    public string horizontalControl = "HorizontalPlayer1";
    public string fire = "FirePlayerOne";
    public float speed = 5;

    [Header("SpawnType")]
    public GameObject blockPreFab;
    public float startDownForce = 2;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 axisPlayer = Vector3.right * Input.GetAxis(horizontalControl) * speed; // Stringen kallar på input manager och behöver Axis, använder gravity i skriptet. Normalizerad p.ga. *, inbyggd. Vector3 axisplayer siktar höger * input. 
        // Använder Vector3 p.ga. position i Unity. 

          transform.position += axisPlayer * Time.deltaTime;
        // visual transform position - visual spawners riktiga position + på våran vector som är 1*X * delta.

        

        if (Input.GetButtonDown(fire))// Reagerar bara 1 frame när knapp trycks ner.
        {
            SpawnBox();
        }
    }

    void SpawnBox()
    {
        GameObject myBox = Instantiate(blockPreFab, transform.position, Quaternion.identity);
        // Skapat instans gameobject blockPreFab, skapade på position som är visualSpawner, ingen rotation "Quaternion". 

        myBox.GetComponent<Rigidbody2D>().velocity = Vector3.down * startDownForce;
        // Spawnar mybox, tar komponent RigidBody2d, lägger in velocity och använder gravity för att gå neråt. 
    }
}
