using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSurface : MonoBehaviour
{
    //public int numberOfPoints;
    //public float length;
    //public float waveHeight = 2;

    //private float distBetween;
    //private LineRenderer line;
    //private List<Vector3> points = new List<Vector3>(); 
    public GameObject waterParticle;
    public GameObject waterSplash;
    //public float speed1, speed2;
    //public int count = 10;
    //private int orbitalX = 0;
    void Start()
    {
        //line = GetComponent<LineRenderer>();
        //line.positionCount = numberOfPoints;
    }

    void Update()
    {
        //DrawSurfaceLine();
    }
    void DrawSurfaceLine()
    {
        //points.Clear();
        //float halfWaveHeight = waveHeight * 0.5f;
        //float step = length / numberOfPoints;
        //for (int i = 0; i < numberOfPoints; ++i)
        //{
        //    points.Add(new Vector3(i * step, Mathf.Sin(step * i + Time.time) * halfWaveHeight, 0));
        //    line.SetPosition(i, points[i]);
        //}
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
        {
            return;
        }

        collision.GetComponent<CannonHealth>().TakeDmg(SoundManager.Sound.CannonDrownSound, waterParticle, 1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var obj = collision.gameObject;
        if(obj.GetComponent<BlockType>() != null)
        {
            SlowStuffDown(obj);
        }

        if (canSpawnParticle && Time.timeSinceLevelLoad > 2f)
        {
            StartCoroutine(ParticleDelay());
            GameObject waterClone = Instantiate(waterSplash, collision.transform.position, waterSplash.transform.rotation);
        }
    }

    void SlowStuffDown(GameObject obj)
    {
        Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
        if(rb != null)
        {
            rb.velocity *= 0.3f;
            rb.gravityScale = 0.75f;
        }

    }

    bool canSpawnParticle = true;
    IEnumerator ParticleDelay()
    {
        canSpawnParticle = false;
        yield return new WaitForSeconds(0.1f);
        canSpawnParticle = true;
    }

    //void Splash(Vector3 pos)
    //{
    //    ParticleSystem p = waterParticle.GetComponent<ParticleSystem>();
    //    p.startSpeed = speed1;
    //}
}
