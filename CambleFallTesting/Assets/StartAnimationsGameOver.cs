using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartAnimationsGameOver : MonoBehaviour
{
    private void OnEnable()
    {
        GameObject.FindGameObjectWithTag("Finish").GetComponent<RoundTracker>().ActivateAnimations();
    }
}
