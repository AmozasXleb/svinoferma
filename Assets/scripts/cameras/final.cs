using UnityEngine;

public class final : MonoBehaviour
{
    public GameObject lv;
    public GameObject ln;
    public GameObject pv;
    public GameObject pn;

    public cameraSwitch switchCam;

    void Update()
    {
        if (lv.GetComponent<Camera>().rect == new Rect (0, 0.5f, 0.5f, 0.5f) && ln.GetComponent<Camera>().rect == new Rect(0, 0, 0.5f, 0.5f) && pn.GetComponent<Camera>().rect == new Rect(0.5f, 0f, 0.5f, 0.5f) && pv.GetComponent<Camera>().rect == new Rect(0.5f, 0.5f, 0.5f, 0.5f))
        {
            if (switchCam.ActiveCameras.Contains(lv) && switchCam.ActiveCameras.Contains(ln) && switchCam.ActiveCameras.Contains(pv) && switchCam.ActiveCameras.Contains(pn))
            {
                Debug.Log("ура");
            }
        }
    }
}
