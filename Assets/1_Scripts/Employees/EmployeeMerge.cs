using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeMerge : MonoBehaviour
{
    private float zCoordinate;
    private Vector3 startPosition;
    public bool IsDragged;
    private Employee employee;
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        employee = GetComponent<Employee>();

        animator.Play("Spawn", 0, 0f);
        startPosition = transform.position;
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
        animator.Play("Idle", 0, 0f);
        IsDragged = false;
        transform.position = startPosition;
    }

    private Vector3 MouseWorldPosition()
    {
        Vector3 rawMousePosition = Input.mousePosition;
        rawMousePosition.z = zCoordinate;

        return Camera.main.ScreenToWorldPoint(rawMousePosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Employee"))
        {
            if (!IsDragged)
            {
                ParticleManager.DisappearParticlePosition = transform.position;
            }
            else if(IsDragged)
            {
                RuntimeEvents.EmployeesMerged.Raise();
                ForceOrderFinish();
                gameObject.SetActive(false);
            }
        }
    }

    private void ForceOrderFinish()
    {
        RuntimeEvents.NewCustomerAtCounter.Raise();
        if(employee.ServedCustomer != null)
        {
            ParticleManager.DisappearParticlePosition = employee.ServedCustomer.position; //let the particle manager know where to spawn a poof particle
            ParticleManager.CashEarnedParticlePosition = startPosition + new Vector3(0f, 3.2f, 0f); //let the particle manager know where to spawn a money particle
            RuntimeEvents.OrderFinished.Raise();
            Destroy(employee.ServedCustomer.gameObject); //remove the completely served customer from the game
        }
    }
}
