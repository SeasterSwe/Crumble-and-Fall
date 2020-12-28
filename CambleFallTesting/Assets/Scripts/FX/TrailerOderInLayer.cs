using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerOderInLayer : MonoBehaviour
{
    public bool OverideAnim = false;
    // Start is called before the first frame update
    void Start()
    {
        int u = 0;
        for (int i = 0; i < transform.childCount; i++)
        {
           // Animation anim = transform.GetChild(i).GetComponent<Animation>();
           
            Animator ar = transform.GetChild(i).GetComponent<Animator>();
            ar.StartPlayback();
            ar.speed = Random.Range(0, 4);
            ar.SetInteger("State", 1);
            u++;
            if(u > 4)
            {
                u = 0;
            }
            StartCoroutine( PlayRandom(ar,  1));
        }
    }
    IEnumerator PlayRandom(Animator ar, float v)
    {
        yield return null;
        
        yield return new WaitForSeconds(v);
        ar.speed = 1;
        ar.SetInteger("State", 0);
        print("Normal");

        yield return new WaitForSeconds(11.25f);
        ar.SetInteger("State", 1);
        OverideAnim = true;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            SpriteRenderer spR = transform.GetChild(i).GetComponent<SpriteRenderer>();
            spR.sortingOrder = (int) (spR.transform.position.x + 20 - spR.transform.position.y) * 10;
            if (OverideAnim)
            {
                transform.GetChild(i).GetComponent<Animator>().SetInteger("State", 1);
            }   
        }


        
    }
}
