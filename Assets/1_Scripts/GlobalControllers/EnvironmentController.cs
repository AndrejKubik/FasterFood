using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentController : MonoBehaviour
{
    public List<Animator> Kitchens;
    public static int KitchenLevel = 0;

    private void Start()
    {
        Kitchens[KitchenLevel].Play("KitchenAppear", 0, 0f);
    }

    public void UpgradeKitchen() //called by: NewLevelAppeared
    {
        Kitchens[KitchenLevel].Play("KitchenDisappear", 0, 0f);
        KitchenLevel++;
        Kitchens[KitchenLevel].Play("KitchenAppear", 0, 0f);
    }
}
