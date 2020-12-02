using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : BarBase
{
    protected override void Start()
    {
        base.Start();
        cannon.GetComponent<CannonHealth>().healthBar = this;
    }
}
