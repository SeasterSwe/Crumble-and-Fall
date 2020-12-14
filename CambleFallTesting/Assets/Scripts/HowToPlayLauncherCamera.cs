using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HowToPlayLauncherCamera : MonoBehaviour
{
    public Ease ease;
    public Transform[] cameraPositions;
    public static int howToPlayerLauncherPos = 0;
    void Start()
    {
        howToPlayerLauncherPos = 0;
        transform.position = cameraPositions[0].position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            ChangeScene(-1);

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            ChangeScene(1);
    }

    public void ChangeScene(int direction)
    {
        howToPlayerLauncherPos += direction;
        if (howToPlayerLauncherPos >= cameraPositions.Length)
        {
            howToPlayerLauncherPos = cameraPositions.Length - 1;
            return;
        }
        else if(howToPlayerLauncherPos < 0)
        {
            howToPlayerLauncherPos = 0;
            return;
        }

        transform.DOMove(cameraPositions[howToPlayerLauncherPos].position, 1f).SetEase(ease);
    }
}
