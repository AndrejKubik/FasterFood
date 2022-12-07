using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAppearance : MonoBehaviour
{
    [Header("MODEL PARENTS: ")]
    [SerializeField] private Transform hats;
    [SerializeField] private Transform heads;
    [SerializeField] private Transform bodies;

    private void OnEnable()
    {
        CharacterAppearance.ShowRandomModel(hats);
        CharacterAppearance.ShowRandomModel(heads);
        CharacterAppearance.ShowRandomModel(bodies);
    }

    
}
