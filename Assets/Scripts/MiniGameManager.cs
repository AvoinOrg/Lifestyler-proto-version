using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]
public class MiniGameInfo
{
    public string name;
}

public class MiniGameManager : MonoBehaviour
{
    public static MiniGameManager Instance;

    public MiniGameInfo[] scenes;
    public float currentSpeed = 1.0f;
    public AnimateLocalScale animateTexts;
    public Text metaText;
    public Text additonalText;
    public GameObject runningBearScene;
    public GameObject victoryBear;
    public MeshRenderer bearBgRenderer;

    int currentOpenScene = -1;
    int lastGame = -1;
    bool loading = false;
    bool inGame = false;
    int gamesPlayed = 0;
    int lives = 1;
    float showMetaTime = 2.0f;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        NewGame();
    }

    void NewGame()
    {
        Debug.Log("New game");
        metaText.text = "";
        additonalText.text = "";
        gamesPlayed = 0;
        currentSpeed = 1.0f;
        Invoke("NextGame", showMetaTime);
    }

    void NextGame()
    {
        StartCoroutine("ShowNextGame");
    }

    IEnumerator ShowNextGame()
    {
        inGame = true;
        loading = true;

        Fader.Instance.FadeIn(Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f));

        // Wait until fade is complete, before loading a scene
        yield return new WaitForSeconds(Fader.Instance.fadeTime);

        // Hide meta scene objects
        runningBearScene.SetActive(false);

        // Load random mini game

        int nextGame = -1;
        if (scenes.Length > 1)
        {
            do
            {
                nextGame = Random.Range(0, scenes.Length);
            }
            while (nextGame == lastGame);
        }
        else
        {
            nextGame = 0;
        }

        currentOpenScene = nextGame;
        SceneManager.LoadScene(scenes[currentOpenScene].name, LoadSceneMode.Additive);
        Debug.Log("Next game: " + scenes[currentOpenScene].name);

        Fader.Instance.FadeOut();

        loading = false;
    }

    IEnumerator ShowMeteGame()
    {
        loading = true;

        Fader.Instance.FadeIn(Random.ColorHSV(0.0f, 1.0f, 1.0f, 1.0f, 1.0f, 1.0f));

        // Wait until fade is complete, before unloading a scene
        yield return new WaitForSeconds(Fader.Instance.fadeTime);

        // When we show meta game, we close previous mini game
        if (currentOpenScene >= 0)
        {
            lastGame = currentOpenScene;
            SceneManager.UnloadSceneAsync(scenes[currentOpenScene].name);
            currentOpenScene = -1;
        }

        // Hide victory bear
        victoryBear.SetActive(false);

        // Clear text
        metaText.text = "";
        additonalText.text = "";

        Fader.Instance.FadeOut();

        // Increase game speed if we have played enought
        gamesPlayed++;

        if (gamesPlayed > 8)
        {
            SpeedUpTo(9.0f);
        }
        else if(gamesPlayed > 7)
        {
            SpeedUpTo(8.0f);
        }
        else if(gamesPlayed > 6)
        {
            SpeedUpTo(7.0f);
        }
        else if(gamesPlayed > 5)
        {
            SpeedUpTo(6.0f);
        }
        else if(gamesPlayed > 4)
        {
            SpeedUpTo(5.0f);
        }
        else if (gamesPlayed > 3)
        {
            SpeedUpTo(4.0f);
        }
        else if (gamesPlayed > 2)
        {
            SpeedUpTo(3.0f);
        }
        else if (gamesPlayed > 1)
        {
            SpeedUpTo(2.0f);
        }
        else if (gamesPlayed > 0)
        {
            SpeedUpTo(1.5f);
        }
        Invoke("HideSpeedUpText", showMetaTime);

        loading = false;
        inGame = false;

        // Check did we lose the game,
        // if not, go to next game
        if (lives <= 0)
        {
            Debug.Log("Game over!");
            metaText.text = "Game Over!";
            CancelInvoke("HideSpeedUpText");
            Invoke("FadeToGameOverBlack", 1.0f - Fader.Instance.fadeTime);
            Invoke("RestartGame", 1.0f);
        }
        else
        {
            // Show meta scene objects
            runningBearScene.SetActive(true);

            Invoke("NextGame", showMetaTime);
        }
    }

    void FadeToGameOverBlack()
    {
        Fader.Instance.FadeIn(Color.black);
    }

    void RestartGame()
    {
        SceneManager.LoadScene(0);
    }

    void HideSpeedUpText()
    {
        metaText.text = "";
    }

    void SpeedUpTo(float v)
    {
        currentSpeed = v;
        Invoke("ShowSpeedUp", 1.0f);
    }

    void ShowSpeedUp()
    {
        metaText.text = "SPEED UP!";
        metaText.GetComponent<AnimateLocalScale>().Play();
        animateTexts.Play();
    }

    public void WinGame(string info)
    {
        additonalText.text = info;
        metaText.text = "GOOD!";
        metaText.GetComponent<AnimateLocalScale>().Play();

        victoryBear.SetActive(true);
        victoryBear.GetComponentInChildren<Animator>().SetTrigger("Victory");
        FaderForMaterial.Instance.FadeIn(new Color(1.0f, 0.53f, 0.0f));
        animateTexts.Play();

        Debug.Log("Game won!");
    }

    public void LoseGame(string info)
    {
        additonalText.text = info;
        metaText.text = "FAIL!";
        metaText.GetComponent<AnimateLocalScale>().Play();

        lives--;

        victoryBear.SetActive(true);
        victoryBear.GetComponentInChildren<Animator>().SetTrigger("Failure");
        FaderForMaterial.Instance.FadeIn(Color.red);
        animateTexts.Play();

        Debug.Log("Lost game. Lives left: " + lives);
    }

}
