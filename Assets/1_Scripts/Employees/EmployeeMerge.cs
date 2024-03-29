using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeMerge : MonoBehaviour
{
    private Vector3 startPosition;
    private Quaternion startRotation;
    public float heightOffset;
    public int SpawnPointIndex;
    public bool IsDragged;
    private Employee employee;
    private EmployeeStats employeeStats;
    private int maxLevel;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        employee = GetComponent<Employee>();
        employeeStats = GetComponent<EmployeeStats>();
        maxLevel = employeeStats.levels.Count - 1;
        animator.Play("Spawn", 0, 0f);
        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void OnMouseDown()
    {
        if(employeeStats.CurrentLevelNumber < maxLevel)
        {
            GetComponent<Animator>().Play("DragWiggle", 0, 0f);
            IsDragged = true;
        }
    }

    private void OnMouseDrag() //OG
    {
        if (employeeStats.CurrentLevelNumber < maxLevel) transform.position = new Vector3(MouseWorldPosition().x, MouseWorldPosition().y - heightOffset, transform.position.z);
    }

    private void OnMouseUp()
    {
        if (employeeStats.CurrentLevelNumber < maxLevel) ResetEmployee();
    }

    private Vector3 MouseWorldPosition()
    {
        Vector3 rawMousePosition = Input.mousePosition;
        rawMousePosition.z = Camera.main.WorldToScreenPoint(transform.position).z;

        return Camera.main.ScreenToWorldPoint(rawMousePosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == GameLayers.Employee) //when the employee gets hit by another employee
        {
            EmployeeStats otherStats = other.GetComponent<EmployeeStats>(); //store the other employee's stats for level check
            
            if (!IsDragged && employeeStats.CurrentLevelNumber == otherStats.CurrentLevelNumber) //if levels of both are same and this employe is the one being hit
            {
                //get all nececssary data from the other employee
                EmployeeMerge otherEmployeeMerge = other.GetComponent<EmployeeMerge>();
                Employee otherEmployee = other.GetComponent<Employee>();
                Instantiate(ParticleManager.instance.PoofParticle, transform.position, transform.rotation);
                employeeStats.ChangeLevel(); //Level-up this employee
                if (employeeStats.CurrentLevelNumber > EnvironmentController.KitchenLevel) RuntimeEvents.NewLevelAppeared.Raise();
                animator.Play("Spawn", 0, 0f); //play the spawn animation on this employee after leveling up
                otherEmployeeMerge.ForceOrderFinish(); //force finish the other employees order since he will be destroyed
                EmployeeManager.instance.ActiveEmployees.Remove(otherEmployee); //remove the other employee from active employees
                EmployeeManager.SpawnPoints[otherEmployeeMerge.SpawnPointIndex].IsOccupied = false; //free the other employee's spawn point
                Destroy(other.gameObject); //destroy the other employee
            }
        }
    }

    public void ForceOrderFinish()
    {
        if(employee.ServedCustomer != null)
        {
            Instantiate(ParticleManager.instance.PoofParticle, employee.ServedCustomer.position, transform.rotation);
            Instantiate(ParticleManager.instance.CashEarnedParticle, startPosition + new Vector3(0f, 3.2f, 0f), transform.rotation);
            RuntimeEvents.OrderFinished.Raise();
            Destroy(employee.ServedCustomer.gameObject); //remove this employee's served customer from the game
        }
    }

    private void ResetEmployee()
    {
        IsDragged = false;
        transform.position = startPosition;
        transform.rotation = startRotation;
        animator.Play("Spawn", 0, 0f);
    }
}
