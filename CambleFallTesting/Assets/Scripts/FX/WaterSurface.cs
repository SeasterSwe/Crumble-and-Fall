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
    float t = 0;
    float drownRate = 1f;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player"))
            return;

        t += Time.deltaTime;
        if (t >= drownRate)
        {
            t = 0;
            SoundManager.PlaySound(SoundManager.Sound.CannonDrownSound, collision.transform.position);
            GameObject waterBubble = Instantiate(waterParticle, collision.transform.position, waterParticle.transform.rotation);
            collision.GetComponent<CannonHealth>().TakeDmg(1f, false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
            t = 2;

        if (gjordesFörAttRetaRobert && Time.time > 2f)
        {
            StartCoroutine(ParticleDelay());
            GameObject waterClone = Instantiate(waterSplash, collision.transform.position, waterSplash.transform.rotation);
        }

    }

    bool gjordesFörAttRetaRobert = true;
    IEnumerator ParticleDelay()
    {
        gjordesFörAttRetaRobert = false;
        yield return new WaitForSeconds(0.2f);
        gjordesFörAttRetaRobert = true;
    }

    //void Splash(Vector3 pos)
    //{
    //    ParticleSystem p = waterParticle.GetComponent<ParticleSystem>();
    //    p.startSpeed = speed1;
    //}
}
