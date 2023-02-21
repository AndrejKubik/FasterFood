using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    [SerializeField] private List<CinemachineVirtualCamera> cameras;

    private void Update()
    {
        //if(EmployeeManager.SpawnPoints[10].IsOccupied || EmployeeManager.SpawnPoints[9].IsOccupied)
        //{
        //    cameras[0].Priority = 0;
        //    cameras[1].Priority = 0;
        //    cameras[2].Priority = 0;
        //    cameras[3].Priority = 1;
        //}
        if (EmployeeManager.SpawnPoints[4].IsOccupied || EmployeeManager.SpawnPoints[3].IsOccupied)
        {
            cameras[0].Priority = 0;
            cameras[1].Priority = 0;
            cameras[2].Priority = 1;
            //cameras[3].Priority = 0;
        }
        else if (EmployeeManager.SpawnPoints[2].IsOccupied || EmployeeManager.SpawnPoints[1].IsOccupied)
        {
            cameras[0].Priority = 0;
            cameras[1].Priority = 1;
            cameras[2].Priority = 0;
            //cameras[3].Priority = 0;
        }
        else
        {
            cameras[0].Priority = 1;
            cameras[1].Priority = 0;
            cameras[2].Priority = 0;
            //cameras[3].Priority = 0;
        }


        //    if (EmployeeManager.instance.ActiveEmployees.Count <= 5)
        //{
        //    cameras[0].Priority = 1;
        //    cameras[1].Priority = 0;
        //    cameras[2].Priority = 0;
        //    cameras[3].Priority = 0;
        //}
        //else if(EmployeeManager.instance.ActiveEmployees.Count > 5 && EmployeeManager.instance.ActiveEmployees.Count <= 7)
        //{
        //    cameras[0].Priority = 0;
        //    cameras[1].Priority = 1;
        //    cameras[2].Priority = 0;
        //    cameras[3].Priority = 0;
        //}
        //else if (EmployeeManager.instance.ActiveEmployees.Count > 7 && EmployeeManager.instance.ActiveEmployees.Count <= 9)
        //{
        //    cameras[0].Priority = 0;
        //    cameras[1].Priority = 0;
        //    cameras[2].Priority = 1;
        //    cameras[3].Priority = 0;
        //}
        //else if (EmployeeManager.instance.ActiveEmployees.Count > 9 && EmployeeManager.instance.ActiveEmployees.Count <= 11)
        //{
        //    cameras[0].Priority = 0;
        //    cameras[1].Priority = 0;
        //    cameras[2].Priority = 0;
        //    cameras[3].Priority = 1;
        //}
    }
}
