using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyGame : MiniGame
{
    public Transform player;
    public GameObject[] goodEnergies;
    public Transform[] salesmanPositions;
    public Transform[] salesmen;
    public AnimationCurve walkCurve;

    Touch touch;

    float currentX;
    float targetX;
    float minX = -2.2f;
    float maxX = 2.2f;

    float upSpeed = 1.0f;
    float dragSpeed = 10.0f;
    float curveValue;
    float waveWidth = 0.2f;

    float targetY = 2.1f;
    int goodSalesManIndex = 0;

    public override void Start()
    {
        base.Start();

        // Reset player position
        player.localPosition = new Vector3(Random.Range(-1.8f, 1.8f), player.localPosition.y, player.localPosition.z);
        currentX = player.localPosition.x;
        targetX = currentX;

        // Randomize good energy symbol
        int r = Random.Range(0, goodEnergies.Length);
        for (int i = 0; i < goodEnergies.Length; ++i)
        {
            goodEnergies[i].SetActive(i == r);
        }

        // Randomize sales man positions
        int salesManIndex = Random.Range(0, 3);
        salesmen[0].position = salesmanPositions[salesManIndex].position;
        salesManIndex = Utils.IncreaseIndex(salesManIndex, 2);
        salesmen[1].position = salesmanPositions[salesManIndex].position;
        salesManIndex = Utils.IncreaseIndex(salesManIndex, 2);
        salesmen[2].position = salesmanPositions[salesManIndex].position;
    }

    void Update()
    {
        if (!running)
            return;

        if (Input.touchCount > 0)
        {
            // Get current touch change x position target by dragging
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                targetX += touch.deltaPosition.x / Screen.width;
                targetX = Mathf.Clamp(targetX, minX, maxX);
            }
        }

        // Use delta tied to game speed
        float frameDelta = GetFrameDelta();

        // Move player up with constant speed and sideways with a curve around the target position
        currentX = Mathf.Lerp(currentX, targetX, dragSpeed * frameDelta);
        curveValue += frameDelta;
        float x = currentX + walkCurve.Evaluate(curveValue) * waveWidth;
        float y = player.localPosition.y + upSpeed * frameDelta;
        player.localPosition = new Vector3(x, y, player.localPosition.z);

        // Once player reaches to of the screen, check did he win
        if (player.localPosition.y >= targetY)
        {
            // Go through all the salesmen and see who was closest
            float shortestDistance = 100000.0f;
            int closestIndex = -1;
            for (int i = 0; i < salesmen.Length; ++i)
            {
                float dist = Vector2.Distance(player.position, salesmen[i].position);

                if (dist < shortestDistance)
                {
                    shortestDistance = dist;
                    closestIndex = i;
                }
            }

            salesmen[closestIndex].GetComponentInChildren<Animator>().SetTrigger("React");

            // If the good salesman was closest, we win, otherwise we lose
            if (closestIndex == goodSalesManIndex)
            {
                MiniGameManager.Instance.WinGame(GetVictoryString());
            }
            else
            {
                MiniGameManager.Instance.LoseGame(GetLoseString());
            }

            // Game is now resolved, go back to meta screen
            EndGame();
        }

    }

}
