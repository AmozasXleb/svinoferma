using UnityEngine;
using System.Collections.Generic;

public class cameraSwitch : MonoBehaviour
{

    //public GameObject[] Cameras;
    public List<GameObject> ActiveCameras;
    public bool fullScreen = false;

    public void UpdateActives()
    {

        if (4 == ActiveCameras.Count)
        {
            fullScreen = true;
        }
        

    }

}
