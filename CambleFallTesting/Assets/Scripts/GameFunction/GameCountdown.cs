using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameCountdown : MonoBehaviour
{
    public float startTime = 30f;
   // public TextMeshProUGUI countDownText;
    public string text;

    public bool colorChanging = false;
    public Gradient colorGradient;
    private float colorVal = 1;
    public GameObject playerLeft; //fixa sen
    public GameObject playerRight; //fixa sen

    private void Start()
    {
        StartCoroutine(CountDownTimer(startTime));
    }

    private void Update()
    {
        if (colorChanging)
        {
            colorVal = Mathf.PingPong(Time.time, 1);
        //    countDownText.color = colorGradient.Evaluate(colorVal);
        }
    }

    IEnumerator CountDownTimer(float time)
    {
        while (time >= 0)
        {
            if (time <= 1)
                break;

            time -= Time.deltaTime;
            ChangeText(text, Mathf.FloorToInt(time)); //floor to int???
            yield return null;
        }

        ChangeText("GO GO GO!!!!");
        ActivateAllBlocks();

        playerLeft.SetActive(true);
        playerRight.SetActive(true);

        yield return new WaitForSeconds(3f);
        
      //  Destroy(countDownText);

        yield return new WaitForEndOfFrame();
    }

    void ActivateAllBlocks()
    {
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach(GameObject obj in blocks)
        {
            obj.GetComponent<Rigidbody2D>().simulated = true;
            //obj.GetComponent<Rigidbody2D>().freezeRotation = false;
        }
    }

    void ChangeText(string text)
    {
   //     countDownText.text = text;
    }

    void ChangeText(string text, float val)
    {
    //    countDownText.text = text + val;
    }
}
