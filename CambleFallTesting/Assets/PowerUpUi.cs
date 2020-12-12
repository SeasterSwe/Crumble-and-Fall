using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PowerUpUi : MonoBehaviour
{
    public GameObject[] icons;
    TextMeshProUGUI[] texts;
    public float distDown = 10;
    public CannonHeightBonuses cannonHeightBonuses;
    public GenerateBlockToInventoryOverTime generateBlock;

    private void Awake()
    {
        CannonHeightBonuses[] c = Object.FindObjectsOfType<CannonHeightBonuses>();
        //print(c.Length);
        foreach (CannonHeightBonuses a in c)
        {
            if (a.transform.position.x < 0 && GetComponent<RectTransform>().position.x < 0)
                cannonHeightBonuses = a;
            else
                cannonHeightBonuses = a;
        }
    }
    void Start()
    {
        //har ingen aning vrf den hämtar sig själv... ngn som vet?
        var childs = gameObject.GetComponentsInChildren<RectTransform>();
        icons = new GameObject[childs.Length - 1];
        int i = 0;
        GameObject text = (Resources.Load("Text (TMP)") as GameObject);
        texts = new TextMeshProUGUI[childs.Length - 1];
        foreach (RectTransform r in childs)
        {
            if (r != this.GetComponent<RectTransform>()) //fix på att den tar sig själv
            {
                icons[i] = r.gameObject;
                GameObject textUi = Instantiate(text);
                texts[i] = textUi.GetComponent<TextMeshProUGUI>();
                textUi.transform.SetParent(r.gameObject.transform);
                textUi.transform.position = r.gameObject.transform.position + (Vector3.down * distDown);
                i++;
            }
        }


    }
    void LateUpdate()
    {
        if (GameState.currentState == GameState.gameStates.Fight)
        {
            texts[0].text = "+" + (cannonHeightBonuses.currentRotaionBonus * 100).ToString("F0").PadLeft(3, '0') + "%";
            texts[1].text = "+" + (cannonHeightBonuses.currentVelBouns * 100).ToString("F0").PadLeft(3, '0') + "%";
            texts[2].text = "+" + (generateBlock.bonus * 100).ToString("F0").PadLeft(3, '0') + "%";
        }
    }
}
