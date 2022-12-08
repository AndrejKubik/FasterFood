using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private float currentMoneyUI;
    [SerializeField] private TextMeshProUGUI moneyCount;

    [Header("UPGRADE PROGRESS BARS: ")]
    [SerializeField] private Image customersUpgradeFill;
    [SerializeField] private Image dishUpgradeFill;
    [SerializeField] private Image prepSpeedUpgradeFill;

    [Header("UPGRADE COST TEXTS: ")]
    [SerializeField] private TextMeshProUGUI employeeCost;
    [SerializeField] private TextMeshProUGUI newRecipeCost;
    [SerializeField] private TextMeshProUGUI serviceSpeedCost;
    [SerializeField] private TextMeshProUGUI customerSpawnCost;

    [Header("UPGRADE BUTTON ANIMATORS: ")]
    [SerializeField] private Animator employeeButtonAnimator;
    [SerializeField] private Animator newRecipeButtonAnimator;
    [SerializeField] private Animator serviceSpeedButtonAnimator;
    [SerializeField] private Animator customerSpawnButtonAnimator;

    [Header("UPGRADE BUTTON BLOCKERS: ")]
    [SerializeField] private GameObject employeeButtonBlock;
    [SerializeField] private GameObject newRecipeButtonBlock;
    [SerializeField] private GameObject serviceSpeedButtonBlock;
    [SerializeField] private GameObject customerSpawnButtonBlock;

    public static GameObject EmployeeButtonBlock;
    public static GameObject NewRecipeButtonBlock;
    public static GameObject ServiceSpeedButtonBlock;
    public static GameObject CustomerSpawnButtonBlock;

    [Space(10f), SerializeField] private Animator currentMoneyAnimator;

    private void Awake()
    {
        EmployeeButtonBlock = employeeButtonBlock;
        NewRecipeButtonBlock = newRecipeButtonBlock;
        ServiceSpeedButtonBlock = serviceSpeedButtonBlock;
        CustomerSpawnButtonBlock = customerSpawnButtonBlock;
    }

    private void Start()
    {
        moneyCount.text = GameManager.MoneyTotal.ToString();

        UpdateAllCosts();
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
        if(!EmployeeManager.MaxEmployees)
        {
            SpendMoney(GameManager.CurrentEmployeePrice, RuntimeEvents.EmployeeHired); //if the another level is available for this upgrade, buy it
            UpdateCostText(employeeCost, GameManager.CurrentEmployeePrice);
            PlayButtonAnimation(employeeButtonAnimator);
        }
    }

    public void UpgradeDishRecipe() //called by a button
    {
        if (GameManager.CurrentDishRecipe < Settings.ShopSettings.MaxDishRecipes)
        {
            SpendMoney(GameManager.CurrentRecipePrice, RuntimeEvents.DishRecipeUpgraded); //if the another level is available for this upgrade, buy it
            UpdateCostText(newRecipeCost, GameManager.CurrentRecipePrice);
            PlayButtonAnimation(newRecipeButtonAnimator);
            serviceSpeedButtonBlock.SetActive(false);
        }
    }

    public void UpgradeServiceSpeed() //called by a button
    {
        if (GameManager.OrderPrepTime > Settings.EmployeeSettings.MinOrderPrepTime)
        {
            SpendMoney(GameManager.CurrentServiceSpeedPrice, RuntimeEvents.ServiceSpeedUpgraded); //if the another level is available for this upgrade, buy it
            UpdateCostText(serviceSpeedCost, GameManager.CurrentServiceSpeedPrice);
            PlayButtonAnimation(serviceSpeedButtonAnimator);
        }
    }

    public void UpgradeCustomerSpawnSpeed() //called by a button
    {
        if (GameManager.CustomerSpawnCooldown > Settings.CustomerSettings.MinSpawnCooldown)
        {
            SpendMoney(GameManager.CurrentCustomerSpawnPrice, RuntimeEvents.CustomerSpawnSpeedUpgraded); //if the another level is available for this upgrade, buy it
            UpdateCostText(customerSpawnCost, GameManager.CurrentCustomerSpawnPrice);
            PlayButtonAnimation(customerSpawnButtonAnimator);
        }
    }

    public void AnimateMoneyImage() //called by: OrderFinished
    {
        currentMoneyAnimator.Play("MoneyEarned", 0, 0f);
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

    private void UpdateAllCosts()
    {
        UpdateCostText(employeeCost, GameManager.CurrentEmployeePrice);
        UpdateCostText(newRecipeCost, GameManager.CurrentRecipePrice);
        UpdateCostText(serviceSpeedCost, GameManager.CurrentServiceSpeedPrice);
        UpdateCostText(customerSpawnCost, GameManager.CurrentCustomerSpawnPrice);
    }

    private void UpdateCostText(TextMeshProUGUI price, float newPrice)
    {
        price.text = newPrice.ToString();
    }

    private void PlayButtonAnimation(Animator animator)
    {
        animator.Play("ButtonPress");
    }

    private void UpdateButtonFills()
    {
        customersUpgradeFill.fillAmount = Mathf.MoveTowards(customersUpgradeFill.fillAmount, (float)GameManager.CurrentCustomerSpawnSegments / (Settings.ShopSettings.CustomerSpawnSegments + 1), Time.deltaTime * Settings.ShopSettings.ButtonFillSpeed);
        dishUpgradeFill.fillAmount = Mathf.MoveTowards(dishUpgradeFill.fillAmount, (float)GameManager.CurrentRecipeSegments / (Settings.ShopSettings.NewRecipeSegments + 1), Time.deltaTime * Settings.ShopSettings.ButtonFillSpeed);
        prepSpeedUpgradeFill.fillAmount = Mathf.MoveTowards(prepSpeedUpgradeFill.fillAmount, (float)GameManager.CurrentServiceSpeedSegments / (Settings.ShopSettings.ServiceSpeedSegments + 1), Time.deltaTime * Settings.ShopSettings.ButtonFillSpeed);
    }
}
