using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateCanons : MonoBehaviour
{
    public GameObject cannonPlOne;
    public GameObject cannonPlTwo;

    // Start is called before the first frame update
    void Start()
    {
        cannonPlOne.SetActive(false);
        cannonPlTwo.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (GameState.currentState == GameState.gameStates.StartFight)
        {
            cannonPlOne.SetActive(true);
            cannonPlTwo.SetActive(true);
        }
    }
}
