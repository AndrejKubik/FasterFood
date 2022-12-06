using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Employee : MonoBehaviour
{
    [HideInInspector] public Transform ServedCustomer;
    [SerializeField] private float workTime = 2f;

    private bool customerBeingServed;
    public Transform CustomerWaitPosition;

    [SerializeField] private GameObject progressBar;
    [SerializeField] private Image progressBarFill;

    private void Update()
    {
        if (customerBeingServed && ServedCustomer != null)
        {
            ServedCustomer.position = Vector3.Lerp(ServedCustomer.position, CustomerWaitPosition.position, Settings.CustomerSettings.CustomerMovementSpeed * Time.deltaTime);

            progressBarFill.fillAmount = Mathf.MoveTowards(progressBarFill.fillAmount, 1f, Time.deltaTime * 1f / workTime);
        }
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
            //Debug.Log("Customers left: " + CustomerManager.WaitingCustomers.Count);

            //Debug.Log(name + " is preparing an order from " + ServedCustomer.name);

            progressBar.SetActive(true); //show the preparation progress bar above the employee
            customerBeingServed = true;

            yield return new WaitForSeconds(delay);

            if (ServedCustomer != null) Destroy(ServedCustomer.gameObject); //remove the completely served customer from the game
            ServedCustomer = null;

            customerBeingServed = false;

            progressBarFill.fillAmount = 0f; //reset the employee's progress bar
            progressBar.SetActive(false); //hide the preparation progress bar above the employee

            //Debug.Log("OrderFinished");

            RuntimeEvents.NewCustomerAtCounter.Raise();
            RuntimeEvents.OrderFinished.Raise();
        }
    }
}
