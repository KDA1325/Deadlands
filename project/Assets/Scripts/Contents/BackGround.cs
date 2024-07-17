using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    void Start()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.drawMode = SpriteDrawMode.Tiled;

        float height = 2f * Camera.main.orthographicSize;
        float width = height * Camera.main.aspect;

        spriteRenderer.size = new Vector3(width, height);
    }
}
