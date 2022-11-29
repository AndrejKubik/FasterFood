using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeManager : MonoBehaviour
{
    public static List<Employee> Employees = new List<Employee>();

    [SerializeField] private Transform employeesParent;

    public static Transform OrderingCustomer;

    private void Start()
    {
        for(int i = 0; i < employeesParent.childCount; i++)
        {
            Employees.Add(employeesParent.GetChild(i).GetComponent<Employee>());
        }
    }

    public void ServeCustomer() //called in the event listener
    {
        for(int i = 0; i < Employees.Count; i++) //check all employees' statuses
        {
            if (Employees[i].ServedCustomer == null) //when the loop caches an employee that is empty-handed
            {
                Employees[i].ServedCustomer = OrderingCustomer; //assign the current customer to the current employee
                OrderingCustomer = null; //clear out the slot for the next customer

                RuntimeEvents.OrderAccepted.Raise();
            }
        }
    }

}
