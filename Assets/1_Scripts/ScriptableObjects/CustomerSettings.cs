using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerSettings", menuName = "GameSettings/Customers")]
public class CustomerSettings : ScriptableObject
{
    [Range(0.15f, 5f)] public float BaseSpawnCooldown = 2f;
    [HideInInspector] public float MinSpawnCooldown = 0.15f;

    public int MaxCustomersInLine = 6;

    public float CustomerMovementSpeed = 10f;
    public float EntranceFreeDelay = 0.001f;
}
