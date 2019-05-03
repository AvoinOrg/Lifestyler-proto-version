using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Parent class for mini games
/// Handles starting and ending
/// Provides commonly used members and functions
/// </summary>
public class MiniGame : MonoBehaviour
{
    protected bool running = false;
    public AnimateLocalScale textAnimation;
    public string[] winStrings;
    public string[] loseStrings;

    /// <summary>
    /// Game introductions, animates the title for 1 second
    /// and calls StartGame after that
    /// </summary>
    public virtual void Start()
    {
        // Animate instructions and start game
        Invoke("StartGame", 1.0f);
        textAnimation.Play();
    }

    /// <summary>
    /// This gets called automatically after game has been introduced
    /// </summary>
    public virtual void StartGame()
    {
        running = true;
    }

    /// <summary>
    /// Call this to end game and go back to meta game after 1 second
    /// </summary>
    public void EndGame()
    {
        running = false;
        textAnimation.gameObject.SetActive(false);
        Invoke("End", 2.0f);
    }

    void End()
    {
        MiniGameManager.Instance.StartCoroutine("ShowMeteGame");
    }

    /// <summary>
    /// Get delta time adjusted with game speed
    /// </summary>
    /// <returns></returns>
    public float GetFrameDelta()
    {
        return MiniGameManager.Instance.currentSpeed * Time.deltaTime;
    }

    /// <summary>
    /// Return one of victory strings for info
    /// </summary>
    /// <returns></returns>
    public string GetVictoryString()
    {
        return winStrings[Random.Range(0, winStrings.Length)];
    }

    /// <summary>
    /// Return one of lose strings for info
    /// </summary>
    /// <returns></returns>
    public string GetLoseString()
    {
        return loseStrings[Random.Range(0, loseStrings.Length)];
    }

}
