using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonLoadBar : BarBase
{
    protected override void Start()
    {
        base.Start();
        cannon.GetComponent<Cannon>().loadBar = this;
    }
}
