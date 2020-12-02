using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using TMPro;
public class TweenTestMOthadackawetfawegawse : MonoBehaviour
{
    public float speed = 2f;
    Vector3 startPos;
    Vector3 outOfScreenPos;
    [SerializeField] private Ease ease;
    [SerializeField] private Ease colorEase;
    public Vector3 dirOut;
    TextMeshProUGUI text;

    void Start()
    {
        outOfScreenPos = transform.position + (dirOut * 800);
        startPos = transform.position;
        transform.position = outOfScreenPos;
        transform.DOMove(startPos, speed, false).SetEase(ease).OnComplete(Woo);
        Tja();
        //StartCoroutine(RevealText());
    }
    void Tja()
    {
        text = GetComponent<TextMeshProUGUI>();
        text.DOColor(Color.cyan, 2f).SetEase(colorEase);
    }

    void Woo()
    {
        StartCoroutine(RevealText());
    }

    IEnumerator RevealText()
    {
        var originalString = "SALAMALECKO WHAWEHYGFWGHFGHAWFGWAG";
        text.text = "";

        var numCharsRevealed = 0;
        while (numCharsRevealed < originalString.Length)
        {
            while (originalString[numCharsRevealed] == ' ')
                ++numCharsRevealed;

            ++numCharsRevealed;

            text.text = originalString.Substring(0, numCharsRevealed);

            yield return new WaitForSeconds(0.07f);
        }
    }
}
