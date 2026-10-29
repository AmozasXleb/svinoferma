using UnityEngine;
using UnityEngine.EventSystems;

public class camerasFuncs : MonoBehaviour
{
    public GameObject whatCameraAttechedTo;
    public cameraSwitch CameraSwitch;
    public GameObject camera1;

    private void OnMouseDown()
    {
        CheckCameraRay(camera1.GetComponentInChildren<Camera>());
    }

    void CheckCameraRay(Camera cam)
    {
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == this.transform)
            {
                //Debug.Log($"Сигнал (клик) получен от камеры: {cam.name}");
                
            }
        }
    }
}
