using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerSpawning : MonoBehaviour
{
    [SerializeField] private GameObject customer;
    [SerializeField] private Transform customerSpawnPoint;
    [SerializeField] private Transform spawnParent;
    private float spawnCooldown = 3f;

    private int namingIndex = 0; //debug only

    public static bool EntranceOccupied;

    private void Update()
    {
        if(spawnCooldown <= 0f)
        {
            if(CustomerManager.WaitingCustomers.Count < Settings.CustomerSettings.MaxCustomersInLine && !EntranceOccupied)
            {
                SpawnACustomer();
                spawnCooldown = Settings.CustomerSettings.SpawnCooldown;
            }
        }
        else if(spawnCooldown > 0f)
        {
            spawnCooldown -= Time.deltaTime;
        }
    }

    public void SpawnACustomer() //called by: CustomerBaited + references
    {
        GameObject newCustomer = Instantiate(customer, customerSpawnPoint.position, customerSpawnPoint.rotation, spawnParent); //spawn a new customer at the shop entrance

        newCustomer.name = "Customer" + namingIndex; //name the fresh customer properly
        namingIndex++; //increment the customer number for the next name

        Transform modelsParent = newCustomer.transform.GetChild(0); //get the reference to the character models parent

        int randomIndex = Random.Range(0, modelsParent.childCount); //choose random appearance for the fresh customer from the models parent's children
        modelsParent.GetChild(randomIndex).gameObject.SetActive(true); //show the chosen customer model

        CustomerManager.WaitingCustomers.Add(newCustomer.transform); //add the fresh customer to the list of all customers in line for an order
        //Debug.Log("Awaiting customers: " + CustomerManager.WaitingCustomers.Count);

        EntranceOccupied = true;
    }
}
