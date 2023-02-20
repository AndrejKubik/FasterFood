using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D image;
    [SerializeField] private Texture2D image2;

    private void Start()
    {
        Cursor.SetCursor(image, new Vector2(10f, 10f), CursorMode.ForceSoftware);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Cursor.SetCursor(image2, new Vector2(10f, 10f), CursorMode.ForceSoftware);

        if (Input.GetMouseButtonUp(0)) Cursor.SetCursor(image, new Vector2(10f, 10f), CursorMode.ForceSoftware);
    }
}
