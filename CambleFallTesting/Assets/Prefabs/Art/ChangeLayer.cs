using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLayer : MonoBehaviour
{
    public int moveLayers = -10;
    public AnimationCurve ani;
    // Start is called before the first frame update
    void Start()
    {
        SpriteRenderer[] changeThis = gameObject.GetComponentsInChildren<SpriteRenderer>();
        foreach(SpriteRenderer sr in changeThis)
        {
            sr.sortingOrder += moveLayers;
        }
    }
}
