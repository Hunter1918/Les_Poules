using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CAM_Manager : MonoBehaviour
{
    public CinemachineVirtualCamera mainCam;
    public CinemachineVirtualCamera secondaryCam;

    void update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SwitchToSecondaryCam();
        }
        else
        {
            SwitchToMainCam();
        }
    }
    private void SwitchToSecondaryCam()
    {
        mainCam.Priority = 0;
        secondaryCam.Priority = 10;
    }
    private void SwitchToMainCam()
    {
        mainCam.Priority = 10;
        secondaryCam.Priority = 0;
    }
}
