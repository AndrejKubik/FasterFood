using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawning : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    [SerializeField] private Transform customerSpawnPoint;
    [SerializeField] private Transform spawnParent;

    int i = 0; //debug only

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject newCustomer = Instantiate(customer, customerSpawnPoint.position, customerSpawnPoint.rotation, spawnParent); //spawn a new customer at the shop entrance

            //debug only
            newCustomer.name = "Customer" + i;
            i++;

            CustomerManager.WaitingCustomers.Add(newCustomer.transform); //add the fresh customer to the list of all customers in line for an order
            Debug.Log("Awaiting customers: " + CustomerManager.WaitingCustomers.Count);
        }
    }
}
