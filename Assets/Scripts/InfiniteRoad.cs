using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InfiniteRoad : MonoBehaviour
{
    public Transform pos1;
    public Transform pos2;
    public float speed;
    public float v;

    void Update()
    {
        v += speed * Time.deltaTime;
        if(v > 1.0f)
        {
            v -= 1.0f; 
        }
        transform.position = Vector3.Lerp(pos1.position, pos2.position, v);
    }
}
