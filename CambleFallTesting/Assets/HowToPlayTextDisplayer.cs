using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HowToPlayTextDisplayer : MonoBehaviour
{
    public int showVal;
    private string text;
    TextMeshProUGUI textMeshProUGUI;

    //kanske spara en string array å ändra från den, men detta funkar för nu
    private void Start()
    {
        textMeshProUGUI = GetComponent<TextMeshProUGUI>();
        text = textMeshProUGUI.text;
    }
    void Update()
    {
        if (HowToPlayLauncherCamera.howToPlayerLauncherPos == showVal)
            textMeshProUGUI.text = text;
        else
            textMeshProUGUI.text = "";
    }
}
