using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessageOnCollisionEnter2D : MonoBehaviour
{
    public GameObject target;
    public string message;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == target)
        {
            target.SendMessage(message, gameObject);
        }
    }
}
