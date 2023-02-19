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

    [SerializeField] private GameEvent employeesMerged;
    public static GameEvent EmployeesMerged;

    [SerializeField] private GameEvent orderFinished;
    public static GameEvent OrderFinished;

    [SerializeField] private GameEvent newLevelAppeared;
    public static GameEvent NewLevelAppeared;

    [SerializeField] private GameEvent serviceSpeedUpgraded;
    public static GameEvent ServiceSpeedUpgraded;

    [SerializeField] private GameEvent customerSpawnSpeedUpgraded;
    public static GameEvent CustomerSpawnSpeedUpgraded;

    [SerializeField] private GameEvent maxCustomerSpawnReached;
    public static GameEvent MaxCustomerSpawnReached;

    [SerializeField] private GameEvent maxEmployeesReached;
    public static GameEvent MaxEmployeesReached;

    [SerializeField] private GameEvent maxServiceSpeedReached;
    public static GameEvent MaxServiceSpeedReached;

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
        EmployeesMerged = employeesMerged;
        OrderFinished = orderFinished;
        NewLevelAppeared = newLevelAppeared;
        ServiceSpeedUpgraded = serviceSpeedUpgraded;
        CustomerSpawnSpeedUpgraded = customerSpawnSpeedUpgraded;

        MaxCustomerSpawnReached = maxCustomerSpawnReached;
        MaxEmployeesReached = maxEmployeesReached;
        MaxServiceSpeedReached = maxServiceSpeedReached;

        NotEnoughMoney = notEnoughMoney;
        UpgradeBought = upgradeBought;
    }
}
