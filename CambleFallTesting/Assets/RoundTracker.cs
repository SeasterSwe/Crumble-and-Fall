using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;

//Asså de funkar :D mvh Jakob
public class RoundTracker : MonoBehaviour
{
    public int totalRounds = 3;
    public int roundsToWin;
    private int winsLeft;
    private int winsRight;
    public int[] wins;
    public static RoundTracker instance;
    public GameObject starPos;
    public float distBetwean;
    public Image star;
    public Image redStar;
    public Image yellowStar;
    private Image[] stars;
    public GameObject particle;

    void Start()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            totalRounds = GameStats.amountOfRounds;
            roundsToWin = Mathf.FloorToInt((totalRounds / 2f) + 1);
        }
        else
        {
            Destroy(gameObject);
        }

        if (wins.Length == 0)
            wins = new int[totalRounds];
    }
    public bool CheckIfWin()
    {
        if (winsLeft >= roundsToWin)
            return true;
        else if (winsRight >= roundsToWin)
            return true;
        else
            return false;
    }

    public bool leftPlayerWon;

    public void LeftWin()
    {
        wins[winsLeft] = 1;

        winsLeft++;
        leftPlayerWon = true;

        ChangeScene();
    }
    public void RightWin()
    {
        wins[(totalRounds - 1) - winsRight] = 2;
        //instansiate redstar in middle?
        winsRight++;
        leftPlayerWon = false;

        ChangeScene();
    }

    void ChangeScene()
    {
        GameObject.FindGameObjectWithTag("GameSceneManager").GetComponent<GameSceneManager>().ChangeScene("GameOver");
    }

    public void ActivateAnimations()
    {
        Transform[] animations = GameObject.FindGameObjectWithTag("GameOverAnimations").GetComponentsInChildren<Transform>();
        print(animations.Length);
        if (leftPlayerWon == true)
        {
            animations[1].gameObject.SetActive(false);
            animations[5].gameObject.SetActive(false);
        }
        //höger
        if (leftPlayerWon == false)
        {
            animations[2].gameObject.SetActive(false);
            animations[4].gameObject.SetActive(false);
        }
    }

    public void Setup(GameObject obj)
    {
        Vector3 sPos = obj.transform.position;
        totalRounds = GameStats.amountOfRounds;
        float fixedDist = (totalRounds - 1) * distBetwean / 2;

        starPos = obj;

        if (wins.Length == 0)
            wins = new int[totalRounds];

        Image startClone = null;

        //if in game over

        for (int i = 0; i < totalRounds; i++)
        {
            if (SceneManager.GetActiveScene().name == "GameOver")
            {
                if (!leftPlayerWon && i == totalRounds - winsRight || leftPlayerWon && i == winsLeft - 1)
                {
                    startClone = Instantiate(star, sPos + (Vector3.right * (-fixedDist + distBetwean * i)), star.transform.rotation);
                }
                else
                {
                    if (wins[i] == 1)
                        startClone = Instantiate(yellowStar, sPos + (Vector3.right * (-fixedDist + distBetwean * i)), star.transform.rotation);
                    else if (wins[i] == 2)
                        startClone = Instantiate(redStar, sPos + (Vector3.right * (-fixedDist + distBetwean * i)), star.transform.rotation);
                    else
                        startClone = Instantiate(star, sPos + (Vector3.right * (-fixedDist + distBetwean * i)), star.transform.rotation);
                }
                startClone.transform.SetParent(obj.transform);
            }
            else
            {
                if (wins[i] == 1)
                    startClone = Instantiate(yellowStar, sPos + (Vector3.right * (-fixedDist + distBetwean * i)), star.transform.rotation);
                else if (wins[i] == 2)
                    startClone = Instantiate(redStar, sPos + (Vector3.right * (-fixedDist + distBetwean * i)), star.transform.rotation);
                else
                    startClone = Instantiate(star, sPos + (Vector3.right * (-fixedDist + distBetwean * i)), star.transform.rotation);

                startClone.transform.SetParent(obj.transform);
            }
        }

        stars = starPos.GetComponentsInChildren<Image>();
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            if (leftPlayerWon == false)
                StartCoroutine(JuiceStar(redStar, stars[totalRounds - winsRight].transform.position));
            else
                StartCoroutine(JuiceStar(yellowStar, stars[winsLeft - 1].transform.position));
        }

    }
    bool spinStar;
    IEnumerator JuiceStar(Image star, Vector3 endPos)
    {
        spinStar = true;
        var text = GameObject.FindGameObjectWithTag("GameOverAnimations").GetComponent<StartAnimationsGameOver>().text;
        string textText = GameObject.FindGameObjectWithTag("GameOverAnimations").GetComponent<StartAnimationsGameOver>().Text();
        Color color = text.color;
        color.a = 0;
        text.color = color;
        text.text = "";


        yield return new WaitForSeconds(0.3f);
        Image starImage = Instantiate(star, GameObject.Find("Canvas").transform.position, star.transform.rotation);
        StartCoroutine(SpinStar(starImage));
        starImage.transform.SetParent(GameObject.Find("Canvas").transform);
        starImage.rectTransform.localScale = Vector3.zero;
        starImage.rectTransform.DOScale(Vector3.one * 2.5f, 1.4f);
        starImage.rectTransform.DOMove(starImage.transform.position + Vector3.up * 120, 1.4f);


        yield return new WaitForSeconds(1.4f);
        starImage.rectTransform.DOMove(endPos, 1f);
        starImage.rectTransform.DOScale(Vector3.zero, 1.5f);


        yield return new WaitForSeconds(0.2f);
        text.text = textText;
        text.DOColor(Color.white, 0.2f);


        yield return new WaitForSeconds(0.4f);
        starImage.DOColor(color, 0.2f);
        SpawnParticle(Camera.main.ScreenToWorldPoint(endPos));
        
        SoundManager.PlaySound(SoundManager.Sound.StarSound);    
        spinStar = false;

        if (leftPlayerWon == false)
        {
            var temp = stars[totalRounds - winsRight];
            temp.GetComponent<Image>().sprite = star.sprite;
            StartCoroutine(TweenSmallStar(temp));
        }
        else
        {
            var temp = stars[winsLeft - 1];
            temp.GetComponent<Image>().sprite = star.sprite;
            StartCoroutine(TweenSmallStar(temp));
        }
    }
    IEnumerator SpinStar(Image star)
    {
        float rotationSpeed = 360;
        float currentLerpTime = 0;
        float lerpTime = 3f;
        Vector3 startRot = Vector3.zero;
        Vector3 endRot = new Vector3(0, 0, rotationSpeed * lerpTime);
        while (spinStar)
        {
            currentLerpTime += Time.deltaTime;
            if (currentLerpTime > lerpTime)
                currentLerpTime = lerpTime;

            float t = currentLerpTime / lerpTime;
            t = t * t * (3f - 2f * t);

            star.transform.rotation = Quaternion.Euler(Vector3.Lerp(startRot, endRot, t));
            yield return null;
        }
        yield return new WaitForEndOfFrame();
    }

    void SpawnParticle(Vector3 pos)
    {
        pos.z = 0;
        GameObject clone = Instantiate(particle, pos, particle.transform.rotation);
    }

    IEnumerator TweenSmallStar(Image star)
    {
        Vector3 startScale = star.rectTransform.localScale;
        star.rectTransform.DOScale(startScale + Vector3.one * 0.3f, 0.4f);
        yield return new WaitForSeconds(0.4f);
        star.rectTransform.DOScale(startScale + Vector3.one * 0.1f, 0.4f);
    }

    public void ResetStats()
    {
        wins = new int[totalRounds];
        winsLeft = 0;
        winsRight = 0;
    }
}
