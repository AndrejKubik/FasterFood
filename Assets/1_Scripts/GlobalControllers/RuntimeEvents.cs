using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeEvents : MonoBehaviour
{
    [SerializeField] private GameEvent orderAccepted;
    public static GameEvent OrderAccepted;

    [SerializeField] private GameEvent newCustomerAtCounter;
    public static GameEvent NewCustomerAtCounter;

    [SerializeField] private GameEvent customerBaited;
    public static GameEvent CustomerBaited;

    [SerializeField] private GameEvent employeeHired;
    public static GameEvent EmployeeHired;

    [SerializeField] private GameEvent orderFinished;
    public static GameEvent OrderFinished;

    [SerializeField] private GameEvent dishRecipeUpgraded;
    public static GameEvent DishRecipeUpgraded;

    [SerializeField] private GameEvent serviceSpeedUpgraded;
    public static GameEvent ServiceSpeedUpgraded;

    [SerializeField] private GameEvent customerSpawnSpeedUpgraded;
    public static GameEvent CustomerSpawnSpeedUpgraded;

    [SerializeField] private GameEvent recipeChanged;
    public static GameEvent RecipeChanged;

    [Header("MAX UPGRADE EVENTS: ")]
    [SerializeField] private GameEvent maxCustomerSpawnReached;
    public static GameEvent MaxCustomerSpawnReached;

    [SerializeField] private GameEvent maxEmployeesReached;
    public static GameEvent MaxEmployeesReached;

    [SerializeField] private GameEvent maxRecipeReached;
    public static GameEvent MaxRecipeReached;

    [SerializeField] private GameEvent maxServiceSpeedReached;
    public static GameEvent MaxServiceSpeedReached;

    [Header("MAX UPGRADE EVENTS: ")]
    [SerializeField] private GameEvent notEnoughMoney;
    public static GameEvent NotEnoughMoney;

    [SerializeField] private GameEvent upgradeBought;
    public static GameEvent UpgradeBought;

    private void Awake()
    {
        OrderAccepted = orderAccepted;
        NewCustomerAtCounter = newCustomerAtCounter;
        CustomerBaited = customerBaited;
        EmployeeHired = employeeHired;
        OrderFinished = orderFinished;
        DishRecipeUpgraded = dishRecipeUpgraded;
        ServiceSpeedUpgraded = serviceSpeedUpgraded;
        CustomerSpawnSpeedUpgraded = customerSpawnSpeedUpgraded;

        MaxCustomerSpawnReached = maxCustomerSpawnReached;
        MaxEmployeesReached = maxEmployeesReached;
        MaxRecipeReached = maxRecipeReached;
        MaxServiceSpeedReached = maxServiceSpeedReached;
        RecipeChanged = recipeChanged;

        NotEnoughMoney = notEnoughMoney;
        UpgradeBought = upgradeBought;
    }
}
