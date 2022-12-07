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
    }
}
