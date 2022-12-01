using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private CustomerSettings customerSettings;
    public static CustomerSettings CustomerSettings;

    public static CameraControl CameraControl;

    private void Awake()
    {
        CustomerSettings = customerSettings;
        CameraControl = GetComponent<CameraControl>();
    }
}
