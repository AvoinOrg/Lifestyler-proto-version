using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    void OnMouseDrag()
    {
        if (enabled)
        {
            Vector3 pos = new Vector3(Input.mousePosition.x, Input.mousePosition.y, -1.0f * (Camera.main.transform.position.z));
            transform.position = Camera.main.ScreenToWorldPoint(pos);
        }
    }
}
