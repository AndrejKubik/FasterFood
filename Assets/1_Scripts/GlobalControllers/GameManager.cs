using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static float MoneyTotal;
    public static float MoneyGain;
    public static float MoneyTextUpdateSpeed;

    public static float DishRecipeMultiplier;
    public static int CurrentDishRecipe;

    public static float OrderPrepTime;
    public static float OrderPrepTimeReduceAmmount;

    public static float CustomerSpawnCooldown;

    public static int CurrentRecipeSegments;
    public static int CurrentServiceSpeedSegments;
    public static int CurrentCustomerSpawnSegments;

    public static float CurrentEmployeePrice;
    public static float CurrentRecipePrice;
    public static float CurrentServiceSpeedPrice;
    public static float CurrentCustomerSpawnPrice;

    [SerializeField] private Animator kitchenAnimator;

    private void Start()
    {
        MoneyGain = Settings.ShopSettings.BaseMoneyGain;
        DishRecipeMultiplier = 1f;
        CurrentDishRecipe = 0;

        MoneyTextUpdateSpeed = Settings.ShopSettings.BaseMoneyUpdateSpeed;

        OrderPrepTime = Settings.EmployeeSettings.BaseOrderPrepTime;
        OrderPrepTimeReduceAmmount = Settings.ShopSettings.PrepTimeReduceAmmount;

        CustomerSpawnCooldown = Settings.CustomerSettings.BaseSpawnCooldown;

        CurrentEmployeePrice = Settings.ShopSettings.BaseEmployeeCost;
        CurrentRecipePrice = Settings.ShopSettings.BaseNewRecipeCost;
        CurrentServiceSpeedPrice = Settings.ShopSettings.BaseServiceSpeedCost;
        CurrentCustomerSpawnPrice = Settings.ShopSettings.BaseCustomerSpawnSpeedCost;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) MoneyTotal += 50;
    }

    public void EarnMoney() //called by: OrderFinished
    {
        MoneyTotal += MoneyGain * DishRecipeMultiplier;
    }

    public void UpdateEmployeePrice() //called by: EmployeeHired
    {
        IncreasePrice(ref CurrentEmployeePrice, Settings.ShopSettings.EmployeePriceMultiplier);
    }

    public void UpgradePrepTime() //called by: ServiceSpeedUpgraded
    {
        BuyUpgrade("ReduceOrderPrepTime", ref CurrentServiceSpeedSegments, Settings.ShopSettings.ServiceSpeedSegments);
        IncreasePrice(ref CurrentServiceSpeedPrice, Settings.ShopSettings.ServiceSpeedPriceMultiplier);
    }

    public void UpgradeCustomerSpawnSpeed() //called by: CustomerSpawnSpeedUpgraded
    {
        BuyUpgrade("ReduceCustomerSpawnCooldown", ref CurrentCustomerSpawnSegments, Settings.ShopSettings.CustomerSpawnSegments);
        IncreasePrice(ref CurrentCustomerSpawnPrice, Settings.ShopSettings.CustomerSpawnPriceMultiplier);
    }

    public void UpgradeDishRecipe() //called by: DishRecipeUpgraded
    {
        BuyUpgrade("ChangeDishRecipe", ref CurrentRecipeSegments, Settings.ShopSettings.NewRecipeSegments);
        IncreasePrice(ref CurrentRecipePrice, Settings.ShopSettings.RecipePriceMultiplier);
    }

    private void ReduceOrderPrepTime() //called by: UpgradePrepTime()
    {
        OrderPrepTime -= OrderPrepTimeReduceAmmount; //reduce the time required to prepare an order
        Debug.Log("Prep time reduced!");

        float nextPrepTime = OrderPrepTime - OrderPrepTimeReduceAmmount; //check the value of the next upgrade

        if (nextPrepTime < Settings.EmployeeSettings.MinOrderPrepTime) //if the next upgrade would pass the upgrade limit
        {
            RuntimeEvents.MaxServiceSpeedReached.Raise(); //show the button blocker
        }
    }

    private void ReduceCustomerSpawnCooldown() //called by: UpgradeCustomerSpawnSpeed()
    {
        CustomerSpawnCooldown -= Settings.ShopSettings.SpawnCooldownReduceAmmount; //reduce the time required to wait for next customer auto-spawn
        Debug.Log("Customer spawn cooldown reduced!");

        float nextCooldown = CustomerSpawnCooldown - Settings.ShopSettings.SpawnCooldownReduceAmmount; //check the value of the next upgrade

        if(nextCooldown < Settings.CustomerSettings.MinSpawnCooldown) //if the next upgrade is not within the limit
        {
            RuntimeEvents.MaxCustomerSpawnReached.Raise(); //block further upgrades
        }
    }

    private void ChangeDishRecipe() //called by: UpgradeDishRecipe()
    {
        MoneyTextUpdateSpeed += Settings.ShopSettings.MoneyUpdateSpeedIncrease; //increase the money number update speed
        DishRecipeMultiplier += Settings.ShopSettings.DishCostIncrease; //increase the money gain

        OrderPrepTime += Settings.ShopSettings.NewDishPrepTimeIncrease; //increase the dish prep time due to more "complex" meal

        CurrentDishRecipe++; //increment the current dish recipe index

        if (CurrentDishRecipe == 1) kitchenAnimator.Play("ChangeToHotdog");
        else if (CurrentDishRecipe == 2) kitchenAnimator.Play("ChangeToPizza");

        Debug.Log("New dish is being served");

        //change the appearance of the dish being served

        int nextRecipe = CurrentDishRecipe + 1; //check what is the next dish recipe

        if (nextRecipe > Settings.ShopSettings.MaxDishRecipes) //if there is no more recipes to buy
        {
            RuntimeEvents.MaxRecipeReached.Raise(); //show button blocker
        }

        RuntimeEvents.RecipeChanged.Raise(); //allow the player to further reduce prep time
    }

    private void BuyUpgrade(string method, ref int boughtSegments, int numberOfSegments)
    {
        if (boughtSegments < numberOfSegments)
        {
            boughtSegments++;
        }
        else if (boughtSegments >= numberOfSegments)
        {
            Invoke(method, 0f);
            boughtSegments = 0;
        }
    }

    private void IncreasePrice(ref float price, float priceIncrease)
    {
        float rawPrice = price * priceIncrease;
        price = Mathf.RoundToInt(rawPrice);
    }
}
