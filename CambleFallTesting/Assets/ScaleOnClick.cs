using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class ScaleOnClick : MonoBehaviour
{
    Vector3 startScale;
    RectTransform rectTransform;
    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        startScale = rectTransform.localScale;   
    }
    public void OnClick()
    {
        startScale = rectTransform.localScale;
        rectTransform.DOScale(startScale + (Vector3.one * 0.2f), 0.1f).OnComplete(ResetScale);
    }
    void ResetScale()
    {
        rectTransform.DOScale(startScale, 0.1f);
    }

}
