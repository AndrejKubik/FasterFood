using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopSettings", menuName = "GameSettings/Shop")]
public class ShopSettings : ScriptableObject
{
    [Header("BASE PRICES: ")]
    public float BaseCustomerPrice = 1f;
    public float BaseEmployeeCost = 10f;
    public float BaseNewRecipeCost = 15f;
    public float BaseServiceSpeedCost = 5f;
    public float BaseCustomerSpawnSpeedCost = 5f;

    [Header("PRICE INCREASE MULTIPLIERS: ")]
    public float CustomerPriceMultiplier = 0f;
    public float EmployeePriceMultiplier = 2f;
    public float RecipePriceMultiplier = 3f;
    public float ServiceSpeedPriceMultiplier = 1.2f;
    public float CustomerSpawnPriceMultiplier = 1.5f;

    [Space(10f), Header("UPGRADE SEGMENT COUNTS: ")]
    public int NewRecipeSegments = 5;
    public int ServiceSpeedSegments = 3;
    public int CustomerSpawnSegments = 4;

    [Space(10f), Header("MONEY CONTROL: ")] 
    public float BaseMoneyGain = 5f;

    [Space(10f), Header("RECIPE UPGRADE CONTROL: ")]

    public int MaxDishRecipes = 3;
    public float NewDishPrepTimeIncrease = 2f;
    public float DishCostIncrease = 10f;

    [Space(10f), Header("SERVICE SPEED UPGRADE CONTROL: ")]
    public float PrepTimeReduceAmmount = 0.5f;

    [Space(10f), Header("CUSTOMER SPAWN UPGRADE CONTROL: ")]
    public float SpawnCooldownReduceAmmount = 0.1f;

    [Space(10f), Header("UI CONTROL: ")] 
    public float BaseMoneyUpdateSpeed = 10f;
    public float MoneyUpdateSpeedIncrease = 3f;
    public float ButtonFillSpeed = 5f;
}
