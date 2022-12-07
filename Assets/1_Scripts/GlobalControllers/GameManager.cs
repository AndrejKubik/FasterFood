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

    private void Start()
    {
        MoneyGain = Settings.ShopSettings.BaseMoneyGain;
        DishRecipeMultiplier = 1f;
        CurrentDishRecipe = 0;

        MoneyTextUpdateSpeed = Settings.ShopSettings.BaseMoneyUpdateSpeed;

        OrderPrepTime = Settings.EmployeeSettings.BaseOrderPrepTime;
        OrderPrepTimeReduceAmmount = Settings.ShopSettings.PrepTimeReduceAmmount;

        CustomerSpawnCooldown = Settings.CustomerSettings.BaseSpawnCooldown;
    }

    public void EarnMoney() //called by: OrderFinished
    {
        MoneyTotal += MoneyGain * DishRecipeMultiplier;
    }

    public void UpgradePrepTime() //called by: ServiceSpeedUpgraded
    {
        BuyUpgrade("ReduceOrderPrepTime", ref CurrentServiceSpeedSegments, Settings.ShopSettings.ServiceSpeedSegments);
    }

    public void UpgradeCustomerSpawnSpeed() //called by: CustomerSpawnSpeedUpgraded
    {
        BuyUpgrade("ReduceCustomerSpawnCooldown", ref CurrentCustomerSpawnSegments, Settings.ShopSettings.CustomerSpawnSegments);
    }

    public void UpgradeDishRecipe() //called by: DishRecipeUpgraded
    {
        BuyUpgrade("ChangeDishRecipe", ref CurrentRecipeSegments, Settings.ShopSettings.NewRecipeSegments);
    }

    private void ReduceOrderPrepTime() //called by: UpgradePrepTime()
    {
        OrderPrepTime -= OrderPrepTimeReduceAmmount;
        Debug.Log("Prep time reduced!");
    }

    private void ReduceCustomerSpawnCooldown() //called by: UpgradeCustomerSpawnSpeed()
    {
        CustomerSpawnCooldown -= Settings.ShopSettings.SpawnCooldownReduceAmmount;
        Debug.Log("Customer spawn cooldown reduced!");
    }

    private void ChangeDishRecipe() //called by: UpgradeDishRecipe()
    {
        MoneyTextUpdateSpeed += Settings.ShopSettings.MoneyUpdateSpeedIncrease; //increase the money number update speed
        DishRecipeMultiplier += Settings.ShopSettings.DishCostIncrease; //increase the money gain

        OrderPrepTime += Settings.ShopSettings.NewDishPrepTimeIncrease; //increase the dish prep time due to more "complex" meal

        CurrentDishRecipe++; //increment the current dish recipe index

        Debug.Log("New dish is being served");

        //change the appearance of the dish being served
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
}
