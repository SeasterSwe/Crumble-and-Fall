using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class animTest : MonoBehaviour
{
    public Animator banim;
    public int i = 0;
    // Start is called before the first frame update
    void Start()
    {
        banim.SetInteger("State", 2);
    }

    // Update is called once per frame
    void Update()
    {
      if(  Input.GetButtonDown("FirePlayerOne")){
            banim.SetInteger("State", i);
        }
    }
}
