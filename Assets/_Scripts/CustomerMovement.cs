using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    private bool counterReached;

    private bool waitingInLine;
    private GameObject customerInFront;

    private void Update()
    {
        if(!counterReached && !waitingInLine) transform.Translate(Vector3.forward * 10f * Time.deltaTime); //move the customer toward the counter

        if(customerInFront == null) waitingInLine = false; //if there is no customers in front, stop waiting in line and keep moving
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Counter")) //when the customer reaches the counter
        {
            counterReached = true; //stop moving
            EmployeeManager.OrderingCustomer = transform; //cache this customer as the one currently being served

            RuntimeEvents.NewCustomerAtCounter.Raise();
        }
        else if(other.CompareTag("Customer")) //when a customer touches another customer in line
        {
            if(customerInFront == null) //if the customer hasn't touched another already
            {
                waitingInLine = true; //start waiting in line for the customer in front 
                customerInFront = other.gameObject; //cache the touched customer
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Customer"))
        {
            customerInFront = null; //clear out the front customer slot so this one can keep moving
        }
    }
}
