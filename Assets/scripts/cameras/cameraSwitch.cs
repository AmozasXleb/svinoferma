using UnityEngine;

public class cameraSwitch : MonoBehaviour
{

    public GameObject[] Cameras;
    public GameObject[] ActiveCameras;

    void Update()
    {
        for (int i = 0; i < Cameras.Length; i++)
        {
            if (Cameras[i].activeInHierarchy == true)
            {
               
            }
            //if (ActiveCameras[i] != null)
            //{
            //    ActiveCameras[i].SetActive(true);
            //}
        }
    }

}
