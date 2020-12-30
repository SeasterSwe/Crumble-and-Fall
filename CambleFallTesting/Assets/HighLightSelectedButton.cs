using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighLightSelectedButton : MonoBehaviour
{
    public Button selectedModeButton;
    public Button selectedRoundButton;
    private Color color;
    private Vector3 normalScale;
    private void Start()
    {
        color = Color.white;
        color.a = 170 / 255f;
        normalScale = Vector3.one * 3.2872f;
    }

    public void SwapSelectedModeButton(Button button)
    {
        if (selectedModeButton != null)
        {
            var colors = selectedModeButton.colors;
            colors.normalColor = color;
            selectedModeButton.colors = colors;
            selectedModeButton.gameObject.GetComponent<RectTransform>().localScale = normalScale;
        }
        selectedModeButton = button;
        selectedModeButton.gameObject.GetComponent<RectTransform>().localScale = Vector3.one * 4f;
        
        var colors2 = selectedModeButton.colors;
        colors2.normalColor = Color.white;
        selectedModeButton.colors = colors2;
    }
    public void SwapSelectedRoundButton(Button button)
    {
        if (selectedRoundButton != null)
        {
            var colors = selectedRoundButton.colors;
            colors.normalColor = color;
            selectedRoundButton.colors = colors;
            selectedRoundButton.gameObject.GetComponent<RectTransform>().localScale = normalScale;
        }

        selectedRoundButton = button;
        selectedRoundButton.gameObject.GetComponent<RectTransform>().localScale = Vector3.one * 4f;

        var colors2 = selectedRoundButton.colors;
        colors2.normalColor = Color.white;
        selectedRoundButton.colors = colors2;
    }

}
