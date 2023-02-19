using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeMerge : MonoBehaviour
{
    private float zCoordinate;
    private Vector3 startPosition;
    private Quaternion startRotation;
    public int SpawnPointIndex;
    public bool IsDragged;
    private Employee employee;
    private EmployeeStats employeeStats;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        employee = GetComponent<Employee>();
        employeeStats = GetComponent<EmployeeStats>();
        animator.Play("Spawn", 0, 0f);
    }

    private void OnEnable()
    {
        if (animator != null)
        {
            animator.Play("Spawn", 0, 0f);
        }

        startPosition = transform.position;
        startRotation = transform.rotation;
    }

    private void OnMouseDown()
    {
        zCoordinate = Camera.main.WorldToScreenPoint(transform.position).z;
        GetComponent<Animator>().Play("DragWiggle", 0, 0f);
        IsDragged = true;
    }

    private void OnMouseDrag()
    {
        transform.position = new Vector3(MouseWorldPosition().x, MouseWorldPosition().y, transform.position.z);
    }

    private void OnMouseUp()
    {
        ResetEmployee();
    }

    private Vector3 MouseWorldPosition()
    {
        Vector3 rawMousePosition = Input.mousePosition;
        rawMousePosition.z = zCoordinate;

        return Camera.main.ScreenToWorldPoint(rawMousePosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == GameLayers.Employee) //when the employee gets hit by another employee
        {
            EmployeeStats otherStats = other.GetComponent<EmployeeStats>(); //store the other employee's stats for level check

            if (!IsDragged && employeeStats.currentLevel == otherStats.currentLevel) //if levels of both are same and this employe is the one being hit
            {
                //get all nececssary data from the other employee
                EmployeeMerge otherEmployeeMerge = other.GetComponent<EmployeeMerge>();
                Employee otherEmployee = other.GetComponent<Employee>();

                employeeStats.ChangeLevel(); //Level-up this employee
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
            ParticleManager.DisappearParticlePosition = employee.ServedCustomer.position; //let the particle manager know where to spawn a poof particle
            ParticleManager.CashEarnedParticlePosition = startPosition + new Vector3(0f, 3.2f, 0f); //let the particle manager know where to spawn a money particle
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
