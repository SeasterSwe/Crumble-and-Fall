using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DefaultExecutionOrder(1)]
public class ActivateCanons : MonoBehaviour
{
    public GameObject cannonPlOne;
    public GameObject cannonPlTwo;

    // Start is called before the first frame update
    void Awake()
    {
        Cannon[] cannons = FindObjectsOfType<Cannon>();
        if (cannonPlOne == null)
        {
            foreach (Cannon cannon in cannons)
            {
                if (cannon.transform.position.x < 0)
                {
                    cannonPlOne = cannon.gameObject;

                }
            }
        }

        if (cannonPlTwo == null)
        {
            foreach (Cannon cannon in cannons)
            {
                if (cannon.transform.position.x < 0)
                {
                    cannonPlTwo = cannon.gameObject;

                }
            }
        }


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
