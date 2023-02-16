using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeMerge : MonoBehaviour
{
    private float zCoordinate;
    private Vector3 startPosition;
    private bool isDragged;
    [SerializeField] private Employee employee;
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
        isDragged = true;
    }

    private void OnMouseDrag()
    {
        transform.position = new Vector3(MouseWorldPosition().x, MouseWorldPosition().y, transform.position.z);
    }

    private void OnMouseUp()
    {
        animator.Play("Idle", 0, 0f);
        isDragged = false;
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
            if (!isDragged)
            {
                ParticleManager.DisappearParticlePosition = transform.position;
                RuntimeEvents.EmployeesMerged.Raise();
                RuntimeEvents.NewCustomerAtCounter.Raise();
                RuntimeEvents.OrderFinished.Raise();
            }
            else if(isDragged)
            {
                Destroy(employee.ServedCustomer.gameObject);
                Destroy(gameObject);
            }
        }
    }
}
