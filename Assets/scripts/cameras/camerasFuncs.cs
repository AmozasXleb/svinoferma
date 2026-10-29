using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine;

public class camerasFuncs : MonoBehaviour
{
    public GameObject whatCameraAttechedTo;
    public cameraSwitch CameraSwitch;
    public GameObject camera1;
    public GameObject camera2;
    public bool isClicked = false;

    private void OnMouseDown()
    {
        CheckCameraRay(camera1.GetComponentInChildren<Camera>());
        CheckCameraRay(camera2.GetComponentInChildren<Camera>());
    }

    void CheckCameraRay(Camera cam)
    {
        if (cam == null) return;

        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if (hit.transform == this.transform)
            {
                CameraSwitch.UpdateActives();
                if (CameraSwitch.fullScreen == false)
                {
                    //Debug.Log($"Сигнал (клик) получен от камеры: {cam.name}");
                    whatCameraAttechedTo.SetActive(true);
                    Debug.Log(whatCameraAttechedTo);
                    CameraSwitch.UpdateActives();


                    //camera1.SetActive(false);
                }
                else
                {
                    whatCameraAttechedTo.SetActive(true);
                    whatCameraAttechedTo.GetComponent<Camera>().rect = cam.rect;
                    //camera1.transform.GetChild(0).gameObject.SetActive(false);
                    //gameObject.GetComponentInChildren<Camera>().rect = cam.rect;
                    //Debug.Log(camera1.transform.GetChild(0));
                    //camera1.transform.GetChild(0).gameObject.SetActive(true);
                    //CameraSwitch.UpdateActives();

                }

            }
        }
    }
}
