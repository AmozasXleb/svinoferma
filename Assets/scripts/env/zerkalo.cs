using UnityEngine;

public class zerkalo : MonoBehaviour
{
    public bool onKuh = true;
    public GameObject kuhnCam;
    public GameObject komnCam;
    public cameraSwitch CameraSwitch;


    private void OnMouseEnter()
    {
            gameObject.GetComponent<Outline>().OutlineWidth = 7.34f;
    }

    private void OnMouseExit()
    {
        gameObject.GetComponent<Outline>().OutlineWidth = 0f;
    }

    public void OnMouseDown()
    {
        if (onKuh)
        {
            kuhnCam.SetActive(true);
            kuhnCam.GetComponent<Camera>().rect = komnCam.GetComponent<Camera>().rect;
            for (int i = 0; i < CameraSwitch.ActiveCameras.Count; i++)
            {
                if (CameraSwitch.ActiveCameras[i] == komnCam)
                {
                    CameraSwitch.ActiveCameras[i] = kuhnCam;
                    onKuh = false;

                    komnCam.SetActive(false);
                }
            }
        }
        else
        {
            komnCam.SetActive(true);
            komnCam.GetComponent<Camera>().rect = kuhnCam.GetComponent<Camera>().rect;
            for (int i = 0; i < CameraSwitch.ActiveCameras.Count; i++)
            {
                if (CameraSwitch.ActiveCameras[i] == kuhnCam)
                {
                    CameraSwitch.ActiveCameras[i] = komnCam;
                    onKuh = true;

                    kuhnCam.SetActive(false);
                }
            }
        }

    }
}
