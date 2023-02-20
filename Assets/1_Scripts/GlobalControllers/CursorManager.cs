using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    [SerializeField] private Texture2D image;
    [SerializeField] private Texture2D image2;
    [SerializeField] private float delay;

    private void Start()
    {
        Cursor.SetCursor(image, new Vector2(10, 10), CursorMode.ForceSoftware);
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0)) Cursor.SetCursor(image2, new Vector2(10, 10), CursorMode.ForceSoftware);

        if (Input.GetMouseButtonUp(0)) Cursor.SetCursor(image, new Vector2(10, 10), CursorMode.ForceSoftware);
    }

    IEnumerator ChangeCursor(float delay)
    {
        Cursor.SetCursor(image2, new Vector2(10, 10), CursorMode.ForceSoftware);
        yield return new WaitForSeconds(delay);
        Cursor.SetCursor(image, new Vector2(10, 10), CursorMode.ForceSoftware);
    }
}
