using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class HowToPlayButtonFix : MonoBehaviour
{
    public GameObject canvas;
    public GameObject[] buttons;
    //int currentState;
    void Start()
    {
        //currentState = HowToPlayLauncherCamera.howToPlayerLauncherPos;
        var b = canvas.GetComponentsInChildren<Button>();
        buttons = new GameObject[b.Length];
        print(b.Length);
        int n = 0;
        foreach(Button button in b)
        {
            buttons[n] = button.gameObject;
            n++;
        }    
    }

    void Update()
    {
        if(buttons[0] != null)
        {
            if (HowToPlayLauncherCamera.howToPlayerLauncherPos == 0)
                FadeOut(buttons[0]);
            else
                FadeIn(buttons[0]);
        }
        if(buttons[1] != null)
        {
            if (HowToPlayLauncherCamera.howToPlayerLauncherPos == 2)
                FadeOut(buttons[1]);
            else
                FadeIn(buttons[1]);
        }
    }

    void FadeOut(GameObject button)
    {
        Color color = button.GetComponent<Image>().color;
        color.a = 0f;
        button.GetComponent<Image>().DOColor(color, 0.5f);
        //button.GetComponent<RectTransform>().DOScale(Vector3.zero, 2.2f);//.OnComplete(DeactivateLeft);
    }
    void FadeIn(GameObject button)
    {
        Color color = button.GetComponent<Image>().color;
        color.a = 1f;
        button.GetComponent<Image>().DOColor(color, 0.5f);
        //button.GetComponent<RectTransform>().DOScale(Vector3.zero, 2.2f);//.OnComplete(DeactivateRight);
    }

    //void DeactivateLeft()
    //{
    //    buttons[0].SetActive(false);
    //}
    //void DeactivateRight()
    //{
    //    buttons[1].SetActive(false);
    //}
}
