using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class BackButton : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EventSystem eventSystem = GameObject.Find("EventSystem").GetComponent<EventSystem>();
            ExecuteEvents.Execute(GetComponent<Button>().gameObject, new BaseEventData(eventSystem), ExecuteEvents.submitHandler);
        }
    }
}
