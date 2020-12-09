using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CannonLoadBar : BarBase
{
    protected override void Start()
    {
        bar = transform.Find("AmmoBar").GetComponent<Image>();
        cannon.GetComponent<Cannon>().loadBar = this;
    }
}
