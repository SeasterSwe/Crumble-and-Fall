using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarBase : MonoBehaviour
{
    public GameObject cannon;
    public float heightOverCannon = 1;
    private Image bar;
    protected virtual void Start()
    {
        bar = transform.Find("Bar").GetComponent<Image>();
    }
    void Update()
    {
        transform.position = cannon.transform.position + Vector3.up * heightOverCannon;
    }
    public void UpdateFillAmount(float amount)
    {
        bar.fillAmount = amount;
    }
}
