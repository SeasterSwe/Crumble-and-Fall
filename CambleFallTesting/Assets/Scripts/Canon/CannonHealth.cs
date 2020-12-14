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
    private Color originalColors;
    public GameObject explotion;
    public Color blinkColor;
    private void Awake()
    {
        foreach (SpriteRenderer child in GetComponentsInChildren<SpriteRenderer>())
            sprites.Add(child);

        sprites.Remove(transform.Find("LoadImage").GetComponent<SpriteRenderer>());
        sprites.Add(GetComponent<SpriteRenderer>());

        originalColors = sprites[0].color;
        currentHeatlh = startHealth;
    }
    bool canTakeDmg = true;
    public void TakeDmg(float amount = 1, bool playSound = true)
    {
        if (canTakeDmg)
        {
            canTakeDmg = false;
            currentHeatlh -= amount;
            StartCoroutine(FadeSprite(0.3f, 5));
            healthBar.UpdateFillAmount(currentHeatlh / startHealth);
            if (playSound)
                SoundManager.PlaySound(SoundManager.Sound.CannonHurtSound);

            if (currentHeatlh <= 0)
            {
                Death();
            }
        }
    }

    public void TakeDmg(GameObject particle, float amount = 1, bool playSound = true)
    {
        if (canTakeDmg)
        {
            GameObject particleClone = Instantiate(particle, transform.position, particle.transform.rotation);
            canTakeDmg = false;
            currentHeatlh -= amount;
            StartCoroutine(FadeSprite(0.3f, 5));
            healthBar.UpdateFillAmount(currentHeatlh / startHealth);
            
            if (playSound)
                SoundManager.PlaySound(SoundManager.Sound.CannonHurtSound);

            if (currentHeatlh <= 0)
            {
                Death();
            }
        }
    }

    public void TakeDmg(SoundManager.Sound sound, GameObject particle, float amount = 1)
    {
        if (canTakeDmg)
        {
            GameObject particleClone = Instantiate(particle, transform.position, particle.transform.rotation);
            canTakeDmg = false;
            currentHeatlh -= amount;
            StartCoroutine(FadeSprite(0.3f, 5));
            healthBar.UpdateFillAmount(currentHeatlh / startHealth);

            SoundManager.PlaySound(sound);

            if (currentHeatlh <= 0)
            {
                Death();
            }
        }
    }

    IEnumerator FadeSprite(float delay, int amount)
    {
        float t = (delay / 2f);
        for (int i = 0; i < amount; i++)
        {
            foreach (SpriteRenderer sprite in sprites)
                sprite.color = blinkColor;

            yield return new WaitForSeconds(t);
            foreach (SpriteRenderer sprite in sprites)
                sprite.color = originalColors;

            yield return new WaitForSeconds(t);
        }
        canTakeDmg = true;
    }
    void Death()
    {
        Camera.main.GetComponent<CameraZoom>().ZoomOnObj(gameObject, 3f);
        StartCoroutine(BrainDead());
    }
    IEnumerator BrainDead()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject exp = Instantiate(explotion, transform.position, explotion.transform.rotation);
        Destroy(gameObject);
        print(gameObject.name + " Lost");

    }
}
