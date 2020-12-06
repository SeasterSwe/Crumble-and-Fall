using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevationCheck : MonoBehaviour
{
    public GameObject area;
    public Vector2 areaBounds;
    public LayerMask blockLayer;

    public float towerHight;
    public Transform uiMeter;

    private float groundlevel;
    public Collider2D highestBlock;

    // Start is called before the first frame update
    void Start()
    {
        areaBounds = area.GetComponent<Renderer>().bounds.extents * 2;
        GetGroundLevel();
    }

    public float GetGroundLevel()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down);
        if (hit.collider != null)
        {
            groundlevel = hit.point.y;
            Debug.DrawRay(hit.point, Vector2.one * 100, Color.yellow, 2);
            return groundlevel;
        }
        else
        {
            Debug.LogError("ElevationControler cant find Ground");
        }
        Debug.LogError("ElevationControler cant find Ground");
        return 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetPlayerTowerHight();
        UpdateUI();
    }

    private void GetPlayerTowerHight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position + Vector3.up * 20, areaBounds, 0, Vector2.down, Mathf.Infinity, blockLayer);
        if (hit.collider)
        {
            towerHight = hit.point.y - groundlevel;
            highestBlock = hit.collider;
            Debug.DrawRay(hit.point, hit.normal, Color.yellow, 0.1f);
        }
    }

    private void UpdateUI()
    {
        Vector3 scale = uiMeter.localScale;
        scale.y = towerHight;
        uiMeter.localScale = scale;
    }
}
