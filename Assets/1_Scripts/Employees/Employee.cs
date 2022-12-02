using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Employee : MonoBehaviour
{
    [HideInInspector] public Transform ServedCustomer;
    [SerializeField] private float workTime = 2f;

    private bool customerBeingServed;
    public Transform CustomerWaitPosition;

    private void Update()
    {
        if (customerBeingServed && ServedCustomer != null) ServedCustomer.position = Vector3.Lerp(ServedCustomer.position, CustomerWaitPosition.position, 15f * Time.deltaTime);
    }

    public void PrepareOrder() //called by: OrderAccepted
    {
        if(!customerBeingServed && ServedCustomer != null) StartCoroutine(OrderPrep(workTime)); //start preparing the order for the assigned customer
    }

    IEnumerator OrderPrep(float delay)
    {
        if(CustomerManager.WaitingCustomers.Count > 0)
        {
            CustomerManager.WaitingCustomers.RemoveAt(0);
            Debug.Log("Customers left: " + CustomerManager.WaitingCustomers.Count);

            Debug.Log(name + " is preparing an order from " + ServedCustomer.name);

            customerBeingServed = true;

            yield return new WaitForSeconds(delay);

            if (ServedCustomer != null) Destroy(ServedCustomer.gameObject); //remove the completely served customer from the game
            ServedCustomer = null;

            customerBeingServed = false;

            Debug.Log("OrderFinished");

            RuntimeEvents.NewCustomerAtCounter.Raise();
            RuntimeEvents.OrderFinished.Raise();
        }
    }
}
