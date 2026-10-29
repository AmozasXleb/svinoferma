using UnityEngine;

public class cameraSwitch : MonoBehaviour
{

    //public GameObject[] Cameras;
    public GameObject[] ActiveCameras;
    public bool fullScreen = false;

    public void UpdateActives()
    {
        int isOn = 0;
        for (int i = 0; i < ActiveCameras.Length; i++)
        {
            if (ActiveCameras[i].activeSelf == true)
            {
                isOn++;
            }
        }

        if (isOn == ActiveCameras.Length)
        {
            fullScreen = true;
        }

        Debug.Log(isOn);

    }

}
