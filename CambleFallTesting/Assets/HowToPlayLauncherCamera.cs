using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class HowToPlayLauncherCamera : MonoBehaviour
{
    public Ease ease;
    public Transform[] cameraPositions;
    int n = 0;
    void Start()
    {
        n = 0;
        transform.position = cameraPositions[0].position;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            ChangeScene(1);
    }

    public void ChangeScene(int direction)
    {
        n += direction;
        if (n >= cameraPositions.Length)
            n = 0;
        if (n < 0)
            n = cameraPositions.Length - 1;

        transform.DOMove(cameraPositions[n].position, 1f).SetEase(ease);
    }
}
