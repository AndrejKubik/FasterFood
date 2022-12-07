using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterAppearance
{
    public static void ShowRandomModel(Transform parent)
    {
        int randomIndex = Random.Range(0, parent.childCount); //choose a random chlid object
        parent.GetChild(randomIndex).gameObject.SetActive(true); //show the chosen child model object
    }
}
