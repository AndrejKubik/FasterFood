using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeManager : MonoBehaviour
{
    public static List<Employee> ActiveEmployees = new List<Employee>();
    [SerializeField] private Transform employeesParent;
    [SerializeField] private List<GameObject> allEmployees;

    public static Transform OrderingCustomer;

    private bool currentCustomerServed;

    private void Start()
    {
        SetExistingEmployeesAsActive(); //set all active worker objects on the scene as active employees so they can serve customers
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            HireAnEmployee();
        }
    }

    public void ServeCustomer() //called by: NewCustomerAtCounter
    {
        if(CustomerManager.WaitingCustomers.Count > 0)
        {
            if (OrderingCustomer == null && CustomerManager.WaitingCustomers[0].GetComponent<CustomerMovement>().counterReached) OrderingCustomer = CustomerManager.WaitingCustomers[0];

            for (int i = 0; i < ActiveEmployees.Count; i++) //check all employees' statuses
            {
                if (ActiveEmployees[i].ServedCustomer == null && !currentCustomerServed) //when the loop caches an employee that is empty-handed
                {
                    currentCustomerServed = true;

                    ActiveEmployees[i].ServedCustomer = OrderingCustomer; //assign the current customer to the current employee

                    RuntimeEvents.OrderAccepted.Raise();
                }
            }

            OrderingCustomer = null; //clear out the slot for the next customer
            currentCustomerServed = false; //let the next customer be assigned to an employee
        }
    }

    private void SetExistingEmployeesAsActive()
    {
        for (int i = 0; i < employeesParent.childCount; i++)
        {
            Employee employee = employeesParent.GetChild(i).GetComponent<Employee>();

            if(employee.gameObject.activeSelf) ActiveEmployees.Add(employee);
        }
    }

    private void HireAnEmployee()
    {
        allEmployees[ActiveEmployees.Count].SetActive(true); //show the next worker object on the scene
        ActiveEmployees.Add(allEmployees[ActiveEmployees.Count].GetComponent<Employee>()); //set the newly shown employee as active so he can receive and prepare orders
    }

}
