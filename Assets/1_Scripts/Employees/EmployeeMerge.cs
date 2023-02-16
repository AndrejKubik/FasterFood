using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeMerge : MonoBehaviour
{
    private float zCoordinate;
    private Vector3 startPosition;
    private Quaternion startRotation;
    public bool IsDragged;
    private Employee employee;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        employee = GetComponent<Employee>();
        animator.Play("Spawn", 0, 0f);
    }

    private void OnEnable()
    {
        if (animator != null)
        {
            animator.Play("Spawn", 0, 0f);
            //animator.applyRootMotion = true;
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
        if(other.gameObject.layer == GameLayers.Employee)
        {
            if (!IsDragged)
            {
                ParticleManager.DisappearParticlePosition = transform.position;
            }
            else if(IsDragged)
            {
                ForceOrderFinish();
                ResetEmployee();
                gameObject.SetActive(false);
                RuntimeEvents.EmployeesMerged.Raise();
            }
        }
    }

    private void ForceOrderFinish()
    {
        if(employee.ServedCustomer != null)
        {
            ParticleManager.DisappearParticlePosition = employee.ServedCustomer.position; //let the particle manager know where to spawn a poof particle
            ParticleManager.CashEarnedParticlePosition = startPosition + new Vector3(0f, 3.2f, 0f); //let the particle manager know where to spawn a money particle
            RuntimeEvents.OrderFinished.Raise();
            Destroy(employee.ServedCustomer.gameObject); //remove the completely served customer from the game
        }
        RuntimeEvents.NewCustomerAtCounter.Raise();
    }

    private void ResetEmployee()
    {
        IsDragged = false;
        transform.position = startPosition;
        transform.rotation = startRotation;
        animator.StopPlayback();
    }
}
