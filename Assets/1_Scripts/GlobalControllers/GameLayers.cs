using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameLayers : MonoBehaviour
{
    [SerializeField] private int employee;
    public static int Employee;

    [SerializeField] private int counter;
    public static int Counter;

    [SerializeField] private int customer;
    public static int Customer;

    [SerializeField] private int entrance;
    public static int Entrance;

    private void Awake()
    {
        Employee = employee;
        Counter = counter;
        Customer = customer;
        Entrance = entrance;
    }
}
