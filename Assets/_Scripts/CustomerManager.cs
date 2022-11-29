using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerManager : MonoBehaviour
{
    public static List<Transform> WaitingCustomers = new List<Transform>();

    [SerializeField] private Transform customersParent;

    private void Start()
    {
        for(int i = 0; i < customersParent.childCount; i++)
        {
            WaitingCustomers.Add(customersParent.GetChild(i));
        }
    }
}
