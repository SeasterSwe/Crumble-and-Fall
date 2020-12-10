using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HealthBar : BarBase
{
    public Ease ease;
    protected override void Start()
    {
        bar = transform.Find("HealthBar").GetComponent<Image>();
        cannon.GetComponent<CannonHealth>().healthBar = this;
    }
    public override void UpdateFillAmount(float amount)
    {
        bar.DOFillAmount(amount, 0.3f).SetEase(ease);
    }
}
