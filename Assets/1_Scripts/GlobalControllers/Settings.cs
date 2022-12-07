using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Settings : MonoBehaviour
{
    [SerializeField] private CustomerSettings customerSettings;
    public static CustomerSettings CustomerSettings;

    [SerializeField] private EmployeeSettings employeeSettings;
    public static EmployeeSettings EmployeeSettings;

    public static CameraControl CameraControl;

    [SerializeField] private ShopSettings shopSettings;
    public static ShopSettings ShopSettings;

    private void Awake()
    {
        CustomerSettings = customerSettings;
        EmployeeSettings = employeeSettings;
        CameraControl = GetComponent<CameraControl>();
        ShopSettings = shopSettings;
    }
}
