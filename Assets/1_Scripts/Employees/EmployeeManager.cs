using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeManager : MonoBehaviour
{
    [SerializeField] private Transform employeesParent;
    [SerializeField] private List<Transform> spawnPositions = new List<Transform>();
    public static List<SpawnPoint> SpawnPoints = new List<SpawnPoint>();
    public static List<Employee> ActiveEmployees = new List<Employee>();

    public static Transform OrderingCustomer;
    private bool currentCustomerServed;

    [SerializeField] private GameObject employeePrefab;

    public class SpawnPoint
    {
        public Vector3 Position;
        public Quaternion Rotation;
        public bool IsOccupied;

        public SpawnPoint(Vector3 position, Quaternion rotation, bool isOccupied)
        {
            Position = position;
            IsOccupied = isOccupied;
            Rotation = rotation;
        }
    }

    private void Start()
    {
        LoadAllSpawnPoints(); //set all active worker objects on the scene as active employees so they can serve customers
        HireAnEmployee();
    }

    public void ServeCustomer() //called by: NewCustomerAtCounter
    {
        if(CustomerManager.WaitingCustomers.Count > 0)
        {
            CustomerMovement firstInLine = CustomerManager.WaitingCustomers[0].GetComponent<CustomerMovement>();

            if (OrderingCustomer == null && firstInLine.counterReached)
            {
                OrderingCustomer = CustomerManager.WaitingCustomers[0];
            }

            for (int i = 0; i < ActiveEmployees.Count; i++) //check all employees' statuses
            {
                if (ActiveEmployees[i].ServedCustomer == null && !currentCustomerServed) //when the loop caches an employee that is empty-handed
                {
                    currentCustomerServed = true;
                    ActiveEmployees[i].ServedCustomer = OrderingCustomer; //assign the current customer to the current employee
                    OrderingCustomer = null; //clear out the slot for the next customer
                    RuntimeEvents.OrderAccepted.Raise();
                }
            }

            currentCustomerServed = false; //let the next customer be assigned to an employee
        }
    }

    private void LoadAllSpawnPoints()
    {
        for (int i = 0; i < spawnPositions.Count; i++)
        {
            SpawnPoints.Add(new SpawnPoint(spawnPositions[i].position, spawnPositions[i].rotation, false));
        }
    }

    public void HireAnEmployee() //called by: EmployeeHired
    {
        for (int i = 0; i < SpawnPoints.Count; i++)
        {
            if(!SpawnPoints[i].IsOccupied)
            {
                GameObject newEmployee = Instantiate(employeePrefab, SpawnPoints[i].Position, SpawnPoints[i].Rotation);
                ActiveEmployees.Add(newEmployee.GetComponent<Employee>());
                newEmployee.GetComponent<EmployeeMerge>().SpawnPointIndex = ActiveEmployees.Count - 1;
                SpawnPoints[i].IsOccupied = true;
                RuntimeEvents.NewCustomerAtCounter.Raise();
                return;
            }
        }
        
        //if (ActiveEmployees.Count >= allEmployees.Count) RuntimeEvents.MaxEmployeesReached.Raise();
    }

}
