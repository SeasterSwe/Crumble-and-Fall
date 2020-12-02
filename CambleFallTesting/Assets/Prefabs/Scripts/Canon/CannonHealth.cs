using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonHealth : MonoBehaviour
{
    [HideInInspector]
    public BarBase healthBar;
    public float startHealth;
    //TODO : Private when Lose state is set.
    public float currentHeatlh;
    private List<SpriteRenderer> sprites = new List<SpriteRenderer>();
    Color blinkColor = new Color(144, 104, 59, 159);
    private void Awake()
    {
        foreach(SpriteRenderer child in GetComponentsInChildren<SpriteRenderer>())
            sprites.Add(child);

        sprites.Remove(transform.Find("LoadImage").GetComponent<SpriteRenderer>());
        sprites.Add(GetComponent<SpriteRenderer>());

        currentHeatlh = startHealth;
    }
    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //        TakeDmg();
    //}
    public void TakeDmg(float amount = 1)
    {
        currentHeatlh -= amount;
        StartCoroutine(FadeSprite(0.3f, 5));
        healthBar.UpdateFillAmount(currentHeatlh / startHealth);
        if (currentHeatlh <= 0)
        {
            Death();
        }
    }
    IEnumerator FadeSprite(float delay, int amount)
    {
        float t = (delay / 2f);
        Color originalColor = sprites[0].color;
        for (int i = 0; i < amount; i++)
        {
            foreach(SpriteRenderer sprite in sprites)                        
                sprite.color = blinkColor;
            
            yield return new WaitForSeconds(t);
            foreach (SpriteRenderer sprite in sprites)
                sprite.color = originalColor;

            yield return new WaitForSeconds(t);
        }
    }
    void Death()
    {
        //GameState.currentState = GameState.TogglegameStatesForward
        print(gameObject.name + " Lost");
    }
}
