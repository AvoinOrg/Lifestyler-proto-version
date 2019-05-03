using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToDirection : MonoBehaviour
{
    public Vector2 dir;
    public float speed = 1.0f;

    void Update()
    {
        transform.Translate(dir * speed * MiniGameManager.Instance.currentSpeed * Time.deltaTime);
    }
}
