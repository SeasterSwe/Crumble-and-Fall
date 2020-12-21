using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class LoadingScreen : MonoBehaviour
{
    public float loadTime;
    public Image loadBar;
    void Start()
    {
        StartCoroutine(Load(loadTime));
    }
    IEnumerator Load(float t)
    {
        //transition time = 1
        loadBar.DOFillAmount(1, t + 0.7f);//.SetEase(Ease.OutBounce);
        yield return new WaitForSeconds(t);
        GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>().ChangeScene("Game_v3");
    }
}
