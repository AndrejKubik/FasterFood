using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private float currentMoneyUI;
    [SerializeField] private TextMeshProUGUI moneyCount;

    [Space(10f), SerializeField] private Image customersUpgradeFill;
    [SerializeField] private Image dishUpgradeFill;
    [SerializeField] private Image prepSpeedUpgradeFill;

    private void Start()
    {
        moneyCount.text = GameManager.MoneyTotal.ToString();
    }

    private void Update()
    {
        UpdateMoneyCount();
        UpdateButtonFills();
    }

    public void BaitCustomer() //called by a button
    {
        if (CustomerManager.WaitingCustomers.Count < Settings.CustomerSettings.MaxCustomersInLine)
        {
            if (!CustomerSpawning.EntranceOccupied) SpendMoney(Settings.ShopSettings.BaseCustomerPrice, RuntimeEvents.CustomerBaited);
            else if (CustomerSpawning.EntranceOccupied) Debug.Log("Wait up!");
        } 
        else Debug.Log("Max Customers in line!");
    }

    public void HireAnEmployee() //called by a button
    {
        if(!EmployeeManager.MaxEmployees) SpendMoney(Settings.ShopSettings.BaseEmployeeCost, RuntimeEvents.EmployeeHired); //if the another level is available for this upgrade, buy it
        else if(EmployeeManager.MaxEmployees) Debug.Log("Max employees reached!"); //otherwise do nothing
    }

    public void UpgradeDishRecipe() //called by a button
    {
        if (GameManager.CurrentDishRecipe < Settings.ShopSettings.MaxDishRecipes) SpendMoney(Settings.ShopSettings.BaseNewRecipeCost, RuntimeEvents.DishRecipeUpgraded); //if the another level is available for this upgrade, buy it
        else if(GameManager.CurrentDishRecipe >= Settings.ShopSettings.MaxDishRecipes) Debug.Log("Max recipes reached!"); //otherwise do nothing
    }

    public void UpgradeServiceSpeed() //called by a button
    {
        if (GameManager.OrderPrepTime > Settings.EmployeeSettings.MinOrderPrepTime) SpendMoney(Settings.ShopSettings.BaseServiceSpeedCost, RuntimeEvents.ServiceSpeedUpgraded); //if the another level is available for this upgrade, buy it
        else if (GameManager.OrderPrepTime <= Settings.EmployeeSettings.MinOrderPrepTime) Debug.Log("Maximum prep speed achieved!"); //otherwise do nothing
    }

    public void UpgradeCustomerSpawnSpeed() //called by a button
    {
        if (GameManager.CustomerSpawnCooldown > Settings.CustomerSettings.MinSpawnCooldown) SpendMoney(Settings.ShopSettings.BaseCustomerSpawnSpeedCost, RuntimeEvents.CustomerSpawnSpeedUpgraded); //if the another level is available for this upgrade, buy it
        else if (GameManager.CustomerSpawnCooldown <= Settings.CustomerSettings.MinSpawnCooldown) Debug.Log("Minimum spawn cooldown reached!"); //otherwise do nothing
    }

    private void SpendMoney(float price, GameEvent runtimeEvent)
    {
        if (GameManager.MoneyTotal >= price)
        {
            runtimeEvent.Raise();

            GameManager.MoneyTotal -= price;
        }
        else Debug.Log("Not enough money!");
    }

    private void UpdateMoneyCount()
    {
        currentMoneyUI = Mathf.Round(Mathf.MoveTowards(currentMoneyUI, GameManager.MoneyTotal, Time.deltaTime * GameManager.MoneyTextUpdateSpeed) * 100f) * 0.01f;
        moneyCount.text = Mathf.RoundToInt(currentMoneyUI).ToString();
    }

    private void UpdateButtonFills()
    {
        customersUpgradeFill.fillAmount = Mathf.MoveTowards(customersUpgradeFill.fillAmount, (float)GameManager.CurrentCustomerSpawnSegments / (Settings.ShopSettings.CustomerSpawnSegments + 1), Time.deltaTime * Settings.ShopSettings.ButtonFillSpeed);
        dishUpgradeFill.fillAmount = Mathf.MoveTowards(dishUpgradeFill.fillAmount, (float)GameManager.CurrentRecipeSegments / (Settings.ShopSettings.NewRecipeSegments + 1), Time.deltaTime * Settings.ShopSettings.ButtonFillSpeed);
        prepSpeedUpgradeFill.fillAmount = Mathf.MoveTowards(prepSpeedUpgradeFill.fillAmount, (float)GameManager.CurrentServiceSpeedSegments / (Settings.ShopSettings.ServiceSpeedSegments + 1), Time.deltaTime * Settings.ShopSettings.ButtonFillSpeed);
    }
}
