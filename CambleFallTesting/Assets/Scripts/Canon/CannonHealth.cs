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
    private Color originalColors;
    public GameObject explotion;
    private void Awake()
    {
        foreach(SpriteRenderer child in GetComponentsInChildren<SpriteRenderer>())
            sprites.Add(child);

        sprites.Remove(transform.Find("LoadImage").GetComponent<SpriteRenderer>());
        sprites.Add(GetComponent<SpriteRenderer>());

        originalColors = sprites[0].color;
        currentHeatlh = startHealth;
    }

    public void TakeDmg(float amount = 1, bool playSound = true)
    {
        currentHeatlh -= amount;
        StartCoroutine(FadeSprite(0.3f, 5));
        healthBar.UpdateFillAmount(currentHeatlh / startHealth);
        if(playSound)
            SoundManager.PlaySound(SoundManager.Sound.CannonHurtSound);

        if (currentHeatlh <= 0)
        {
            Death();
        }
    }
    IEnumerator FadeSprite(float delay, int amount)
    {
        float t = (delay / 2f);
        for (int i = 0; i < amount; i++)
        {
            foreach(SpriteRenderer sprite in sprites)                        
                sprite.color = blinkColor;
            
            yield return new WaitForSeconds(t);
            foreach (SpriteRenderer sprite in sprites)
                sprite.color = originalColors;

            yield return new WaitForSeconds(t);
        }
    }
    void Death()
    {
        GameObject exp = Instantiate(explotion, transform.position, explotion.transform.rotation);
        //zoom in and out?
        print(gameObject.name + " Lost");
    }
}
