using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraControl : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;

    [SerializeField] private CinemachineVirtualCamera camera2;

    public void SwitchCamera() //called by a button in pause menu
    {
        if(camera2.Priority == 0) camera2.Priority = 1;
        else if(camera2.Priority == 1) camera2.Priority = 0;
    }
}
