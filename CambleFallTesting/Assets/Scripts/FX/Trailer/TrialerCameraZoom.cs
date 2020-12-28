using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrialerCameraZoom : MonoBehaviour
{
    Camera cam;
    public float moveSpeed = 4;
    public float zoomSpeed = 2;
    private float orgZoom;
    public float targetZoomSize = 4;
    // Start is called before the first frame update
    void Start()
    {
        if(cam == null)
        {
            cam = Camera.main;
        }

        orgZoom = cam.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("TrailerZoom1") || Input.GetMouseButton(0))
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, Vector3.forward * -20 + Vector3.right *-8, moveSpeed* Time.deltaTime);
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetZoomSize, zoomSpeed * Time.deltaTime);
        }
        else if(Input.GetButton("TrailerZoom2") || Input.GetMouseButton(1))
        {
            cam.transform.position = Vector3.MoveTowards(cam.transform.position, Vector3.forward * -20 + Vector3.right * 8, moveSpeed * Time.deltaTime);
            cam.orthographicSize = Mathf.MoveTowards(cam.orthographicSize, targetZoomSize, zoomSpeed * Time.deltaTime);
        }
        else
        {
            cam.transform.position = Vector3.forward * -20;
            cam.orthographicSize = orgZoom;
        }
    }
}
