using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransportGame : MiniGame
{
    float timer = 0.0f;
    float gameTime = 15.0f;

    int peopleDragged = 0;
    int totalPeople = 4;

    public Transform[] bikeSeats;
    public MoveToDirection[] cars;

    public override void StartGame()
    {
        base.StartGame();
        StartCoroutine("SendCars");
    }

    IEnumerator SendCars()
    {
        for (int i = 0; i < cars.Length; ++i)
        {
            cars[i].enabled = true;
            yield return new WaitForSeconds(1.0f / MiniGameManager.Instance.currentSpeed);
        }
    }

    void Update()
    {
        if (!running)
            return;

        timer += GetFrameDelta();

        // Once time is over, we end the game and see if we failed or not
        if (timer > gameTime)
        {
            MiniGameManager.Instance.LoseGame(GetLoseString());
            EndGame();
        }
    }

    public void MissCar()
    {
        StopAllCoroutines();
        for(int i = 0; i < cars.Length; ++i)
        {
            cars[i].enabled = false;
        }

        MiniGameManager.Instance.LoseGame(GetLoseString());
        EndGame();
    }

    public void AddPerson(GameObject person)
    {
        peopleDragged++;
        Debug.Log("People dragged: " + peopleDragged);

        // Disable everything from person
        person.GetComponent<Draggable>().enabled = false;
        person.GetComponent<Rigidbody2D>().isKinematic = true;
        person.GetComponent<Collider2D>().enabled = false;
        person.GetComponent<SpriteRenderer>().flipX = false;

        // Move person to closest seat
        float closestSeatDist = 99999.9f;
        for (int i = 0; i < bikeSeats.Length; ++i)
        {
            float dist = Vector2.Distance(bikeSeats[i].position, person.transform.position);
            if (dist < closestSeatDist && bikeSeats[i].childCount == 0)
            {
                closestSeatDist = dist;
                person.transform.position = bikeSeats[i].position;
                person.transform.parent = bikeSeats[i];
            }
        }

        // Win if got all people dragged
        if (peopleDragged >= totalPeople)
        {
            MiniGameManager.Instance.WinGame(GetVictoryString());
            EndGame();
        }
    }
}
