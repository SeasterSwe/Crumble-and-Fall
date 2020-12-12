using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class TweemEventUi : MonoBehaviour
{

    EventSystem eventSystem;
    public GameObject selected;
    public Vector3 selectedScale;
    void Start()
    {
        eventSystem = GetComponent<EventSystem>();
        selected = eventSystem.firstSelectedGameObject;
        selectedScale = selected.GetComponent<RectTransform>().localScale;
        Scale();
    }

    // Update is called once per frame
    void Update()
    {
        if (eventSystem.currentSelectedGameObject != null)
        {
            if (eventSystem.currentSelectedGameObject != selected)
            {
                DeScale();
                selected = eventSystem.currentSelectedGameObject;
                Scale();
            }
        }

    }

    void Scale()
    {
        selectedScale = selected.GetComponent<RectTransform>().localScale;
        selected.GetComponent<RectTransform>().DOScale(selectedScale + (Vector3.one * 0.3f), 0.3f).SetUpdate(true);
    }
    void DeScale()
    {
        selected.GetComponent<RectTransform>().DOScale(selectedScale, 0.3f).SetUpdate(true);
    }
}
