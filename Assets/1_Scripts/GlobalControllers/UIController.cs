using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    public static float Money;

    [SerializeField] private TextMeshProUGUI moneyCount;
    private float targetMoney;
    private float currentMoneyUI;

    private void Start()
    {
        moneyCount.text = Money.ToString();
    }

    private void Update()
    {
        //if (currentMoneyUI != targetMoney) UpdateMoneyCount();
        UpdateMoneyCount();
    }

    public void BaitCustomer() //called by a button
    {
        SpendMoney(Settings.ShopSettings.BaseCustomerPrice, RuntimeEvents.CustomerBaited);
        Debug.Log("Customer baited!");
    }

    public void HireAnEmployee() //called by a button
    {
        SpendMoney(Settings.ShopSettings.BaseEmployeeCost, RuntimeEvents.EmployeeHired);
        Debug.Log("Employee hired!");
    }

    private void SpendMoney(float price, GameEvent runtimeEvent)
    {
        if (Money >= price)
        {
            runtimeEvent.Raise();

            Money -= price;
            targetMoney = (float)System.Math.Round(Money, 2);
        }
        else Debug.Log("Not enough money!");
    }

    public void EarnMoney() //called by: OrderFinished
    {
        Money += Settings.ShopSettings.BaseMoneyGain;
    }

    private void UpdateMoneyCount()
    {
        //currentMoneyUI = Mathf.MoveTowards(currentMoneyUI, Money, Time.deltaTime * Settings.ShopSettings.MoneyUpdateSpeed);

        currentMoneyUI = Mathf.Round(Mathf.MoveTowards(currentMoneyUI, Money, Time.deltaTime * Settings.ShopSettings.MoneyUpdateSpeed) * 100f) * 0.01f;
        //targetMoney = Mathf.Round(currentMoneyUI * 100f) * 0.01f;
        moneyCount.text = currentMoneyUI.ToString();
    }
}
