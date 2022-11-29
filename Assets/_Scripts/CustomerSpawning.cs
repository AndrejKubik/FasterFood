using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawning : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    [SerializeField] private Transform customerSpawnPoint;
    [SerializeField] private Transform spawnParent;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newCustomer = Instantiate(customer, customerSpawnPoint.position, customerSpawnPoint.rotation, spawnParent);
            CustomerManager.WaitingCustomers.Add(newCustomer.transform);
            Debug.Log("Awaiting customers: " + CustomerManager.WaitingCustomers.Count);
        }
    }
}
