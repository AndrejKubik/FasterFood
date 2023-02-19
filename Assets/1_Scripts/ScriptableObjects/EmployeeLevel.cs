using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewEmployeeLevel", menuName = "Employees/Level")]
public class EmployeeLevel : ScriptableObject
{
    public float RecipePrepTime = 2f;
    public int RecipeCost = 5;
}
