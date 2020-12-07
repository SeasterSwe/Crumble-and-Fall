using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eye : MonoBehaviour
{
    public Transform eyeWhite;
    public Transform pupil;
    public float pupilDistance = 1;

    void Update()
    {
        //Vector3 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        //float angle = (Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg) - 90f;
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        RobbansLösning();
    }

    void RobbansLösning()
    {
        Vector3 targetDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        

        pupil.position = (targetDir - eyeWhite.position).normalized * pupilDistance;
        pupil.position = Vector3.Scale(pupil.position, eyeWhite.localScale);
        pupil.position += eyeWhite.position;
    }


}

