using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockExpression : MonoBehaviour
{

    //public enum expression { happy, scared};
    //public expression currentMood = expression.happy;
    public Sprite idle;
    public Sprite scared;
    // Start is called before the first frame update
    void Start()
    {
        SetMoodIdle();
    }

    public void SetMoodIdle()
    {
        GetComponent<SpriteRenderer>().sprite = idle;
    }
    public void SetMoodScared()
    {
        GetComponent<SpriteRenderer>().sprite = scared;

    }
}
