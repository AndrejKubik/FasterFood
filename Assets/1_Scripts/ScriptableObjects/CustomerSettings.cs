using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CustomerSettings", menuName = "GameSettings/Customers")]
public class CustomerSettings : ScriptableObject
{
    [Range(0.15f, 2f)] public float SpawnCooldown = 2f;
}
