using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerMovement : MonoBehaviour
{
    private bool counterReached;

    private void Update()
    {
        if(!counterReached) transform.Translate(Vector3.forward * 2f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Counter")) counterReached = true;
    }
}
