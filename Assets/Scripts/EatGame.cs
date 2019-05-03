using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EatGame : MiniGame
{
    float timer = 0.0f;
    float gameTime = 10.0f;

    int foodEaten = 0;
    int totalGoodFood = 4;
    bool badFoodEaten = false;

    void Update()
    {
        if (!running)
            return;

        timer += GetFrameDelta();

        // If we eat bad food, lose instantly
        if(badFoodEaten)
        {
            timer += gameTime;
        }

        // Once time is over, we end the game and see if we failed or not
        if (timer > gameTime)
        {
            // Win only if we have eaten all good food and no bad
            if(foodEaten >= totalGoodFood && !badFoodEaten)
            {
                MiniGameManager.Instance.WinGame("Eat game win");
            }
            else
            {
                MiniGameManager.Instance.LoseGame("Eat game lose");
            }
            
            EndGame();
        }
    }

    public void Eat(GameObject food)
    {
        if(food.transform.tag == "Bad")
        {
            badFoodEaten = true;
        }
        else
        {
            foodEaten++;
            Debug.Log("Food eaten: " + foodEaten);

            if (foodEaten == totalGoodFood)
            {
                MiniGameManager.Instance.WinGame("Eat game win");
                EndGame();
            }
        }

        Destroy(food);
    }
}
