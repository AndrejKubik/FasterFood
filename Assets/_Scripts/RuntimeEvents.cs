using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeEvents : MonoBehaviour
{
    [SerializeField] private GameEvent spacePressed;
    public static GameEvent SpacePressed;

    private void Awake()
    {
        SpacePressed = spacePressed;
    }
}
