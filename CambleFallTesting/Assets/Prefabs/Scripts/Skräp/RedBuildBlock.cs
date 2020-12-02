using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBuildBlock : VelocityTest
{
    private void Start()
    {
        GameObject.Find("CannonPlayer2").GetComponent<Cannon>().IncreasMaxCharge(5f);
    }
}
 