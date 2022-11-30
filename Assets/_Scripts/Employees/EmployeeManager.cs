using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeManager : MonoBehaviour
{
    public static List<Employee> Employees = new List<Employee>();

    [SerializeField] private Transform employeesParent;

    public static Transform OrderingCustomer;

    private bool currentCustomerServed;

    private void Start()
    {
        for(int i = 0; i < employeesParent.childCount; i++)
        {
            Employees.Add(employeesParent.GetChild(i).GetComponent<Employee>());
        }
    }

    public void ServeCustomer() //called by: NewCustomerAtCounter
    {
        if(CustomerManager.WaitingCustomers.Count > 0)
        {
            if (OrderingCustomer == null && CustomerManager.WaitingCustomers[0].GetComponent<CustomerMovement>().counterReached) OrderingCustomer = CustomerManager.WaitingCustomers[0];

            for (int i = 0; i < Employees.Count; i++) //check all employees' statuses
            {
                if (Employees[i].ServedCustomer == null && !currentCustomerServed) //when the loop caches an employee that is empty-handed
                {
                    currentCustomerServed = true;

                    Employees[i].ServedCustomer = OrderingCustomer; //assign the current customer to the current employee

                    RuntimeEvents.OrderAccepted.Raise();
                }
            }

            OrderingCustomer = null; //clear out the slot for the next customer
            currentCustomerServed = false; //let the next customer be assigned to an employee
        }
    }

}
