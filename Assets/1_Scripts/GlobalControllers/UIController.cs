using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    private float currentMoneyUI;
    [SerializeField] private TextMeshProUGUI moneyCount;
    [Space(10f), SerializeField] private Animator currentMoneyAnimator;

    [Header("DEBUG MENU OBJECTS: ")]
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject pauseButton;

    [Header("UPGRADE PROGRESS BARS: ")]
    [SerializeField] private Image customersUpgradeFill;
    [SerializeField] private Image prepSpeedUpgradeFill;

    [Header("UPGRADE COST TEXTS: ")]
    [SerializeField] private TextMeshProUGUI employeeCost;
    [SerializeField] private TextMeshProUGUI serviceSpeedCost;
    [SerializeField] private TextMeshProUGUI customerSpawnCost;

    [Header("UPGRADE BUTTON ANIMATORS: ")]
    [SerializeField] private Animator employeeButtonAnimator;
    [SerializeField] private Animator serviceSpeedButtonAnimator;
    [SerializeField] private Animator customerSpawnButtonAnimator;

    [Header("UPGRADE BUTTON BLOCKERS: ")]
    [SerializeField] private GameObject employeeButtonBlock;
    [SerializeField] private GameObject serviceSpeedButtonBlock;
    [SerializeField] private GameObject customerSpawnButtonBlock;

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

    public void PauseGame() //called by menu button
    {
        pauseMenu.SetActive(true);
        pauseButton.SetActive(false);
        Time.timeScale = 0f;
    }

    public void ResumeGame() //called by menu button
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        pauseButton.SetActive(true);
    }

    public void BaitCustomer() //called by a button
    {
        if (CustomerManager.WaitingCustomers.Count < Settings.CustomerSettings.MaxCustomersInLine)
        {
            if (!CustomerSpawning.EntranceOccupied)
            {
                SpendMoney(Settings.ShopSettings.BaseCustomerPrice, RuntimeEvents.CustomerBaited);
                RuntimeEvents.UpgradeBought.Raise(); //play the button sound
            }
            //else if (CustomerSpawning.EntranceOccupied) Debug.Log("Wait up!");
        } 
        else Debug.Log("Max Customers in line!");
    }

    public void HireAnEmployee() //called by a button
    {
        if (GameManager.MoneyTotal >= GameManager.CurrentEmployeePrice)
        {
            RuntimeEvents.EmployeeHired.Raise();
            GameManager.MoneyTotal -= GameManager.CurrentEmployeePrice;
        }
        else Debug.Log("Not enough money!");

        UpdateCostText(employeeCost, GameManager.CurrentEmployeePrice);
        PlayButtonAnimation(employeeButtonAnimator);
    }

    public void UpgradeServiceSpeed() //called by a button
    {
        SpendMoney(GameManager.CurrentServiceSpeedPrice, RuntimeEvents.ServiceSpeedUpgraded); //if the another level is available for this upgrade, buy it
        UpdateCostText(serviceSpeedCost, GameManager.CurrentServiceSpeedPrice);
        PlayButtonAnimation(serviceSpeedButtonAnimator);
    }

    public void UpgradeCustomerSpawnSpeed() //called by a button
    {
        SpendMoney(GameManager.CurrentCustomerSpawnPrice, RuntimeEvents.CustomerSpawnSpeedUpgraded); //if the another level is available for this upgrade, buy it
        UpdateCostText(customerSpawnCost, GameManager.CurrentCustomerSpawnPrice);
        PlayButtonAnimation(customerSpawnButtonAnimator);
    }

    public void AnimateMoneyImage() //called by: OrderFinished
    {
        currentMoneyAnimator.Play("MoneyEarned", 0, 0f);
    }

    public void BlockServiceSpeedUpgrade() //called by: MaxServiceSpeedReached
    {
        serviceSpeedButtonBlock.SetActive(true); //cover the service speed upgrade button
    }

    public void BlockCustomerSpawnUpgrade() //called by: MaxCustomerSpawnReached
    {
        customerSpawnButtonBlock.SetActive(true);
    }

    public void BlockEmployeeHire() //called by: MaxEmployeesReached
    {
        employeeButtonBlock.SetActive(true); //cover the employee button since there are no more slots
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
        UpdateCostText(serviceSpeedCost, GameManager.CurrentServiceSpeedPrice);
        UpdateCostText(customerSpawnCost, GameManager.CurrentCustomerSpawnPrice);
    }

    private void UpdateCostText(TextMeshProUGUI price, float newPrice)
    {
        price.text = newPrice.ToString();
    }

    private void PlayButtonAnimation(Animator animator)
    {
        animator.Play("ButtonPress"); //blob the button
        RuntimeEvents.UpgradeBought.Raise(); //play the button sound
    }

    private void UpdateButtonFills()
    {
        customersUpgradeFill.fillAmount = Mathf.MoveTowards(customersUpgradeFill.fillAmount, (float)GameManager.CurrentCustomerSpawnSegments / (Settings.ShopSettings.CustomerSpawnSegments + 1), Time.deltaTime * Settings.ShopSettings.ButtonFillSpeed);
        prepSpeedUpgradeFill.fillAmount = Mathf.MoveTowards(prepSpeedUpgradeFill.fillAmount, (float)GameManager.CurrentServiceSpeedSegments / (Settings.ShopSettings.ServiceSpeedSegments + 1), Time.deltaTime * Settings.ShopSettings.ButtonFillSpeed);
    }
}
