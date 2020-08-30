using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursor : MonoBehaviour
{
    void Update()
    {
        Cursor.visible = false;
        Vector2 mousePos = Input.mousePosition;
        transform.position = mousePos;
    }
}
