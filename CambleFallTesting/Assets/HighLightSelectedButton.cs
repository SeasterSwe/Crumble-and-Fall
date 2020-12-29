using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HighLightSelectedButton : MonoBehaviour
{
    public Button selectedModeButton;
    public Button selectedRoundButton;
    private Color color;
    private void Start()
    {
        color = Color.white;
        color.a = 170/255f;
    }

    public void SwapSelectedModeButton(Button button)
    {
        if (selectedModeButton != null)
        {
            var colors = selectedModeButton.colors;
            colors.normalColor = color;
            selectedModeButton.colors = colors;
        }

        selectedModeButton = button;

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
        }

        selectedRoundButton = button;

        var colors2 = selectedRoundButton.colors;
        colors2.normalColor = Color.white;
        selectedRoundButton.colors = colors2;
    }

}
