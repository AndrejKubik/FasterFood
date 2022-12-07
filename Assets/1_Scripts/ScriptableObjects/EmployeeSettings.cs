using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EmployeeSettings", menuName = "GameSettings/Employees")]
public class EmployeeSettings : ScriptableObject
{
    public float BaseOrderPrepTime = 5f;
    public float MinOrderPrepTime = 1f;
}
