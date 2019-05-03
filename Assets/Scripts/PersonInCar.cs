using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonInCar : MonoBehaviour
{
    public bool checkLeft = false;
    public float checkX;
    public TransportGame tGame;

    void Update()
    {
        if (transform.childCount > 0)
        {
            if ((checkLeft && checkX < transform.position.x)
                || (!checkLeft && checkX > transform.position.x))
            {
                tGame.MissCar();
                enabled = false;
            }
        }

    }
}
