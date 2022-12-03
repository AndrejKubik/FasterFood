using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopSettings", menuName = "GameSettings/Shop")]
public class ShopSettings : ScriptableObject
{
    [Header("BASE PRICES: ")]
    public float BaseCustomerPrice = 1f;
    public float BaseEmployeeCost = 10f;

    [Space(10f)] public float BaseMoneyGain = 5f;

    [Space(10f)] public float MoneyUpdateSpeed = 10f;
}
