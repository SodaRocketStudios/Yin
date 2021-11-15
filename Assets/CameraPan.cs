using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class CameraPan : MonoBehaviour
{
    [SerializeField]
    private CinemachineVirtualCamera VCam1;
    [SerializeField]
    private CinemachineVirtualCamera VCam2;

    public UnityEvent OnCameraReturn;

    public void SwitchCamera()
    {
        int temp = VCam1.Priority;
        VCam1.Priority = VCam2.Priority;
        VCam2.Priority = temp;
        if(VCam1.Priority < VCam2.Priority)
        {
            StartCoroutine(Timer());
        }
    }

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(5);
        OnCameraReturn.Invoke();
        SwitchCamera();
        
    }
}