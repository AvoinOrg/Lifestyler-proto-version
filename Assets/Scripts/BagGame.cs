using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BagGame : MiniGame
{
    public Transform player;
    public Transform hand;
    public Transform bag;
    public Transform bagNub;

    float speed = 1.0f;
    bool pressed = false;
    bool win = false;
    float maxX = 3.66f;
    float bagPickupHalfWidth = 0.6f;

    public override void Start()
    {
        base.Start();
        bagNub.transform.localPosition = new Vector3(Random.Range(-0.332f, 2.095f), bagNub.transform.localPosition.y, bagNub.transform.localPosition.z);
    }

    void Update()
    {
        if (!running)
            return;

        // Player moves automatically right with constant speed modifyied by game speed
        float speedMod = win ? speed * 3.0f : speed;
        player.localPosition += Vector3.right * speedMod * GetFrameDelta();

        // Only allow one press to try to grab the bag
        if (!pressed && Input.GetMouseButtonDown(0)) //Input.touchCount > 0)
        {
            //if(Input.GetTouch(0).phase == TouchPhase.Began)
            {
                if (hand.position.x < (bag.position.x + bagPickupHalfWidth)
                    && hand.position.x > (bag.position.x - bagPickupHalfWidth))
                {
                    win = true;
                    bag.transform.parent = hand;
                    bag.transform.localPosition = Vector3.zero;
                }
                player.GetComponent<AnimateLocalScale>().Play();

                Debug.Log("Click: " + hand.position.x + "/" + bag.position.x);

                pressed = true;
            }
        }

        // Once we reach end of the screen, we can check if we managed to press in time
        if (player.localPosition.x > maxX || (pressed && !win))
        {
            if (win)
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
