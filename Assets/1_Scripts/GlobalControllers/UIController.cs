using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static float Money;

    [SerializeField] private TextMeshProUGUI moneyCount;
    private float currentMoneyUI;

    private void Start()
    {
        moneyCount.text = Money.ToString();
    }

    private void Update()
    {
        UpdateMoneyCount();
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
        if(!EmployeeManager.MaxEmployees) SpendMoney(Settings.ShopSettings.BaseEmployeeCost, RuntimeEvents.EmployeeHired);
        else if(EmployeeManager.MaxEmployees) Debug.Log("Max employees reached!");
    }

    private void SpendMoney(float price, GameEvent runtimeEvent)
    {
        if (Money >= price)
        {
            runtimeEvent.Raise();

            Money -= price;
        }
        else Debug.Log("Not enough money!");
    }

    public void EarnMoney() //called by: OrderFinished
    {
        Money += Settings.ShopSettings.BaseMoneyGain;
    }

    private void UpdateMoneyCount()
    {
        currentMoneyUI = Mathf.Round(Mathf.MoveTowards(currentMoneyUI, Money, Time.deltaTime * Settings.ShopSettings.MoneyUpdateSpeed) * 100f) * 0.01f;
        moneyCount.text = Mathf.RoundToInt(currentMoneyUI).ToString();
    }
}
