using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 control = Vector3.right * Input.GetAxis("HorizontalPl1");
        transform.position += control * Time.deltaTime;
    }
}
