using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarUi : MonoBehaviour
{
    private void OnEnable()
    {       
        GameObject.FindGameObjectWithTag("Finish").GetComponent<RoundTracker>().Setup(gameObject);
    }

}
