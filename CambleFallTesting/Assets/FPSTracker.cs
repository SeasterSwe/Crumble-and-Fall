using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSTracker : MonoBehaviour
{
    TextMeshProUGUI text;
    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }
    float t = 0;
    private void LateUpdate()
    {
        if (t < Time.time)
        {
            t = 0.356f + Time.time;
            int fps = (int)(1f / Time.unscaledDeltaTime);
            text.text = fps.ToString();
        }

    }
}
