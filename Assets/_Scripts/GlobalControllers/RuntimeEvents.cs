using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuntimeEvents : MonoBehaviour
{
    [SerializeField] private GameEvent orderAccepted;
    public static GameEvent OrderAccepted;

    [SerializeField] private GameEvent newCustomerAtCounter;
    public static GameEvent NewCustomerAtCounter;

    private void Awake()
    {
        OrderAccepted = orderAccepted;
        NewCustomerAtCounter = newCustomerAtCounter;
    }
}
