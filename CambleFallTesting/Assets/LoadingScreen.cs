using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public float loadTime;
    public GameObject text;
    public float scaleTime = 1;
    public Color textColor;
    Vector3 textScale;
    public Sprite[] sprites;
    public GameObject tutorial;
    void Start()
    {
        tutorial.GetComponent<Image>().sprite = sprites[Random.Range(0, sprites.Length)];
        textScale = text.GetComponent<RectTransform>().localScale;
        Scale();
        StartCoroutine(Load(loadTime));
    }

    private void Update()
    {
        if(Input.anyKeyDown)
            GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>().ChangeScene("Game_v3");
    }

    IEnumerator Load(float t)
    {
        //yield return new WaitForSeconds(0.6f);
        //loadBar.DOFillAmount(1, t + 1f);//.SetEase(Ease.OutBounce);


        yield return new WaitForSeconds(t);
        GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>().ChangeScene("Game_v3");
    }

    void Scale()   
    {
        text.GetComponent<RectTransform>().DOScale(textScale + (Vector3.one * 0.02f), scaleTime).OnComplete(UnScale);
        text.GetComponent<TextMeshProUGUI>().DOColor(textColor, scaleTime);
    }
    void UnScale()
    {
        text.GetComponent<RectTransform>().DOScale(textScale, scaleTime).OnComplete(Scale);
        text.GetComponent<TextMeshProUGUI>().DOColor(Color.white, scaleTime);
    }
}
