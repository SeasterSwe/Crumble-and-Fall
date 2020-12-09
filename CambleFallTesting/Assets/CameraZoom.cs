using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraZoom : MonoBehaviour
{
    Transform target;
    float orthographicSizeMin = 6.658269f;
    float orthographicSizeMax = 6;

    public Ease zoom;
    public Ease move;
    public float time = 1f;

    Camera camera;
    Vector3 startPos;
    private void Awake()
    {
        camera = Camera.main;
        startPos = transform.position;
        orthographicSizeMax = Camera.main.orthographicSize;
    }
    
    void Update()
    {    
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, orthographicSizeMin, orthographicSizeMax);
    }

    bool zoomed = true;
    public void ZoomOnObj(GameObject obj)
    {
        float z = camera.transform.position.z;
        Vector2 targetPos = obj.transform.position;
        Vector3 dfbipz = new Vector3(targetPos.x, targetPos.y, z);
        transform.DOMove(dfbipz, time).SetEase(move);
        camera.DOOrthoSize(orthographicSizeMin, time).SetEase(zoom);
        //StartCoroutine(ReSize(time));

    }

    public void ZoomOnObj(GameObject obj, float time)
    {
        float z = camera.transform.position.z;
        Vector2 targetPos = obj.transform.position;
        Vector3 dfbipz = new Vector3(targetPos.x, targetPos.y, z);
        transform.DOMove(dfbipz, time).SetEase(move);
        camera.DOOrthoSize(orthographicSizeMin, time).SetEase(zoom);
        StartCoroutine(ReSize(time));

    }

    IEnumerator ReSize(float time)
    {
        yield return new WaitForSeconds(time * 3f);
        NormalSize();
    }
    void NormalSize()
    {
        transform.DOMove(startPos, time).SetEase(move);
        camera.DOOrthoSize(orthographicSizeMax, time);
    }
}
