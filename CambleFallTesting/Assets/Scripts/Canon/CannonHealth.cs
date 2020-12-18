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
    private Animator animator;
    public AudioClip audioClip;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        foreach (SpriteRenderer child in GetComponentsInChildren<SpriteRenderer>())
            sprites.Add(child);
        
        sprites.Remove(transform.Find("LoadImage").GetComponent<SpriteRenderer>());
        sprites.Add(GetComponent<SpriteRenderer>());

        originalColors = sprites[0].color;
        currentHeatlh = GameStats.cannonStartHealth;
        startHealth = GameStats.cannonStartHealth;
    }
    [HideInInspector]
    public bool canTakeDmg = true;
    public void TakeDmg(float amount = 1, bool playSound = true)
    {
        if (canTakeDmg)
        {
            animator.SetTrigger("TakeDmg");
            canTakeDmg = false;
            currentHeatlh -= amount;
            healthBar.UpdateFillAmount(currentHeatlh / startHealth);
            if (playSound)
                SoundManager.PlaySound(SoundManager.Sound.CannonHurtSound);

            if (currentHeatlh <= 0)
            {
                Death();
            }
            else 
                StartCoroutine(FadeSprite(0.3f, 5));

        }
    }

    public void TakeDmg(GameObject particle, float amount = 1, bool playSound = true)
    {
        if (canTakeDmg)
        {
            GameObject particleClone = Instantiate(particle, transform.position, particle.transform.rotation);
            canTakeDmg = false;
            currentHeatlh -= amount;
            healthBar.UpdateFillAmount(currentHeatlh / startHealth);

            if (playSound)
                SoundManager.PlaySound(SoundManager.Sound.CannonHurtSound);

            if (currentHeatlh <= 0)
            {
                Death();
            }
            else
                StartCoroutine(FadeSprite(0.3f, 5));
        }
    }

    public void TakeDmg(SoundManager.Sound sound, GameObject particle, float amount = 1)
    {
        if (canTakeDmg)
        {
            animator.SetTrigger("TakeDmg");
            GameObject particleClone = Instantiate(particle, transform.position, particle.transform.rotation);
            canTakeDmg = false;
            currentHeatlh -= amount;
            healthBar.UpdateFillAmount(currentHeatlh / startHealth);

            SoundManager.PlaySound(sound);

            if (currentHeatlh <= 0)
            {
                Death();
            }
            else
                StartCoroutine(FadeSprite(0.3f, 5));
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
        animator.SetTrigger("ExitTakeDmg");
        canTakeDmg = true;
    }
    void Death()
    {
        Camera.main.GetComponent<CameraZoom>().ZoomOnObj(gameObject, 3f);
        StartCoroutine(BrainDead());
    }
    IEnumerator BrainDead()
    {
        animator.SetTrigger("Death");
        GameObject.FindGameObjectWithTag("Music").GetComponent<BackRoundMusic>().SwapToHype();
        yield return new WaitForSeconds(0.8f);
        //GameObject exp = Instantiate(explotion, transform.position, explotion.transform.rotation);
        GameState.TogglegameStatesForward();

    }
}
